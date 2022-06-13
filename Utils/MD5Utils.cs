using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace YTDSSTGenII.Utils
{
    public class MD5Utils
    {
        public static String Md5(String content)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Default.GetBytes(content);
            byte[] targetData = md5.ComputeHash(fromData);

            StringBuilder sb = new StringBuilder();
			for (int i= 0; i < targetData.Length ; i++) 
            {
				int bt = targetData[i] & 0xff;
				if (bt < 16) {
					sb.Append("0");
				}
                

				sb.Append(bt.ToString("X"));
			}

			return sb.ToString();
            /*
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("X2");
            }
             * return byte2String;
             */    
        }
    }
}
