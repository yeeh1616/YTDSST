using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Utils;
using System.IO.Ports;

using YTDSSTGenII.Service.Exception;

namespace YTDSSTGenII.Service
{
    /**
     * 兑奖打孔处理
     */
    public static class PrizePunch
    {
        public static IniFile ini = new IniFile("D://advitise//1.ini");
        public static SerialPort MyPort = null;


        public static void ClosePort()
        {
            if (MyPort != null)
            {
                MyPort.Close();
            }
        }

        private static void OpenPort()
        {
            if (MyPort == null)
            {
                MyPort = new SerialPort();
                string s = "9600";
                MyPort.PortName = ini.ReadIniValue("配置", "兑奖打孔");
                MyPort.BaudRate = int.Parse(s);
                MyPort.DataBits = 8;
                MyPort.StopBits = StopBits.One;
                try
                {
                    if (!MyPort.IsOpen)
                    {
                        MyPort.Open();
                    }
                }
                catch (System.Exception exception)
                {
                    CommonUtils.WriteExceptionInfo(exception);
                }
            }
        }

        public static void Punch()
        {
            OpenPort();
            byte[] buffer = new byte[] { 0x7e, 0, 5, 1, 2, 0, 6, 0, 9, 0xed };
            MyPort.DiscardInBuffer();
            MyPort.DiscardOutBuffer();
            MyPort.Write(buffer, 0, buffer.Length);
        }
    }
}
