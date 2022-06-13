using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Utils;
using System.IO.Ports;
using System.Threading;

namespace YTDSSTGenII.Utils
{
    public class MotorOperate
    {
        private string ComName = "";
        private IniFile ini = new IniFile("D://advitise//1.ini");
        public SerialPort MyPort;

        public MotorOperate()
        {
            this.ComName = this.ini.ReadIniValue("配置", "机头");
            this.MyPort = new SerialPort();
            this.MyPort.PortName = this.ComName;
            this.MyPort.BaudRate = 0x2580;
            this.MyPort.DataBits = 8;
            this.MyPort.StopBits = StopBits.One;
            try
            {
                this.MyPort.Open();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        public void ClosePort()
        {
            if (this.MyPort != null)
            {
                this.MyPort.Close();
            }
        }

        public byte[] CompositeQuery(int motorNumber)
        {
            byte[] mixedQuery = this.GetMixedQuery(motorNumber);
            return this.SendCommand(mixedQuery, motorNumber);
        }

        public byte[] CutCardOperate(int motorNumber)
        {
            byte[] cutCard = this.GetCutCard(motorNumber);
            return this.SendCommand(cutCard, motorNumber);
        }

        public static byte GetBCC(byte[] temp, int len)
        {
            byte num = 0;
            for (int i = 0; i < len; i++)
            {
                num = (byte)(num ^ temp[i]);
            }
            return num;
        }

        private byte[] GetCutCard(int motorNumber)
        {

            byte[] buffer = new byte[] { 0xa5, ((byte)motorNumber), 0x11, 1, 0, 0, 0, 0, 0, 0, 0xda };
            return new byte[] { 0xa5, ((byte)motorNumber), 0x11, 1, 0, 0, 0, 0, 0, 0, 0xda, GetBCC(buffer, 11) };
        }

        private byte[] GetMixedQuery(int motorNumber)
        {
            byte[] buffer = new byte[] { 0xa5, ((byte)motorNumber), 0x12, 0, 0, 0, 0, 0, 0, 0, 0xda };
            return new byte[] { 0xa5, ((byte)motorNumber), 0x12, 0, 0, 0, 0, 0, 0, 0, 0xda, GetBCC(buffer, 11) };
        }

        private byte[] GetSetCardLength(int motorNumber, int cardLength)
        {
            byte[] temp = new byte[12];
            temp[0] = 0xa5;
            temp[1] = (byte)motorNumber;
            temp[2] = 0x10;
            temp[3] = 0;
            temp[4] = 0;
            temp[5] = 0;
            temp[6] = 0;
            temp[7] = 0;
            if (cardLength > 0x66)
            {
                cardLength -= 0x80;
                temp[8] = 1;
                temp[9] = (byte)cardLength;
            }
            else
            {
                temp[8] = 0;
                temp[9] = (byte)cardLength;
            }
            temp[10] = 0xda;
            temp[11] = GetBCC(temp, 11);
            return temp;
        }

        private byte[] SendCommand(byte[] data, int motorNumber)
        {
            this.MyPort.DiscardInBuffer();
            this.MyPort.DiscardOutBuffer();
            CommonUtils.WriteMootrInfo("send:" + CommonUtils.ByteArray2HexString(data, 0, data.Length));
            this.MyPort.Write(data, 0, data.Length);
            int num = 0;
            int num2 = 70;
            while (num++ < num2)
            {
                if (this.MyPort.BytesToRead != 0)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            Thread.Sleep(50);
            int bytesToRead = this.MyPort.BytesToRead;
            if (bytesToRead < 2)
            {
                return null;
            }
            byte[] buffer = new byte[bytesToRead];
            this.MyPort.Read(buffer, 0, bytesToRead);
            CommonUtils.WriteMootrInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
            if (buffer[0] != 0x87)
            {
                return null;
            }
            byte[] buffer2 = new byte[] { 0x9e, (byte)motorNumber };
            this.MyPort.DiscardOutBuffer();
            this.MyPort.DiscardInBuffer();
            this.MyPort.Write(buffer2, 0, 2);
            CommonUtils.WriteMootrInfo("Send:" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
            num = 0;
            while (num++ < num2)
            {
                if (this.MyPort.BytesToRead != 0)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            Thread.Sleep(50);
            bytesToRead = this.MyPort.BytesToRead;
            if (bytesToRead < 12)
            {
                return null;
            }
            buffer = new byte[bytesToRead];
            this.MyPort.Read(buffer, 0, bytesToRead);
            CommonUtils.WriteMootrInfo("Read:" + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
            return buffer;
        }

        public bool SetCardLengthOperate(int cardLength, int motorNumber)
        {
            try
            {
                byte[] setCardLength = this.GetSetCardLength(motorNumber, cardLength);
                byte[] buffer2 = this.SendCommand(setCardLength, motorNumber);
                if ((buffer2 == null) || (buffer2.Length < 1))
                {
                    return false;
                }
                return (buffer2[2] == 0x55);
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }
        }
    }
}
