using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YTDSSTGenII.Utils
{
    class Regexlib
    {
        public static bool IsHandset(string strIn) {
            return Regex.IsMatch(strIn, @"^[1]+[3,5,8][0-9]+\d{8}");
        }
        
        public static bool IsNumber(string str_number) 
        {
            return Regex.IsMatch(str_number, "^[0-9]*$");
        } 
        
        public static bool IsPostalcode(string str_postalcode)
        {
           return Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }
            

        public static bool IsTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        public static bool IsValidIp(string strIn)
        {
            return Regex.IsMatch(strIn, @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
        }

        public static bool IsValidTel(string strIn)
        {
            return Regex.IsMatch(strIn, @"^(\d{3,4}-)?\d{6,8}$");
        }
    }
}
