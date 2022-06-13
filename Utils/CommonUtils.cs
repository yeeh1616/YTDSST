using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using YTDSSTGenII.Service.Sg;

namespace YTDSSTGenII.Utils
{
    public class CommonUtils
    {
        public static string ByteArray2HexString(byte[] inp, int offset, int len)
        {
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str = str + Convert.ToString(inp[i + offset], 0x10).PadLeft(2, '0');
            }
            return str;
        }


        public static int ReadCashCount()
        {
            try
            {
                string path = Application.StartupPath + @"\CashCount.txt";
                if (!File.Exists(path))
                {
                    StreamWriter writer = File.CreateText(path);
                    writer.WriteLine("0");
                    writer.Close();
                }
                StreamReader reader = new StreamReader(File.OpenRead(path));
                string s = reader.ReadLine();
                int num = 0;
                try
                {
                    num = int.Parse(s);
                }
                catch (Exception exception)
                {
                    WriteExceptionInfo(exception);
                }
                reader.Close();
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static int ReadCashFaultCount()
        {
            try
            {
                string path = Application.StartupPath + @"\CashFaultCount.txt";
                if (!File.Exists(path))
                {
                    StreamWriter writer = File.CreateText(path);
                    writer.WriteLine("0");
                    writer.Close();
                }
                StreamReader reader = new StreamReader(File.OpenRead(path));
                string s = reader.ReadLine();
                int num = 0;
                try
                {
                    num = int.Parse(s);
                }
                catch (Exception exception)
                {
                    WriteExceptionInfo(exception);
                }
                reader.Close();
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static int ReadCoinCount()
        {
            try
            {
                string path = Application.StartupPath + @"\CoinCount.txt";
                if (!File.Exists(path))
                {
                    StreamWriter writer = File.CreateText(path);
                    writer.WriteLine("0");
                    writer.Close();
                }
                StreamReader reader = new StreamReader(File.OpenRead(path));
                string s = reader.ReadLine();
                int num = 0;
                try
                {
                    num = int.Parse(s);
                }
                catch (Exception exception)
                {
                    WriteExceptionInfo(exception);
                }
                reader.Close();
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static int ReadCoinFaultCount()
        {
            try
            {
                string path = Application.StartupPath + @"\CoinFaultCount.txt";
                if (!File.Exists(path))
                {
                    StreamWriter writer = File.CreateText(path);
                    writer.WriteLine("0");
                    writer.Close();
                }
                StreamReader reader = new StreamReader(File.OpenRead(path));
                string s = reader.ReadLine();
                int num = 0;
                try
                {
                    num = int.Parse(s);
                }
                catch (Exception exception)
                {
                    WriteExceptionInfo(exception);
                }
                reader.Close();
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static int ReadMoneyCount()
        {
            try
            {
                string path = Application.StartupPath + @"\MoneyCount.txt";
                if (!File.Exists(path))
                {
                    StreamWriter writer = File.CreateText(path);
                    writer.WriteLine("0");
                    writer.Close();
                }
                StreamReader reader = new StreamReader(File.OpenRead(path));
                string s = reader.ReadLine();
                int num = 0;
                try
                {
                    num = int.Parse(s);
                }
                catch (Exception exception)
                {
                    WriteExceptionInfo(exception);
                }
                reader.Close();
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static void showTime(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\showTime"))
                {
                    Directory.CreateDirectory(@"E:\showTime");
                }
                string path = @"E:\showTime\showTime" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString("HH-mm-ss-ffff   ") + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteCashCount(int s1)
        {
            try
            {
                string path = Application.StartupPath + @"\CashCount.txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = new StreamWriter(File.OpenWrite(path));
                writer2.WriteLine(s1.ToString());
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteCashFaultCount(int s1)
        {
            try
            {
                string path = Application.StartupPath + @"\CashFaultCount.txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = new StreamWriter(File.OpenWrite(path));
                writer2.WriteLine(s1.ToString());
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteCashInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Cashlog"))
                {
                    Directory.CreateDirectory(@"E:\Cashlog");
                }
                string path = @"E:\Cashlog\Cashlog" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteCoinCount(int s1)
        {
            try
            {
                string path = Application.StartupPath + @"\CoinCount.txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = new StreamWriter(File.OpenWrite(path));
                writer2.WriteLine(s1.ToString());
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteCoinFaultCount(int s1)
        {
            try
            {
                string path = Application.StartupPath + @"\CoinFaultCount.txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = new StreamWriter(File.OpenWrite(path));
                writer2.WriteLine(s1.ToString());
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteExceptionInfo(Exception s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Excetionlog"))
                {
                    Directory.CreateDirectory(@"E:\Excetionlog");
                }
                string path = @"E:\Excetionlog\Excetionlog" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1.Message + s1.StackTrace);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteExceptionInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Excetionlog"))
                {
                    Directory.CreateDirectory(@"E:\Excetionlog");
                }
                string path = @"E:\Excetionlog\ExceptionByString" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteKeyInfo(byte[] s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Keylog"))
                {
                    Directory.CreateDirectory(@"E:\Keylog");
                }
                string path = @"E:\Keylog\Keylogbyte[]" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString("HH-mm-ss-ffff") + ByteArray2HexString(s1, 0, s1.Length));
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteKeyInfo(int s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Keylog"))
                {
                    Directory.CreateDirectory(@"E:\Keylog");
                }
                string path = @"E:\Keylog\Keylog" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1.ToString());
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteMoney(int s1)
        {
            try
            {
                string path = Application.StartupPath + @"\MoneyCount.txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = new StreamWriter(File.OpenWrite(path));
                writer2.WriteLine(s1.ToString());
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteMootrInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\MotorStatus"))
                {
                    Directory.CreateDirectory(@"E:\MotorStatus");
                }
                string path = @"E:\MotorStatus\MotorStatus" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WritePayoutInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Payout"))
                {
                    Directory.CreateDirectory(@"E:\Payout");
                }
                string path = @"E:\Payout\Payout" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteReceiveCoinInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\Cashlog"))
                {
                    Directory.CreateDirectory(@"E:\Cashlog");
                }
                string path = @"E:\Cashlog\ReceiveCoin" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WriteServiceSuspendedInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\ServiceSuspended"))
                {
                    Directory.CreateDirectory(@"E:\ServiceSuspended");
                }
                string path = @"E:\ServiceSuspended\ServiceSuspended" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1);
                writer2.Close();
            }
            catch
            {
            }
        }

        public static void WritePayLogInfo(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\WeiXinLog"))
                {
                    Directory.CreateDirectory(@"E:\WeiXinLog");
                }
                string path = @"E:\WeiXinLog\WeiXinLog" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    File.CreateText(path).Close();
                }
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1);
                writer2.Close();
            }
            catch
            {
            }
        }


        public static string ErroMessage()
        {
            string str = "";
            try
            {
                int type = 0;
                int size = 0x400;
                byte[] bytes = new byte[0x400];
                int num3 = SGBentDll.sgERRMsg(type, ref bytes[0], size);
                byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, bytes, 0, bytes.Length);
                string str2 = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
                str = string.Concat(new object[] { "兑奖返回码： ", num3, "\r\n  错误信息：", str2 });
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return str;
        }

        public static void ErroMessage(ref string errocode, ref string erromessage)
        {
            try
            {
                int type = 0;
                int size = 0x400;
                byte[] bytes = new byte[0x400];
                int num3 = SGBentDll.sgERRMsg(type, ref bytes[0], size);
                byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, bytes, 0, bytes.Length);
                string str = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
                errocode = num3.ToString();
                erromessage = str;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        public static int sgInit(string hwsn, ref string strversion)
        {
            int num = -1;
            try
            {
                byte[] bytes = new byte[0x400];
                int num2 = SGBentDll.sgInit(hwsn, ref bytes[0]);
                byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, bytes, 0, bytes.Length);
                string str = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
                strversion = str;
                return num2;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return num;
            }
        }
    }
}
