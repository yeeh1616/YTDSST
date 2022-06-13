using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace YTDSSTGenII.Utils
{
    class SocketClient
    {
        public static string DrawIp = "";
        public static string DrawPort = "";
        public Socket DrawSocket;
        public Socket MySocket;
        public static string ServerIp = "192.168.8.1";
        public static string ServerPort = "23";

        private static Object _lock = new object();
        
        public static string ByteArray2HexString(byte[] inp, int offset, int len)
        {
            string str2;
            try
            {
                string str = "";
                for (int i = 0; i < len; i++)
                {
                    str = str + Convert.ToString(inp[i + offset], 0x10).PadLeft(2, '0');
                }
                str2 = str;
            }
            catch
            {
                throw;
            }
            return str2;
        }

        public void CloseDraw()
        {
            if (this.DrawSocket != null)
            {
                this.DrawSocket.Close();
                this.DrawSocket = null;
            }
        }

        public void CloseSocket()
        {
            if (this.MySocket != null)
            {
                this.MySocket.Close();
                this.MySocket = null;
            }
        }

        private byte[] GetReturnData(byte[] ReceiveData, int byteLength)
        {
            try
            {
                byte[] buffer = new byte[byteLength];
                for (int i = 0; i < byteLength; i++)
                {
                    buffer[i] = ReceiveData[i];
                }
                return buffer;
            }
            catch
            {
                return null;
            }
        }

        public bool IsSocketConnected(Socket client)
        {
            try
            {
                string str = "7E0100336B23E59BA49C9E9C9D9B9C9C9CA09F6DA2A39F9EA49E9EA09B9C9FA2A09FA39F9BA49B9DA19FA0A4A171A29CA4FCED";
                byte[] buffer = new byte[str.Length / 2];
                for (int i = 0; i < (str.Length - 1); i += 2)
                {
                    byte num2 = (byte) Convert.ToInt32(str.Substring(i, 2), 0x10);
                    buffer[i / 2] = num2;
                }
                byte[] inp = buffer;
                WriteCheckExcetionlog("心跳:      " + ByteArray2HexString(inp, 0, inp.Length));
                byte[] receiveData = this.SendDrawData(inp);
                if (receiveData == null)
                {
                    GlobalParmeters.heartbeatFlag = false;
                    WriteCheckExcetionlog("心跳未返回");
                    return false;
                }
                GlobalParmeters.heartbeatFlag = false;
                byte[] returnData = this.GetReturnData(receiveData, receiveData.Length);
                WriteCheckExcetionlog("心跳返回 :     " + ByteArray2HexString(returnData, 0, returnData.Length));
                return true;
            }
            catch (SocketException exception)
            {
                GlobalParmeters.heartbeatFlag = false;
                WriteCheckExcetionlog("isConnected:" + exception.ToString());
            }
            finally
            {
                GlobalParmeters.heartbeatFlag = false;
            }
            return false;
        }

        public bool OpenDraw()
        {
            lock (_lock)
            {
                try
                {
                    if (this.DrawSocket == null || !this.DrawSocket.Connected)
                    {
                        this.DrawSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    
                    IPAddress address;
                    IPAddress.TryParse(SocketClient.DrawIp, out address);
                    if (address == null)
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(SocketClient.DrawIp);
                        address = hostEntry.AddressList[0];
                    }

                    IPEndPoint remoteEP = new IPEndPoint(address, int.Parse(SocketClient.DrawPort));
                    this.DrawSocket.ReceiveTimeout = 0x2710;
                    this.DrawSocket.Connect(remoteEP);
                }
                catch (Exception exception)
                {
                    WriteCheckExcetionlog("OpenSocket():" + exception.Message);
                    return false;
                }
                WriteCheckExcetionlog("打开连接成功");
                return true;
            }

            /** 
             * Deprecated by Bean.Long 
            try
            {
                IPAddress address = Dns.Resolve(SocketClient.DrawIp).AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(address, int.Parse(DrawPort));
                this.DrawSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.DrawSocket.ReceiveTimeout = 0x2710;
                this.DrawSocket.Connect(remoteEP);
            }
            catch (Exception exception)
            {
                WriteCheckExcetionlog("OpenSocket():" + exception.ToString());
                return false;
            }
            */
        }

        public bool OpenSocket()
        {
            lock (_lock)
            {
                try
                {
                    if (this.MySocket == null || !this.MySocket.Connected)
                    {
                        this.MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }

                    IPAddress address;
                    IPAddress.TryParse(SocketClient.ServerIp, out address);

                    if (address == null)
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(SocketClient.ServerIp);
                        address = hostEntry.AddressList[0];
                    }

                    IPEndPoint remoteEP = new IPEndPoint(address, int.Parse(SocketClient.ServerPort));
                    this.MySocket.ReceiveTimeout = 0x2710;
                    this.MySocket.Connect(remoteEP);
                }
                catch (Exception exception)
                {
                    WriteCheckExcetionlog("OpenSocket():" + exception.Message);
                    return false;
                }
                WriteCheckExcetionlog("打开连接成功");
                return true;
            }
            /**
             * Deprecated by Bean.Long
            try
            {
                IPAddress address = Dns.Resolve(ServerIp).AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(address, int.Parse(ServerPort));
                this.MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.MySocket.ReceiveTimeout = 2*1000;
                this.MySocket.Connect(remoteEP);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
            */
        }

        public byte[] ReadData()
        {
            try
            {
                byte[] buffer = new byte[0x400];
                int byteLength = 0;
                byteLength = this.MySocket.Receive(buffer, buffer.Length, SocketFlags.None);
                if (byteLength < 1)
                {
                    return null;
                }
                return this.GetReturnData(buffer, byteLength);
            }
            catch
            {
                return null;
            }
        }

        public void SendData(byte[] data)
        {
            try
            {
                if (this.MySocket == null)
                {
                    this.OpenSocket();
                }
                this.MySocket.Send(data, data.Length, SocketFlags.None);
            }
            catch (Exception)
            {
            }
        }

        public byte[] SendDrawData(byte[] data)
        {
            byte[] buffer3;
            int num = 0;
            WriteCheckExcetionlog("start 这个值为TRUE 等待30秒 GlobalParmeters.iscansend:  " + GlobalParmeters.iscansend);
            while (GlobalParmeters.iscansend)
            {
                try
                {
                    if (num < 100)
                    {
                        num++;
                        Thread.Sleep(300);
                    }
                    else
                    {
                        GlobalParmeters.iscansend = false;
                        WriteCheckExcetionlog("等待了30秒依然未得到通讯令牌那么断开连接 ");
                        this.CloseDraw();
                    }
                    continue;
                }
                catch (Exception)
                {
                    GlobalParmeters.iscansend = false;
                    WriteCheckExcetionlog("等待过程中出现异常");
                    this.CloseDraw();
                    continue;
                }
            }
            WriteCheckExcetionlog("end  这个值为TRUE 等待30秒 GlobalParmeters.iscansend:  " + GlobalParmeters.iscansend);
            try
            {
                if (this.DrawSocket == null)
                {
                    WriteCheckExcetionlog("通讯过程中出现了异常或者链接中断，从新链接");
                    this.OpenDraw();
                }
                this.DrawSocket.Send(data, data.Length, SocketFlags.None);
                WriteCheckExcetionlog("发包" + ByteArray2HexString(data, 0, data.Length));
                GlobalParmeters.iscansend = true;
                byte[] buffer = new byte[0x400];
                int byteLength = 0;
                byteLength = this.DrawSocket.Receive(buffer, buffer.Length, SocketFlags.None);
                if (byteLength < 1)
                {
                    WriteCheckExcetionlog("接收到的数据为空 bytelength=" + byteLength);
                    this.CloseDraw();
                    return null;
                }
                byte[] returnData = this.GetReturnData(buffer, byteLength);
                GlobalParmeters.iscansend = false;
                WriteCheckExcetionlog("收包" + ByteArray2HexString(returnData, 0, returnData.Length));
                buffer3 = returnData;
            }
            catch (Exception exception)
            {
                WriteCheckExcetionlog("向服务器发送数据过程中出现异常:" + exception.ToString());
                GlobalParmeters.iscansend = false;
                this.CloseDraw();
                buffer3 = null;
            }
            finally
            {
                GlobalParmeters.iscansend = false;
            }
            return buffer3;
        }

        public static void WriteCheckExcetionlog(string s1)
        {
            try
            {
                if (!Directory.Exists(@"E:\LotteryPrize"))
                {
                    Directory.CreateDirectory(@"E:\LotteryPrize");
                }
                string path = @"E:\LotteryPrize\LotteryPrize" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.CreateText(path).Close();
                }
                StreamWriter writer2 = System.IO.File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff  ") + s1);
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
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.CreateText(path).Close();
                }
                StreamWriter writer2 = System.IO.File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString() + s1.Message + s1.StackTrace);
                writer2.Close();
            }
            catch
            {
            }
        }
    }
}
