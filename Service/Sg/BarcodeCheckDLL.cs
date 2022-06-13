using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace YTDSSTGenII.Service.Sg
{
    public static class BarcodeCheckDLL
    {
        // Methods
        [DllImport("BarcodeCheck.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int BarcodeCheck(string s);
    }
}
