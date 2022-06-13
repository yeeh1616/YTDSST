using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Threading;

using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms
{
    public partial class FormUnzipToTerminal : Form
    {
        private string Attributes = "";
        public const uint CREATE_ALWAYS = 2;
        public const uint CREATE_NEW = 1;
        private string DriveName = "";
        private string Extension = "";
        public const short FILE_ATTRIBUTE_NORMAL = 0x80;
        public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
        public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        public const uint FILE_SHARE_READ = 1;
        public const uint FILE_SHARE_WRITE = 2;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        private IniFile ini;
        public const short INVALID_HANDLE_VALUE = -1;
        private string LastWriteTime = "";
        private string Length = "";
        public const uint OPEN_EXISTING = 3;
        private string path = "";

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public FormUnzipToTerminal()
        {
            InitializeComponent();
        }

        private void FormUnzipToTerminal_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnUnzip_Click(object sender, EventArgs e)
        {
            this.btnUnzip.Enabled = false;
            this.progressBar1.Maximum = 0;
            this.progressBar1.Value = 0;
            this.label1.Text = "0%";
            GlobalParmeters.isUnzip = true;
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                if (info.DriveType == DriveType.Removable)
                {
                    this.DriveName = info.Name;
                    break;
                }
            }
            if (this.DriveName == "")
            {
                this.btnUnzip.Enabled = true;
                MessageBox.Show("对不起,未检测到U盘", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (!Directory.Exists(this.DriveName + "adv"))
            {
                this.btnUnzip.Enabled = true;
                MessageBox.Show("对不起,U盘中未找到有效文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if ((!File.Exists(this.DriveName + @"adv\adv.ini") || !File.Exists(this.DriveName + @"\adv\advertisementParameters.xml")) || !File.Exists(this.DriveName + @"\adv\advertisement.zip"))
            {
                this.btnUnzip.Enabled = true;
                MessageBox.Show("对不起,U盘文件已损坏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                this.ini = new IniFile(this.DriveName + @"adv\adv.ini");
                this.lblMessage.Text = "正在解压中，请稍候...";
                this.Attributes = this.ini.ReadIniValue("config", "Attributes");
                this.LastWriteTime = this.ini.ReadIniValue("config", "LastWriteTime");
                DateTime time = Convert.ToDateTime(this.LastWriteTime);
                this.Length = this.ini.ReadIniValue("config", "Length");
                this.Extension = this.ini.ReadIniValue("config", "Extension");
                this.path = this.ini.ReadIniValue("config", "path");
                FileInfo info2 = new FileInfo(this.DriveName + @"adv\advertisement.zip");
                DateTime lastWriteTime = info2.LastWriteTime;
                int num = Math.Abs((int)((((time.Minute * 60) + time.Second) - (lastWriteTime.Minute * 60)) - lastWriteTime.Second));
                if (((this.Attributes != info2.Attributes.ToString()) || (num > 2)) || ((this.Length != info2.Length.ToString()) || (this.Extension != info2.Extension.ToString())))
                {
                    this.btnUnzip.Enabled = true;
                    MessageBox.Show("对不起,文件有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        if (processes[i].ProcessName == "WinVideoPlayByAPlater")
                        {
                            processes[i].Kill();
                        }
                    }
                    new Thread(new ThreadStart(this.checkSoctet)).Start();
                }
            }
        }

        private void checkSoctet()
        {
            fileCopy(this.DriveName + @"adv\advertisementParameters.xml", @"D:\advitise\advertisementParameters.xml");
            string[] strArray = new string[] { this.DriveName + @"\adv\advertisement.zip", this.path };
            this.unZipFile(strArray[0], strArray[1]);
            this.label1.Text = "100%";
            this.lblMessage.Text = "解压已完成，请拔出U盘！";
            GlobalParmeters.isUnzip = false;
            this.btnUnzip.Enabled = true;
        }

        public static string encrypt(string str)
        {
            string s = str;
            string str3 = "";
            byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(s));
            for (int i = 0; i < buffer.Length; i++)
            {
                str3 = str3 + buffer[i].ToString("X");
            }
            return str3;
        }

        private static void fileCopy(string come, string to)
        {
            bool flag = true;
            SafeFileHandle handle = CreateFile(come, (uint)0x80000000, (uint)1, IntPtr.Zero, (uint)3, (uint)(flag ? 0:0x20000000), IntPtr.Zero);
            SafeFileHandle handle2 = CreateFile(to, 0x40000000, 1, IntPtr.Zero, 2, flag ? 0 : 0xa0000000, IntPtr.Zero);
            int count = flag ? 0x2000000 : 0x2000000;
            FileStream input = new FileStream(handle, FileAccess.Read);
            FileStream output = new FileStream(handle2, FileAccess.Write);
            BinaryReader reader = new BinaryReader(input);
            BinaryWriter writer = new BinaryWriter(output);
            byte[] buffer = new byte[count];
            long length = input.Length;
            DateTime now = DateTime.Now;
            while (input.Position < input.Length)
            {
                int num3 = reader.Read(buffer, 0, count);
                writer.Write(buffer, 0, num3);
                TimeSpan span = DateTime.Now.Subtract(now);
                double num1 = ((((double)input.Position) / span.TotalMilliseconds) * 1000.0) / 1048576.0;
                double num4 = ((double)input.Position) / ((double)length);
            }
            reader.Close();
            writer.Close();
        }

        private void FormUnzipToTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
        }

        public string unZipFile(string TargetFile, string fileDir)
        {
            string str = " ";
            try
            {
                ZipEntry entry;
                ZipEntry entry2;
                ZipInputStream stream = new ZipInputStream(File.OpenRead(TargetFile.Trim()))
                {
                    Password = encrypt("123456")
                };
                while ((entry = stream.GetNextEntry()) != null)
                {
                    this.progressBar1.Maximum += (int)entry.Size;
                }
                stream.Close();
                ZipInputStream stream2 = new ZipInputStream(File.OpenRead(TargetFile.Trim()))
                {
                    Password = encrypt("123456")
                };
                string path = fileDir;
                string directoryName = " ";
                while ((entry2 = stream2.GetNextEntry()) != null)
                {
                    directoryName = Path.GetDirectoryName(entry2.Name);
                    if (directoryName.IndexOf(@"\") >= 0)
                    {
                        directoryName = directoryName.Substring(0, directoryName.IndexOf(@"\") + 1);
                    }
                    string str4 = Path.GetDirectoryName(entry2.Name);
                    string fileName = Path.GetFileName(entry2.Name);
                    if (str4 != " ")
                    {
                        if (!Directory.Exists(fileDir + @"\" + str4))
                        {
                            path = fileDir + @"\" + str4;
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if ((str4 == " ") && (fileName != ""))
                    {
                        path = fileDir;
                        str = fileName;
                    }
                    else if (((str4 != " ") && (fileName != "")) && (str4.IndexOf(@"\") > 0))
                    {
                        path = fileDir + @"\" + str4;
                    }
                    if (str4 == directoryName)
                    {
                        path = fileDir + @"\" + directoryName;
                    }
                    if (fileName != string.Empty)
                    {
                        FileStream stream3 = File.Create(path + @"\" + fileName);
                        int count = 0x800;
                        byte[] buffer = new byte[0x800];
                        while (true)
                        {
                            count = stream2.Read(buffer, 0, buffer.Length);
                            if (count <= 0)
                            {
                                break;
                            }
                            stream3.Write(buffer, 0, count);
                            try
                            {
                                this.progressBar1.Value += count;
                            }
                            catch
                            {
                                this.progressBar1.Value = this.progressBar1.Maximum;
                            }
                            this.label1.Text = (((100L * this.progressBar1.Value) / (this.progressBar1.Maximum + 100L))).ToString() + "%";
                        }
                        stream3.Close();
                    }
                }
                stream2.Close();
                return str;
            }
            catch (Exception exception)
            {
                return ("1; " + exception.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = encrypt("123456");
        }
    }
}
