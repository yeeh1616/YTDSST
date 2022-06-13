using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Service.Exception;

namespace YTDSSTGenII.Service
{
    class ControlPanel
    {
        private static IniFile ini = new IniFile("D://advitise//1.ini");
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
                MyPort.PortName = ini.ReadIniValue("配置", "控制板");
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

        public static byte[] Query()
        {
            OpenPort();
            byte[] buffer = new byte[] { 0x7e, 0, 3, 1, 3, 0, 4, 0xed };
            MyPort.DiscardInBuffer();
            MyPort.DiscardOutBuffer();
            MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 50; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(20);
            }
            Thread.Sleep(40);
            if (MyPort.BytesToRead < 1)
            {
                return null;
            }
            byte[] buffer2 = new byte[MyPort.BytesToRead];
            MyPort.Read(buffer2, 0, MyPort.BytesToRead);
            return buffer2;
        }

        public static void SendCommand(byte data, byte data1)
        {
            // 控制板协议
            byte[] buffer = new byte[] { 0x7e, 0, 5, 1, 2, 0, data, data1};
            OpenPort();
            buffer = new byte[] { 0x7e, 0, 5, 1, 2, 0, data, data1, (byte)((((buffer[3] + buffer[4]) + buffer[5]) + buffer[6]) + buffer[7]), 0xed };
            MyPort.Write(buffer, 0, buffer.Length);
        }
    }
}
