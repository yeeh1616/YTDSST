
using System.IO.Ports;
using System.Threading;

using YTDSSTGenII.Utils;


namespace YTDSSTGenII.Service
{
    class JCMBillAcceptorClass
    {
        public static int BillType = 0;
        public static bool CanBillAddFlag = false;
        public static IniFile ini = new IniFile("D://advitise//1.ini");
        public static int jcmFailCount = 0;
        public static int jcmKaChaoCount = 0;
        public static int jcmKaChaoMaxTimes = 0x19;
        public static SerialPort MyPort = null;

        /**
         * 重置纸币器
         */ 
        public static void ResetJCMBillAcceptor()
        {
            try
            {
                Reset();
                UsefulAllBillType();
                SetAllSecurityNormal();
                SetRehibit();
                SetDirection();
                OptionFunction();
                SetCommunitToPoll();
            }
            catch (System.Exception) { }
        }

        public static void ClosePort()
        {
            if (MyPort != null)
            {
                MyPort.Close();
            }
        }

        public static void InhibitAllBillType()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 7, 0xc0, 0xff, 0xff, 0x95, 0x45 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 7)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        private static void OpenPort()
        {
            if (MyPort == null)
            {
                MyPort = new SerialPort();
                string s = "9600";
                MyPort.PortName = ini.ReadIniValue("配置", "纸币接收器");
                MyPort.BaudRate = int.Parse(s);
                MyPort.DataBits = 8;
                MyPort.StopBits = StopBits.One;
                MyPort.Parity = Parity.Even;
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

        public static void OptionFunction()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 7, 0xc5, 0, 0, 0x90, 140 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 7)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        public static int Poll()
        {
            OpenPort();
            byte[] buffer = new byte[] { 0xfc, 5, 0x11, 0x27, 0x56 };
            MyPort.Write(buffer, 0, buffer.Length);
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 5)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                MyPort.Read(buffer2, 0, bytesToRead);
                if (bytesToRead < 1)
                {
                    CommonUtils.WriteCashInfo("通信异常：" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                    ini.WriteIniValue("配置", "钞箱状态", "1");
                    GlobalParmeters.CashAcceptorStatus[0] = 1;
                    return 0;
                }
                GlobalParmeters.CashAcceptorStatus[0] = 0;
                CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                if (buffer2[2] == 0x13)
                {
                    CanBillAddFlag = true;
                    BillType = buffer2[3];
                    SetToBox();
                    return 0;
                }
                if (buffer2[2] == 0x15)
                {
                    SetACKCommond();
                    return 0;
                }
                if (buffer2[2] == 0x43)
                {
                    GlobalParmeters.ShowMessage = "纸币钞箱已满，请联系客服";
                    ini.WriteIniValue("配置", "钞箱状态", "2");
                    InhibitAllBillType();
                    return 0;
                }
                if (buffer2[2] != 0x16)
                {
                    if (buffer2[2] == 0x44)
                    {
                        GlobalParmeters.CashBoxFlag = true;
                        CommonUtils.WriteCashInfo("钞箱被取走");
                        ini.WriteIniValue("配置", "钞箱状态", "1");
                        return 0;
                    }
                    if ((buffer2[2] == 0x45) || (buffer2[2] == 0x49))
                    {
                        jcmKaChaoCount++;
                        if (jcmKaChaoCount >= jcmKaChaoMaxTimes)
                        {
                            CommonUtils.WriteCashInfo("通信异常：卡钞 " + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                            jcmKaChaoCount = 0;
                            return -1;
                        }
                    }
                    if ((((buffer2[2] == 0x45) || (buffer2[2] == 0x49)) || ((buffer2[2] == 70) || (buffer2[2] == 0x47))) || ((buffer2[2] == 0x48) || (buffer2[2] == 0x4a)))
                    {
                        jcmFailCount++;
                        if (jcmFailCount >= 15)
                        {
                            CommonUtils.WriteCashInfo("通信异常：" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                            ini.WriteIniValue("配置", "钞箱状态", "1");
                            jcmFailCount = 0;
                            Reset();
                        }
                    }
                    if ((buffer2[2] == 0x1a) && !GlobalParmeters.ForbitInputFlag)
                    {
                        SetRehibit();
                    }
                    return 0;
                }
                if (CanBillAddFlag)
                {
                    CanBillAddFlag = false;
                    if (BillType > 0)
                    {
                        ini.WriteIniValue("配置", "钞箱状态", "0");
                        switch (BillType)
                        {
                            case 0x61:
                                return 1;

                            case 0x63:
                                return 5;

                            case 100:
                                return 10;

                            case 0x65:
                                return 20;

                            case 0x66:
                                return 50;

                            case 0x67:
                                return 100;
                        }
                    }
                }
            }
            return 0;
        }

        public static void Reset()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 5, 0x40, 0x2b, 0x15 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 5)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        private static void SetACKCommond()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 5, 80, 170, 5 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
        }

        public static void SetAllSecurityNormal()
        {
            OpenPort();
            byte[] buffer = new byte[] { 0xfc, 7, 0xc1, 0, 0, 0xf1, 0xef };
            MyPort.Write(buffer, 0, buffer.Length);
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 7)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer2 = new byte[bytesToRead];
                MyPort.Read(buffer2, 0, bytesToRead);
                if (buffer2 != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                }
            }
        }

        public static void SetCommunitToPoll()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 6, 0xc2, 0, 220, 0xcf };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 7)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        public static void SetDirection()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 6, 0xc4, 0, 12, 0x9b };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 6)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        public static void SetRehibit()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 6, 0xc3, 0, 4, 0xd6 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 6)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        private static void SetToBox()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 5, 0x41, 0xa2, 4 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 5)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }

        public static void UsefulAllBillType()
        {
            OpenPort();
            byte[] inp = new byte[] { 0xfc, 7, 0xc0, 0, 0, 0x2d, 0xb5 };
            CommonUtils.WriteCashInfo("Send:" + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
            MyPort.Write(inp, 0, inp.Length);
            for (int i = 0; i < 20; i++)
            {
                if (MyPort.BytesToRead > 0)
                {
                    break;
                }
                Thread.Sleep(5);
            }
            if (MyPort.BytesToRead >= 1)
            {
                if (MyPort.BytesToRead < 7)
                {
                    Thread.Sleep(50);
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                if (buffer != null)
                {
                    CommonUtils.WriteCashInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                }
            }
        }
    }
}
