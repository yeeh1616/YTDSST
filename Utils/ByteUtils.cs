using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Author: bean.
 */ 
namespace YTDSSTGenII.Utils
{
    public class ByteUtils
    {
        /**
         * 字节拷贝
         */ 
        public static byte[] CopyBytes(byte[] bytes, int start, int size) {
            if(bytes == null)
                return null;

            if(start <0 || size > bytes.Length)
                return null;

            byte[] newBytes = new byte[size];

            for (int i = 0; i < size; i++) {
                if ((start + i) >= bytes.Length)
                {
                    break;
                }

                newBytes[i] = bytes[start + i];
            }

            return newBytes;
        }

        public static int ByteToInt(byte[] bytes) {
            if (bytes.Length != 4)
                throw new ArgumentException("字节数组长度不对.");

            //TODO 考虑字节序的问题
            return  (int)(bytes[0] | bytes[1] << 8 | bytes[2] << 16 | bytes[3] << 24);
            //return System.BitConverter.ToInt32(bytes, 0);
        }

        public static byte[] IntToByte(int param) {
            //TODO 考虑字节序的问题
            byte[] bytes = new byte[4];

            bytes[0] = (byte)(param);
            bytes[1] = (byte)(param >> 8);
            bytes[2] = (byte)(param >> 16);
            bytes[3] = (byte)(param >> 24);

            return bytes;
        }

        public static byte[] ShortToByte(short param)
        {
            byte[] bytes = new byte[2];

            bytes[0] = (byte)(param);
            bytes[1] = (byte)(param >> 8);

            return bytes;
        }

        public static short ByteToShort(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new ArgumentException("参数长度错误.");

            return (short)(bytes[1] << 8 & 0xFF00 | bytes[0] & 0xFF);
        }

        public static String ByteToString(byte[] bytes) {
            return Encoding.GetEncoding("UTF-8").GetString(bytes);
        }

        public static byte[] StringToByte(String param)
        {
            return Encoding.GetEncoding("UTF-8").GetBytes(param);
        }

        public static void WirteByteToBuffer(byte[] source, int sourceStartIndex, int length, byte[] destination, int destinationStartIndex)
        {
            if (source == null || destination == null)
                throw new ArgumentException("参数错误，参数为空");

            if ((destination.Length - destinationStartIndex) < length)
            {
                throw new ArgumentException("参数错误,数组越界");
            }

            for (int i = 0; i < length; i++) {
                if ((destinationStartIndex + i) >= destination.Length)
                {
                    throw new ArgumentException("参数错误，目标数组越界");
                }

                if ((destinationStartIndex + i) > source.Length)
                {
                    throw new ArgumentException("参数错误，源数组越界");
                }

                destination[destinationStartIndex + i] = source[sourceStartIndex + i];
            }
            
        }
    }
}
