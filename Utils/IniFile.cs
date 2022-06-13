using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Utils
{
    public class IniFile
    {
        public string filePath;

        public IniFile(string iniPath)
        {
            this.filePath = iniPath;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        private void GetStringsFromBuffer(byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int index = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - index) > 0))
                    {
                        string str = Encoding.GetEncoding(0).GetString(Buffer, index, i - index);
                        Strings.Add(str);
                        index = i + 1;
                    }
                }
            }
        }

        public string ReadIniValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.filePath);
            return retVal.ToString();
        }

        public void ReadSection(string Section, StringCollection Idents)
        {
            byte[] retVal = new byte[0x4000];
            int bufLen = GetPrivateProfileString(Section, null, null, retVal, retVal.GetUpperBound(0), this.filePath);
            this.GetStringsFromBuffer(retVal, bufLen, Idents);
        }

        public bool ValueExists(string Section, string Ident)
        {
            StringCollection idents = new StringCollection();
            this.ReadSection(Section, idents);
            return (idents.IndexOf(Ident) > -1);
        }

        public void WriteIniValue(string Section, string Key, string value)
        {
            WritePrivateProfileString(Section, Key, value, this.filePath);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}
