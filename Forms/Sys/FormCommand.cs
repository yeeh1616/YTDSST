using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace YTDSSTGenII.Forms.Sys
{
    public partial class FormCommand : Form
    {

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowA(string lp1, string lp2);
        [DllImport("user32.dll")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int _value);

        public FormCommand()
        {
            InitializeComponent();
        }

        private void FormCommand_Load(object sender, EventArgs e)
        {

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe", "-r -t 0");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe", "-s -t 0");
        }

        private void timerCheckProcess_Tick(object sender, EventArgs e)
        {
            resetButtonState();
        }

        private void resetButtonState() {
            //
            Process[] proc = Process.GetProcessesByName("WinVideoPlayByAPlater");
            if (proc.Length == 0)
            {
                lblAdvertise.Text = "广告程序未启动";
                lblAdvertise.ForeColor = Color.Red;
                btnAdvertise.Text = "启动广告程序";
            }
            else {
                lblAdvertise.Text = "广告程序已启动";
                lblAdvertise.ForeColor = Color.Black;
                btnAdvertise.Text = "关闭广告程序";
            }

            proc = Process.GetProcessesByName("WinPowerContorlForUPS");
            if (proc.Length == 0)
            {
                lblPowerControl.Text = "电控程序未启动";
                lblPowerControl.ForeColor = Color.Red;
                btnPowerControl.Text = "启动电控程序";
            }
            else
            {
                lblPowerControl.Text = "电控程序已启动";
                lblPowerControl.ForeColor = Color.Black;
                btnPowerControl.Text = "关闭电控程序";
            }

            proc = Process.GetProcessesByName("UpLoadBySocket");
            if (proc.Length == 0)
            {
                lblDataUpload.Text = "数据上传程序未启动";
                lblDataUpload.ForeColor = Color.Red;
                btnDataUpload.Text = "启动数据上传";
            }
            else
            {
                lblDataUpload.Text = "数据上传程序已启动";
                lblDataUpload.ForeColor = Color.Black;
                btnDataUpload.Text = "关闭数据上传";
            }
        }

        private void btnMainExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnPowerControl_Click(object sender, EventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("WinPowerContorlForUPS");
            if (proc.Length == 0)
            {
                String FileName = @"C:\Program Files\CashVersion\PowerControlByUPS\WinPowerContorlForUPS.exe";
                if(System.IO.File.Exists(FileName))
                {
                    ProcessStartInfo info = new ProcessStartInfo(FileName);
                    new Process { StartInfo = info }.Start();
                }
            }
            else {
                //关闭电控
                proc[0].Kill();
            }

        }

        private void btnDataUpload_Click(object sender, EventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("UpLoadBySocket");
            if (proc.Length == 0)
            {
                String FileName = @"C:\Program Files\CashVersion\数据上传\UpLoadBySocket.exe";
                if(System.IO.File.Exists(FileName))
                {
                    ProcessStartInfo info = new ProcessStartInfo(FileName);
                    new Process { StartInfo = info }.Start();
                }
            }
            else
            {
                //关闭电控
                proc[0].Kill();
            }
        }

        private void btnAdvertise_Click(object sender, EventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("WinVideoPlayByAPlater");
            if (proc.Length == 0)
            {
                String FileName = @"C:\Program Files\CashVersion\aplayer\WinVideoPlayByAPlater.exe";
                if(System.IO.File.Exists(FileName))
                {
                    ProcessStartInfo info = new ProcessStartInfo(FileName);
                    new Process { StartInfo = info }.Start();
                }
            }
            else
            {
                //关闭电控
                proc[0].Kill();
            }
        }

        private void btnTaskbar_Click(object sender, EventArgs e)
        {
            IntPtr hTray = FindWindowA("Shell_TrayWnd", string.Empty);
            if (hTray != null)
            {
                //显示
                ShowWindow(hTray, 5);
               MessageBox.Show("任务栏显示成功", "提示");
            }
        }

        private void btnHideTaskbar_Click(object sender, EventArgs e)
        {
            IntPtr hTray = FindWindowA("Shell_TrayWnd", string.Empty);
            if (hTray != null)
            {
                //显示
                ShowWindow(hTray, 0);
                MessageBox.Show("任务栏隐藏成功", "提示");
            }
        }
    }
}
