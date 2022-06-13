using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using Shell32;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Forms.Properties;

namespace YTDSSTGenII.Forms
{
    public partial class FormNetworkSpeed : Form
    {

        private System.Threading.Timer testTimer;
        public FormNetworkSpeed()
        {
            InitializeComponent();

            testTimer = new System.Threading.Timer(new TimerCallback(timerCall), this, Timeout.Infinite, Timeout.Infinite);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.timer1.Enabled = true;
            this.label1.Text = "正在测试，请稍候...";
            testTimer.Change(0, Timeout.Infinite);
        }

        private void timerCall(object obj)
        {
            int result = 0;
            try
            {
                SocketClient client = new SocketClient();
                client.OpenSocket();
                byte[] bytes = Encoding.ASCII.GetBytes("super");
                byte[] data = new byte[] { 10, 13 };
                client.SendData(bytes);
                client.SendData(data);
                byte[] buffer3 = Encoding.ASCII.GetBytes("enable");
                client.SendData(buffer3);
                client.SendData(data);
                byte[] buffer4 = Encoding.ASCII.GetBytes("show modem-information");
                client.SendData(buffer4);
                client.SendData(data);
                Thread.Sleep(400);
                byte[] buffer5 = client.ReadData();

                if ((buffer5 == null) || (buffer5.Length < 1))
                {
                    result = 0;
                }
                else
                {
                    string str = Encoding.ASCII.GetString(buffer5);
                    int index = str.IndexOf("Modem Signal\t\t:\t");
                    if (index < 0)
                    {
                        result = 0;
                    }
                    else if (str.Length < (index + 0x12))
                    {
                        result = 0;
                    }
                    else if (!int.TryParse(str.Substring(index + 0x10, 2), out result))
                    {
                        result = 0;
                    }
                }
                client.CloseSocket();
            }
            catch (Exception)
            {
                result = 0;
            }
            finally
            {
                if (result <= 0)
                {
                    this.pictureBox1.BackgroundImage = Resources.wifi_0;
                }
                else if ((result > 0) && (result <= 6))
                {
                    this.pictureBox1.BackgroundImage = Resources.wifi_1;
                }
                else if ((result > 6) && (result <= 12))
                {
                    this.pictureBox1.BackgroundImage = Resources.wifi_2;
                }
                else if ((result > 12) && (result <= 0x12))
                {
                    this.pictureBox1.BackgroundImage = Resources.wifi_3;
                }
                else if ((result > 0x12) && (result <= 0x18))
                {
                    this.pictureBox1.BackgroundImage = Resources.wifi_4;
                }
                else if (result > 0x18)
                {
                    this.pictureBox1.BackgroundImage = Resources.wifi_5;
                }
            }

            if (this.label1.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate {
                    this.label1.Text = "测试完成";
                    this.button1.Text = "重新测试";
                }));
            }
            else
            {
                this.label1.Text = "测试完成";
                this.button1.Text = "重新测试";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        private static bool NetWork(string netWorkName, string operation)
        {
            try
            {
                bool flag = false;
                Shell shell = new ShellClass();
                foreach (FolderItem item in shell.NameSpace(0x31).Items())
                {
                    if (item.Name == netWorkName)
                    {
                        ShellFolderItem item2 = (ShellFolderItem)item;
                        foreach (FolderItemVerb verb in item2.Verbs())
                        {
                            if (verb.Name.Contains(operation))
                            {
                                flag = true;
                                verb.DoIt();
                                Thread.Sleep(0x3e8);
                                break;
                            }
                        }
                    }
                }
                return flag;
            }
            catch (Exception exception)
            {
                SocketClient.WriteExceptionInfo(exception);
                return false;
            }
        }
    }
}
