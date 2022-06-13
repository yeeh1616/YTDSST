using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Utils.Network.Http;
using YTDSSTGenII.Utils;
using YTDSSTGenII.Service.Enums;
using YTDSSTGenII.Service.Context;

namespace YTDSSTGenII.Service
{
    class HostLoader
    {
        private static String balanceURL = "http://balance.jkp.ticai.cn/hosts?teminal_code=";
        private static String testBalanceURL = "http://balance.jkp.ticai.cn/test-hosts?teminal_code=";

        public static void loadHostInfo() {
            try
            {
                IniFile configIni = new IniFile("D://advitise/1.ini");
                String terminalCode = configIni.ReadIniValue("配置", "本机编号");

                String hostUrl = balanceURL;
                
                if (MachineContext.AppEnvMode == EnumAppEnvMode.TEST) {
                    hostUrl = testBalanceURL;
                }

                ResponseData responseData = HttpClientProxy.newProxy().sendRequest<LinkedList<Host>>(hostUrl + terminalCode);
                
                if (responseData.Status == ResponseStatus.SUCCESS)
                {
                    LinkedList<Host> hosts = (LinkedList<Host>)responseData.Data;
                    if (hosts != null)
                    {
                        foreach (Host host in hosts)
                        {
                            //兑奖服务器地址
                            if ("prize_server".Equals(host.HostType))
                            {
                                //兑奖服务器IP 兑奖端口
                                configIni.WriteIniValue("配置", "兑奖服务器IP", host.HostName);
                                configIni.WriteIniValue("配置", "兑奖端口", host.Port + "");
                                continue;
                            }
                            //数据上传服务器地址
                            if ("data_server".Equals(host.HostType))
                            {
                                //服务器IP 服务器端口
                                configIni.WriteIniValue("配置", "服务器IP", host.HostName);
                                configIni.WriteIniValue("配置", "服务器端口", host.Port + "");
                                continue;
                            }
                            //在线支付服务器地址
                            if ("pay_server".Equals(host.HostType))
                            {
                                //微信支付服务器 微信支付端口
                                configIni.WriteIniValue("配置", "微信支付服务器", host.HostName);
                                configIni.WriteIniValue("配置", "微信支付端口", host.Port + "");
                                continue;
                            }
                        }
                    }
                }
            }
            catch (System.Exception exp) {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }
    }

    class Host {
        private String hostType;
        private String hostName;
        private int port;

        public String HostName {
            get { return hostName; }
            set {
                this.hostName = value;
            }
        }

        public String HostType {
            get { return hostType; }
            set {
                this.hostType = value;
            }
        }

        public int Port {
            get { return port; }
            set {
                this.port = value;
            }
        }
    }
}
