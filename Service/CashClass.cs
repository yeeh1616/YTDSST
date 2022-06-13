using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Data;
using System.Data.SQLite;
using System.IO.Ports;

using YTDSSTGenII.Service.Exception;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Service
{
    class CashClass
    {
        public static IniFile ini = new IniFile("D://advitise//1.ini");
        public static SerialPort MyPort = null;
        public static byte[] PayoutDatas = new byte[] { 0x30, 0xb1, 0xb2, 0x33, 180, 0x35, 0x36, 0xb7, 0xb8, 0x39 };

        public static void ClosePort()
        {
            if ((MyPort != null) && MyPort.IsOpen)
            {
                MyPort.Close();
            }
        }

        private static ushort CRC_Creater(ushort Cnt_CRC, byte Data, ushort Cnt_COM)
        {
            try
            {
                ushort num = 0;
                while (num < 8)
                {
                    ushort num2 = Data;
                    ushort num3 = Cnt_CRC;
                    Cnt_CRC = (ushort)(Cnt_CRC >> 1);
                    num2 = (ushort)(num2 ^ num3);
                    num2 = (ushort)(num2 & 1);
                    if (num2 == 1)
                    {
                        Cnt_CRC = (ushort)(Cnt_CRC ^ Cnt_COM);
                    }
                    num = (ushort)(num + 1);
                    Data = (byte)(Data >> 1);
                }
                return Cnt_CRC;
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
                return 0;
            }
        }

        private static ushort crc16l(byte[] buf, ushort len)
        {
            try
            {
                ushort num2 = 0;
                for (ushort i = 2; i < (len - 2); i = (ushort)(i + 1))
                {
                    num2 = CRC_Creater(num2, buf[i], 0x8408);
                }
                return num2;
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
                return 0;
            }
        }

        public static int[] Execute(string count)
        {
            int[] numArray = new int[2];
            try
            {
                OpenPort();
                byte[] mixedQuery = GetMixedQuery(count.PadLeft(2, '0'));
                int.Parse(GetIndex(mixedQuery[8]) + GetIndex(mixedQuery[9]));
                if ((mixedQuery == null) || (int.Parse(count) > 20))
                {
                    return numArray;
                }
                byte[] buffer2 = SendCommand(mixedQuery);
                if ((buffer2 == null) || (buffer2.Length <= 0x34))
                {
                    return numArray;
                }
                if (buffer2[4] == 0xe0)
                {
                    int num = int.Parse(GetIndex(buffer2[0x33]) + GetIndex(buffer2[0x34]));
                    numArray[0] = int.Parse(count);
                    numArray[1] = num;
                    CommonUtils.WritePayoutInfo(string.Concat(new object[] { "成功退币", count, "张,进回收箱", num, "次" }));
                    return numArray;
                }
                string index = GetIndex(buffer2[0x2b]);
                string str6 = GetIndex(buffer2[0x2c]);
                string str7 = GetIndex(buffer2[0x33]);
                string str8 = GetIndex(buffer2[0x34]);
                int num2 = int.Parse(index + str6);
                int num3 = int.Parse(str7 + str8);
                numArray[0] = num2;
                numArray[1] = num3;
                CommonUtils.WritePayoutInfo(string.Concat(new object[] { "成功退币", num2, "张,进回收箱", num3, "次" }));
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
            }
            return numArray;
        }

        private static string GetIndex(byte Data)
        {
            try
            {
                for (int i = 0; i < PayoutDatas.Length; i++)
                {
                    if (PayoutDatas[i] == Data)
                    {
                        return i.ToString();
                    }
                }
                return "0";
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
                return "0";
            }
        }

        private static byte[] GetMixedQuery(string MoneyCount)
        {
            try
            {
                byte[] buf = new byte[0x21];
                buf[0] = 0x10;
                buf[1] = 2;
                buf[2] = 0;
                buf[3] = 0x19;
                buf[4] = 0x60;
                buf[5] = 3;
                buf[6] = 0x15;
                buf[7] = 0xe4;
                buf[8] = PayoutDatas[int.Parse(MoneyCount.Substring(0, 1))];
                buf[9] = PayoutDatas[int.Parse(MoneyCount.Substring(1, 1))];
                buf[10] = 0x30;
                buf[11] = 0x30;
                buf[12] = 0x30;
                buf[13] = 0x30;
                buf[14] = 0x30;
                buf[15] = 0x30;
                buf[0x10] = 0x30;
                buf[0x11] = 0xb8;
                buf[0x12] = 0x30;
                buf[0x13] = 0x30;
                buf[20] = 0x30;
                buf[0x15] = 0x30;
                buf[0x16] = 0x30;
                buf[0x17] = 0x30;
                buf[0x18] = 2;
                buf[0x19] = 0;
                buf[0x1a] = 0;
                buf[0x1b] = 0;
                buf[0x1c] = 0x1c;
                buf[0x1d] = 0x10;
                buf[30] = 3;
                byte[] bytes = BitConverter.GetBytes(crc16l(buf, (ushort)buf.Length));
                buf[0x1f] = bytes[0];
                buf[0x20] = bytes[1];
                return buf;
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
                return null;
            }
        }

        private static void OpenPort()
        {
            if (MyPort == null)
            {
                MyPort = new SerialPort();
                string s = "9600";
                MyPort.PortName = ini.ReadIniValue("配置", "纸币找零器");
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

        public static void Reset()
        {
            try
            {
                OpenPort();
                byte[] buffer = ResetMethods();
                if (buffer != null)
                {
                    if ((buffer.Length > 8) && (buffer[4] == 0xe0))
                    {
                        GlobalParmeters.downtime = false;
                        ini.WriteIniValue("配置", "纸币找零器状态", "0");
                    }
                    else
                    {
                        CommonUtils.WriteServiceSuspendedInfo("需要暂停服务，错误代码：" + Convert.ToString(buffer[8]).PadLeft(2, '0'));
                        GlobalParmeters.downtime = true;
                    }
                }
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
            }
        }

        private static byte[] ResetMethods()
        {
            byte[] buffer = null;
            try
            {
                buffer = new byte[200];
                for (int i = 0; i < 3; i++)
                {
                    buffer = SendCommand1(new byte[] { 
                        0x10, 2, 0, 0x11, 0x60, 2, 13, 0, 150, 130, 0, 0, 0, 0, 0, 0,
                        11, 0, 0, 0, 0x1c, 0x10, 3, 0x90, 0xc4
                    });
                    if (((buffer != null) && (buffer.Length > 7)) && (((buffer[4] == 0xe0) || (buffer[7] == 0xf1)) || (buffer[7] == 0xf6)))
                    {
                        return buffer;
                    }
                }
                return buffer;
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
            }
            return null;
        }

        private static byte[] SendCommand(byte[] data)
        {
            try
            {
                byte[] inp = new byte[] { 0x10, 5 };
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
                MyPort.Write(inp, 0, inp.Length);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(10);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(50);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(data, 0, data.Length));
                MyPort.Write(data, 0, data.Length);
                for (int j = 0; j < 20; j++)
                {
                    Thread.Sleep(10);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(50);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int count = MyPort.BytesToRead;
                byte[] buffer3 = new byte[count];
                MyPort.Read(buffer3, 0, count);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer3, 0, buffer3.Length));
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                for (int k = 0; k < 800; k++)
                {
                    Thread.Sleep(50);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(100);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int num6 = MyPort.BytesToRead;
                byte[] buffer4 = new byte[num6];
                MyPort.Read(buffer4, 0, num6);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer4, 0, buffer4.Length));
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                byte[] buffer5 = new byte[] { 0x10, 6 };
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer5, 0, buffer5.Length));
                MyPort.Write(buffer5, 0, buffer5.Length);
                for (int m = 0; m < 50; m++)
                {
                    Thread.Sleep(10);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(200);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int num8 = MyPort.BytesToRead;
                byte[] buffer6 = new byte[num8];
                MyPort.Read(buffer6, 0, num8);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer6, 0, buffer6.Length));
                byte[] bytes = BitConverter.GetBytes(crc16l(buffer6, (ushort)buffer6.Length));
                if (((buffer6.Length > 0x34) && (bytes[0] == buffer6[buffer6.Length - 2])) && (bytes[1] == buffer6[buffer6.Length - 1]))
                {
                    if (buffer6[4] == 0xe0)
                    {
                        byte[] buffer8 = new byte[] { 0x10, 6 };
                        CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer8, 0, buffer8.Length));
                        MyPort.Write(buffer8, 0, buffer8.Length);
                        return buffer6;
                    }
                    if (buffer6[4] == 240)
                    {
                        if ((buffer6[7] == 0xf1) || (buffer6[7] == 0xf6))
                        {
                            GlobalParmeters.downtime = true;
                            CommonUtils.WriteServiceSuspendedInfo("需要暂停服务，错误代码：" + Convert.ToString(buffer6[8]).PadLeft(2, '0'));
                            byte[] buffer9 = new byte[] { 0x10, 6 };
                            CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer9, 0, buffer9.Length));
                            MyPort.Write(buffer9, 0, buffer9.Length);
                            return buffer6;
                        }
                        if ((((buffer6[7] == 120) || (buffer6[7] == 0x7a)) || ((buffer6[7] == 0x7b) || (buffer6[7] == 0x7c))) || (((buffer6[7] == 0x7d) || (buffer6[7] == 0x70)) || (buffer6[7] == 0x88)))
                        {
                            byte[] buffer10 = new byte[] { 0x10, 6 };
                            CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer10, 0, buffer10.Length));
                            MyPort.Write(buffer10, 0, buffer10.Length);
                            Reset();
                            return buffer6;
                        }
                        byte[] buffer11 = new byte[] { 0x10, 6 };
                        CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer11, 0, buffer11.Length));
                        MyPort.Write(buffer11, 0, buffer11.Length);
                        return buffer6;
                    }
                }
                else
                {
                    byte[] buffer12 = null;
                    for (int n = 0; n < 2; n++)
                    {
                        byte[] buffer13 = new byte[] { 0x10, 0x15 };
                        CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer13, 0, buffer13.Length));
                        MyPort.Write(buffer13, 0, buffer13.Length);
                        MyPort.DiscardInBuffer();
                        MyPort.DiscardOutBuffer();
                        for (int num10 = 0; num10 < 50; num10++)
                        {
                            Thread.Sleep(10);
                            if (MyPort.BytesToRead > 0)
                            {
                                break;
                            }
                        }
                        Thread.Sleep(200);
                        if (MyPort.BytesToRead < 1)
                        {
                            return null;
                        }
                        int num11 = MyPort.BytesToRead;
                        buffer12 = new byte[num11];
                        MyPort.Read(buffer12, 0, num11);
                        CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer12, 0, buffer12.Length));
                        BitConverter.GetBytes(crc16l(buffer12, (ushort)buffer12.Length));
                        if (((buffer12.Length > 0x34) && (bytes[0] == buffer12[buffer12.Length - 2])) && (bytes[1] == buffer12[buffer12.Length - 1]))
                        {
                            break;
                        }
                    }
                    if (buffer12.Length > 0x34)
                    {
                        if (buffer12[4] == 0xe0)
                        {
                            byte[] buffer14 = new byte[] { 0x10, 6 };
                            CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer14, 0, buffer14.Length));
                            MyPort.Write(buffer14, 0, buffer14.Length);
                            return buffer12;
                        }
                        if (buffer12[4] == 240)
                        {
                            if ((buffer12[7] == 0xf1) || (buffer6[7] == 0xf6))
                            {
                                GlobalParmeters.downtime = true;
                                CommonUtils.WriteServiceSuspendedInfo("需要暂停服务，错误代码：" + Convert.ToString(buffer12[8]).PadLeft(2, '0'));
                                byte[] buffer15 = new byte[] { 0x10, 6 };
                                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer15, 0, buffer15.Length));
                                MyPort.Write(buffer15, 0, buffer15.Length);
                                return buffer12;
                            }
                            if ((((buffer12[7] == 120) || (buffer12[7] == 0x7a)) || ((buffer12[7] == 0x7b) || (buffer12[7] == 0x7c))) || (((buffer12[7] == 0x7d) || (buffer12[7] == 0x70)) || (buffer12[7] == 0x88)))
                            {
                                byte[] buffer16 = new byte[] { 0x10, 6 };
                                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer16, 0, buffer16.Length));
                                MyPort.Write(buffer16, 0, buffer16.Length);
                                Reset();
                                return buffer12;
                            }
                            byte[] buffer17 = new byte[] { 0x10, 6 };
                            CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer17, 0, buffer17.Length));
                            MyPort.Write(buffer17, 0, buffer17.Length);
                            return buffer12;
                        }
                    }
                }
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
            }
            return null;
        }

        private static byte[] SendCommand1(byte[] data)
        {
            try
            {
                byte[] inp = new byte[] { 0x10, 5 };
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(inp, 0, inp.Length));
                MyPort.Write(inp, 0, inp.Length);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(10);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(50);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int bytesToRead = MyPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                MyPort.Read(buffer, 0, bytesToRead);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(data, 0, data.Length));
                MyPort.Write(data, 0, data.Length);
                for (int j = 0; j < 20; j++)
                {
                    Thread.Sleep(10);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(50);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int count = MyPort.BytesToRead;
                byte[] buffer3 = new byte[count];
                MyPort.Read(buffer3, 0, count);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer3, 0, buffer3.Length));
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                for (int k = 0; k < 500; k++)
                {
                    Thread.Sleep(60);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(100);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int num6 = MyPort.BytesToRead;
                byte[] buffer4 = new byte[num6];
                MyPort.Read(buffer4, 0, num6);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer4, 0, buffer4.Length));
                MyPort.DiscardInBuffer();
                MyPort.DiscardOutBuffer();
                byte[] buffer5 = new byte[] { 0x10, 6 };
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer5, 0, buffer5.Length));
                MyPort.Write(buffer5, 0, buffer5.Length);
                for (int m = 0; m < 50; m++)
                {
                    Thread.Sleep(10);
                    if (MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(200);
                if (MyPort.BytesToRead < 1)
                {
                    return null;
                }
                int num8 = MyPort.BytesToRead;
                byte[] buffer6 = new byte[num8];
                MyPort.Read(buffer6, 0, num8);
                CommonUtils.WritePayoutInfo("Read: " + CommonUtils.ByteArray2HexString(buffer6, 0, buffer6.Length));
                byte[] buffer7 = new byte[] { 0x10, 6 };
                CommonUtils.WritePayoutInfo("Send: " + CommonUtils.ByteArray2HexString(buffer7, 0, buffer7.Length));
                MyPort.Write(buffer7, 0, buffer7.Length);
                return buffer6;
            }
            catch (System.Exception exception)
            {
                CommonUtils.WritePayoutInfo(exception.ToString());
            }
            return null;
        }
    }
}
