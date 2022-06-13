using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace YTDSSTGenII.Forms
{
    public partial class FormMaskLayer : Form
    {
        public FormMaskLayer()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")] 
        public static extern long GetWindowLong(IntPtr hwnd, int nIndex); 
  
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")] 
        public static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong); 
  
        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")] 
        private static extern int SetLayeredWindowAttributes(IntPtr Handle, int crKey, byte bAlpha, int dwFlags); 
  
        const int GWL_EXSTYLE = -20; 
        const int WS_EX_TRANSPARENT = 0x20; 
        const int WS_EX_LAYERED = 0x80000; 
        const int LWA_ALPHA = 0x2; 
        const int  LWA_COLORKEY = 0x1;
  
        
        private void Form1_Load(object sender, EventArgs e) 
        {   
            this.BackColor = Color.White; 
            //this.TopMost = true; 
            
            this.FormBorderStyle = FormBorderStyle.None; 
            this.WindowState = FormWindowState.Maximized; 

            //设置WS_EX_TRANSPARENT会造成鼠标穿透
           //SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED);
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE)  | WS_EX_LAYERED);
            SetLayeredWindowAttributes(Handle, 0, 100, LWA_ALPHA | LWA_COLORKEY); 
        } 
    }
}



