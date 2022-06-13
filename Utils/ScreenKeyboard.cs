using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YTDSSTGenII.Utils
{
    class ScreenKeyboard
    {
        private const uint SWP_HIDEWINDOW = 0x80;
        private const uint SWP_NOACTIVATE = 0x10;
        private const uint SWP_NOCOPYBITS = 0x100;
        private const uint SWP_NOMOVE = 2;
        private const uint SWP_NOOWNERZORDER = 0x200;
        private const uint SWP_NOREDRAW = 8;
        private const uint SWP_NOSENDCHANGING = 0x400;
        private const uint SWP_NOSIZE = 1;
        private const uint SWP_NOZORDER = 4;
        private const uint SWP_SHOWWINDOW = 0x40;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        
        public static void ShowScreenKeyboard()
        {
        }
    }
}
