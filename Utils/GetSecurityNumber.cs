using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Configuration;

namespace YTDSSTGenII.Utils
{
    class GetSecurityNumber
    {
        private IniFile ini = new IniFile("D://advitise//1.ini");
        private SerialPort MyPort;

        public static string ByteArray2HexStringForLog(byte[] inp, int offset, int len)
        {
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str = str + Convert.ToString(inp[i + offset], 0x10).PadLeft(2, '0');
            }
            return str;
        }

        public void ClosePort()
        {
            if (this.MyPort.IsOpen)
            {
                this.MyPort.Close();
            }
            if (this.MyPort != null)
            {
                this.MyPort = null;
            }
        }

        public bool OpenPort()
        {
            try
            {
                this.MyPort = new SerialPort();
                this.MyPort.PortName = this.ini.ReadIniValue("配置", "兑奖");
                this.MyPort.BaudRate = 0x2580;
                this.MyPort.DataBits = 8;
                this.MyPort.StopBits = StopBits.One;
                this.MyPort.Open();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }
            return true;
        }

        public string ReadSecurityNumber()
        {
            string str = "";
            try
            {
                if (this.MyPort == null)
                {
                    this.OpenPort();
                }
                if (!this.MyPort.IsOpen)
                {
                    this.MyPort.Open();
                }
                byte[] buffer = new byte[] { 0x1b, 0x31 };
                this.MyPort.DiscardInBuffer();
                this.MyPort.DiscardOutBuffer();
                this.MyPort.Write(buffer, 0, buffer.Length);
                for (int i = 0; i < 60; i++)
                {
                    if (this.MyPort.BytesToRead > 0x1b)
                    {
                        break;
                    }
                    Thread.Sleep(50);
                }
                if (this.MyPort.BytesToRead < 11)
                {
                    Thread.Sleep(700);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                if ((0x29 > this.MyPort.BytesToRead) && (0x1c < this.MyPort.BytesToRead))
                {
                    byte[] buffer2 = new byte[bytesToRead];
                    this.MyPort.Read(buffer2, 0, buffer2.Length);
                    if ((buffer2[bytesToRead - 2] == 13) && (buffer2[bytesToRead - 1] == 10))
                    {
                        str = Encoding.ASCII.GetString(buffer2);
                    }
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return str;
        }
    }
}
