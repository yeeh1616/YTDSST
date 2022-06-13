using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YTDSSTGenII.Utils
{
    class MouseKeyBoardOperate
    {
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        public static long GetLastInputTime()
        {
            LASTINPUTINFO structure = new LASTINPUTINFO();
            structure.cbSize = Marshal.SizeOf(structure);
            if (!GetLastInputInfo(ref structure))
            {
                return 0L;
            }
            long num = Environment.TickCount - structure.dwTime;

            return (num / 1000L);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
    }
}
