using System;
using System.Collections.Generic;
using System.Net;
using log4net;

using Newtonsoft.Json;

using Mina.Transport.Socket;
using Mina.Filter.Logging;
using Mina.Filter.Codec;
using Mina.Core.Future;


using YTDSSTGenII.Utils;
using YTDSSTGenII.Utils.Newtonsoft;
using YTDSSTGenII.Service.Exception;
using YTDSSTGenII.Service.Wexin.Mina;
using YTDSSTGenII.Service.Wexin.Data;

/**
 * 和微信相关的业务类
 */ 
namespace YTDSSTGenII.Service.Wexin
{
    /**
     * 和后台服务器同步微信支付交易的状态，使用长连接TCP模式，
     * 服务器会主动下推交易的状态和命令
     */ 
    public class WeXinSocketClient
    {
        private static ILog logger = LogManager.GetLogger(typeof(WeXinSocketClient));
        private static WeXinSocketClient instance;

        public static object LOCK_OBJ = new object();//锁对象
        
        public static WeXinSocketClient getInstance()
        {
            if(WeXinSocketClient.instance == null) 
            {
                WeXinSocketClient.instance = new WeXinSocketClient();
            }

            return WeXinSocketClient.instance;
        }

        public delegate void RemoteCommandDelegate(RemoteData packet);

        private List<RemoteCommandDelegate> delegates = new List<RemoteCommandDelegate>();

        public bool Connected { get; set; }

        private AsyncSocketConnector connector;
        private IConnectFuture cf;

        private WeXinSocketClient() {
            //TODO 加载配置文件，初始化远程服务器IP和端口等信息
        }

        /**
         * 注册代理处理逻辑
         */ 
        public void RegistDelegate(RemoteCommandDelegate delegator) {
            if (!delegates.Contains(delegator)) {
                lock (LOCK_OBJ)
                {
                    delegates.Add(delegator);
                }                 
            }
        }

        /**
         * 撤销代理
         */ 
        public void UnregistDeletegate(RemoteCommandDelegate delegator) {
            lock (LOCK_OBJ)
            {
                delegates.Remove(delegator);
            }              
        }

        /**
         * 连接服务器
         */ 
        public void ConnectToServer() {

            DoHeartBeat();//先发送心跳来判断连接状态

            if (!Connected) 
            {
                try
                {
                    CommonUtils.WritePayLogInfo("打开链接");
                    connector = new AsyncSocketConnector();

                    connector.FilterChain.AddLast("logger", new LoggingFilter());
                    connector.FilterChain.AddLast("codec", new ProtocolCodecFilter(new WexinCodecFactory()));

                    connector.ExceptionCaught += (o, e) =>
                    {
                        logger.Error(e.Exception);
                    };

                    connector.SessionIdle += (o, e) =>
                    {
                        logger.Info("IDLE " + e.Session.GetIdleCount(e.IdleStatus));
                    };

                    connector.Handler = new WexinHandle(this);
                    connector.ConnectTimeout = 5;

                    IPAddress address;
                    IPAddress.TryParse(Service.Context.ApplicationContext.getInstance().WEIXIN_SERVER_IP, out address);

                    if (address == null)
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(Service.Context.ApplicationContext.getInstance().WEIXIN_SERVER_IP);
                        address = hostEntry.AddressList[0];
                    }

                    cf = connector.Connect(new IPEndPoint(address, Convert.ToInt32(Service.Context.ApplicationContext.getInstance().WEIXIN_SERVER_PORT)));

                    cf.Await();

                    if (cf.Connected)
                    {
                        this.Connected = true;
                        CommonUtils.WritePayLogInfo("打开链接成功");
                    }
                    else
                    {
                        this.Connected = false;
                        CommonUtils.WritePayLogInfo("打开链接失败");
                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                                 
            }
        }

        /**
         * 关闭Socket连接
         */ 
        public void Close() {
            if (connector != null) 
            {
                try
                {
                    if (Connected)
                    {
                        connector.Dispose();
                    }
                }
                catch (System.Exception exp)
                {
                    logger.Info("关闭连接出现错误, " + exp.Message);
                }
                finally
                {
                    Connected = false;
                }
            } 
        }

        /**
         * 心跳，保持和服务器的命令通道连接
         */ 
        private void DoHeartBeat() 
        {
            try
            {
                RemoteData remoteData = new RemoteData();
                //远程命令参数设置
                //1.机器编号
                remoteData.MachineCode = GlobalParmeters.machineNumber; // GlobalParmeters.machineNumber
                                                                        //2.协议类型，即命令类型
                remoteData.ProtocolId = C004ContentReq.PROTOCOL_TYPE;
                //3.请求的参数
                C004ContentReq req = new C004ContentReq();
                remoteData.Content = req;

                WeXinSocketClient.getInstance().WriteSocketData(remoteData);
                Connected = true;
                //CommonUtils.WritePayLogInfo("发送心跳请求信息");
            }
            catch
            {
                Connected = false;
                CommonUtils.WritePayLogInfo("发送心跳请求信息异常");
            }
        }

        /**
         * 解析收到的协议内容，并分发给代理回调业务层方法
         */ 
        private void ParseAndDispactch(RemoteData data)
        {
            //Dispatch To Service Layer
            lock (LOCK_OBJ)
            {
                if (delegates != null && delegates.Count > 0)
                {
                    foreach (RemoteCommandDelegate delegator in delegates)
                    {
                        delegator(data);
                    }
                }
            }            
        }

        /**
         * 发送数据到服务器，包的内容及类型由调用者保证其正确性，此处只做包的封装
         */ 
        public void WriteSocketData(RemoteData remoteData)
        {
            if (!cf.Connected)
            {
                throw new ServiceException("网络已经断开.");
            }

            //RemoteData -> WexinPacket
            WexinPacket packet = new WexinPacket();

            packet.ProtocolId = remoteData.ProtocolId;
            packet.MachineCode = remoteData.MachineCode;

            //内容转成JSON文件
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            //new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver };
            String content = JsonConvert.SerializeObject(remoteData.Content, jsetting);
            if (content == null)
            {
                packet.jsonContent = "{}";
            }
            else
            {
                packet.jsonContent = content;
            }
            
            //生成签名
            String temp = packet.jsonContent + packet.MachineCode + "YTD.TICAI.CN";
            packet.Signature = MD5Utils.Md5(temp);

            try
            {
                //发送网络数据
                if (cf.Connected)
                {
                    IWriteFuture future = cf.Session.Write(packet);
                    future.Await();
                    if (future.Exception != null)
                    {
                        throw future.Exception;
                    }
                }
            }
            catch (System.Exception exp)
            {
                this.Connected = false;
                logger.Info("发送数据失败", exp);
                throw new ServiceException("网络已经断开.", exp);
            }
        }

        internal void OnReceiveRemoteData(RemoteData remoteData)
        {
            ParseAndDispactch(remoteData);                         
        }
    }

}
