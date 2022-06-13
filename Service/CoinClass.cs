using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Data;
using System.Data.SQLite;

using System.Threading;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Service
{
    class CoinClass
    {
        public int EventCount;
        private IniFile ini = new IniFile("D://advitise//1.ini");
        public SerialPort MyPort;

        public CoinClass()
        {
            this.OpenPort();
        }

        public void ActivateCoinPayment()
        {
            try
            {
                byte[] buffer = new byte[] { 3, 1, 1, 0xa4, 0xa5, 0xb2 };
                this.MyPort.Write(buffer, 0, buffer.Length);
                for (int i = 0; i < 20; i++)
                {
                    if (this.MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }
                if (this.MyPort.BytesToRead >= 1)
                {
                    if (this.MyPort.BytesToRead < 11)
                    {
                        Thread.Sleep(30);
                    }
                    int bytesToRead = this.MyPort.BytesToRead;
                    byte[] buffer2 = new byte[bytesToRead];
                    this.MyPort.Read(buffer2, 0, bytesToRead);
                }
            }
            catch (System.Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        public static string ByteArray2HexString(byte[] inp, int offset, int len)
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
            if (this.MyPort != null)
            {
                this.MyPort.Close();
            }
            this.MyPort = null;
        }

        public int CoinPayout(int Money)
        {
            try
            {
                CommonUtils.WritePayoutInfo("CoinPayout start");
                this.ActivateCoinPayment();
                this.QuerySecret();
                byte[] buffer = new byte[14];
                buffer[0] = 3;
                buffer[1] = 9;
                buffer[2] = 1;
                buffer[3] = 0xa7;
                buffer[4] = 0;
                buffer[5] = 0;
                buffer[6] = 0;
                buffer[7] = 0;
                buffer[8] = 0;
                buffer[9] = 0;
                buffer[10] = 0;
                buffer[11] = 0;
                buffer[12] = (byte) Money;
                byte num = (byte) ((((((((((((buffer[0] + buffer[1]) + buffer[2]) + buffer[3]) + buffer[4]) + buffer[5]) + buffer[6]) + buffer[7]) + buffer[8]) + buffer[9]) + buffer[10]) + buffer[11]) + buffer[12]);
                buffer[13] = (byte) ((0xff - num) + 1);
                if (buffer[12] > 20)
                {
                    CommonUtils.WritePayoutInfo("退硬币个数大于20，Money=" + Money);
                    return 0;
                }
                this.MyPort.Write(buffer, 0, buffer.Length);
                CommonUtils.WritePayoutInfo("Send:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                for (int i = 0; i < 20; i++)
                {
                    if (this.MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }
                if (this.MyPort.BytesToRead < 1)
                {
                    CommonUtils.WritePayoutInfo("无数据返回");
                    return 0;
                }
                if (this.MyPort.BytesToRead < 20)
                {
                    Thread.Sleep(500);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                CommonUtils.WritePayoutInfo("start Read,Length=" + buffer2.Length.ToString());
                this.MyPort.Read(buffer2, 0, bytesToRead);
                if ((buffer2 != null) && (buffer2.Length > 1))
                {
                    CommonUtils.WritePayoutInfo("Read:" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                }
                if (bytesToRead < 20)
                {
                    return 0;
                }
                byte[] buffer3 = new byte[] { 3, 0, 1, 0xa6, 0x56 };
                this.MyPort.DiscardInBuffer();
                this.MyPort.DiscardOutBuffer();
                int num4 = 0;
                while (num4 < 5)
                {
                    Thread.Sleep(50);
                    this.MyPort.Write(buffer3, 0, buffer3.Length);
                    for (int j = 0; j < 20; j++)
                    {
                        if (this.MyPort.BytesToRead > 0)
                        {
                            break;
                        }
                        Thread.Sleep(5);
                    }
                    if (this.MyPort.BytesToRead < 6)
                    {
                        num4++;
                    }
                    else
                    {
                        if (this.MyPort.BytesToRead < 14)
                        {
                            Thread.Sleep(100);
                        }
                        int count = this.MyPort.BytesToRead;
                        byte[] buffer4 = new byte[count];
                        this.MyPort.Read(buffer4, 0, count);
                        if (buffer4 != null)
                        {
                            CommonUtils.WritePayoutInfo("Read:" + CommonUtils.ByteArray2HexString(buffer4, 0, buffer4.Length));
                            byte num7 = 0;
                            for (int k = 0; k < (buffer4.Length - 1); k++)
                            {
                                num7 = (byte) (num7 + buffer4[k]);
                            }
                            byte num9 = (byte) ((0xff - num7) + 1);
                            if ((buffer4.Length > 12) && (num9 == buffer4[buffer4.Length - 1]))
                            {
                                num4 = 0;
                                if ((buffer4[11] + buffer4[12]) == Money)
                                {
                                    return Convert.ToInt32(buffer4[11]);
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return 0;
        }

        private void InitOperate()
        {
            byte[] buffer = new byte[] { 2, 0, 1, 0xfe, 0xff };
            this.MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 10)
                {
                    Thread.Sleep(30);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
            }
        }

        public void OpenPort()
        {
            if (this.MyPort == null)
            {
                this.MyPort = new SerialPort();
                this.MyPort.WriteTimeout = 0x7d0;
                string s = "9600";
                this.MyPort.PortName = this.ini.ReadIniValue("配置", "硬币器");
                this.MyPort.BaudRate = int.Parse(s);
                this.MyPort.DataBits = 8;
                this.MyPort.StopBits = StopBits.One;
                try
                {
                    this.MyPort.Open();
                    this.InitOperate();
                    this.SelfCheck();
                    this.TestSoleNoid();
                    this.PollStatus();
                }
                catch (System.Exception exception)
                {
                    CommonUtils.WriteExceptionInfo(exception);
                }
            }
        }

        private void PollStatus()
        {
            byte[] buffer = new byte[] { 2, 0, 1, 0xe5, 0x18 };
            this.MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 0x15)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
                if (buffer2.Length > 10)
                {
                    this.EventCount = buffer2[9];
                }
            }
        }

        public bool QuerySecret()
        {
            try
            {
                byte[] buffer = new byte[] { 3, 0, 1, 160, 0x5c };
                this.MyPort.Write(buffer, 0, buffer.Length);
                CommonUtils.WritePayoutInfo("QuerySecret Send:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                for (int i = 0; i < 20; i++)
                {
                    if (this.MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }
                if (this.MyPort.BytesToRead < 1)
                {
                    return false;
                }
                if (this.MyPort.BytesToRead < 0x12)
                {
                    Thread.Sleep(600);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
                CommonUtils.WritePayoutInfo("QuerySecret Read:" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
            }
            catch (System.Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }
            return true;
        }

        public void RESET()
        {
            byte[] buffer = new byte[] { 3, 0, 1, 1, 0xfb };
            this.MyPort.Write(buffer, 0, buffer.Length);
            CommonUtils.WritePayoutInfo("RESET send:" + ByteArray2HexString(buffer, 0, buffer.Length));
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 10)
                {
                    Thread.Sleep(100);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
                if ((buffer2 != null) && (buffer2.Length > 1))
                {
                    CommonUtils.WritePayoutInfo("RESET Read:" + ByteArray2HexString(buffer2, 0, buffer2.Length));
                }
            }
        }

        public void SelfCheck()
        {
            byte[] buffer = new byte[] { 2, 0, 1, 0xe8, 0x15 };
            this.MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 11)
                {
                    Thread.Sleep(30);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
            }
        }

        public void TestHOPPER()
        {
            byte[] buffer = new byte[] { 3, 0, 1, 0xa3, 0x59 };
            this.MyPort.Write(buffer, 0, buffer.Length);
            CommonUtils.WritePayoutInfo("测试HOPPEN:send:" + ByteArray2HexString(buffer, 0, buffer.Length));
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 12)
                {
                    Thread.Sleep(100);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
                if ((buffer2 != null) && (buffer2.Length > 1))
                {
                    CommonUtils.WritePayoutInfo("测试HOPPEN:Read:" + ByteArray2HexString(buffer2, 0, buffer2.Length));
                    this.RESET();
                }
            }
        }

        private void TestSoleNoid()
        {
            byte[] buffer = new byte[] { 2, 1, 1, 240, 1, 11 };
            this.MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 11)
                {
                    Thread.Sleep(30);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
            }
        }

        private void UnuseAllPipe()
        {
            byte[] buffer = new byte[] { 2, 2, 1, 0xe7, 0, 0, 20 };
            this.MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 12)
                {
                    Thread.Sleep(30);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
            }
        }

        public void UseOnePipe()
        {
            byte[] buffer = new byte[] { 2, 2, 1, 0xe7, 1, 0, 0x13 };
            this.MyPort.Write(buffer, 0, buffer.Length);
            for (int i = 0; i < 20; i++)
            {
                if (this.MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (this.MyPort.BytesToRead >= 1)
            {
                if (this.MyPort.BytesToRead < 12)
                {
                    Thread.Sleep(30);
                }
                int bytesToRead = this.MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                this.MyPort.Read(buffer2, 0, bytesToRead);
            }
        }
    }
}
