
using Mina.Core.Service;

using log4net;

using YTDSSTGenII.Service.Wexin.Data;

namespace YTDSSTGenII.Service.Wexin.Mina
{
    public class WexinHandle : IoHandlerAdapter
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WexinHandle));

        private WeXinSocketClient socketClient;

        public WexinHandle(WeXinSocketClient socketClient) {
            this.socketClient = socketClient;
        }

        override public void ExceptionCaught(global::Mina.Core.Session.IoSession session, System.Exception cause) 
        {
            logger.Error(cause);

            base.ExceptionCaught(session, cause);
        }

        override public void MessageReceived(global::Mina.Core.Session.IoSession session, object message)
        {
            WexinPacket packetData = (WexinPacket)message;
            
            //根据ProtocolId 分发到不同的业务处理类上
            IProtocolHandle handle = ProtocolHandleFactory.Lookup(packetData.ProtocolId);
            if (handle != null)
            {
                RemoteData remoteData = handle.DoHandle(packetData);
                logger.Info(remoteData);
                if (socketClient != null)
                {
                    socketClient.OnReceiveRemoteData(remoteData);
                }
            }
            else 
            {
                logger.Error("不能识别的协议类型:" + packetData.ToString());
            }

            base.MessageReceived(session, message);
        }
    }
}
