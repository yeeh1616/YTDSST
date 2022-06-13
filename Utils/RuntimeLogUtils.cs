using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace YTDSSTGenII.Utils
{
    public class RuntimeLogUtils
    {
        private static string logFileName = "runtime_{0}.log";

        public static void WriteLog(String log)
        {
            String dir = Application.StartupPath + "\\runtime";
            
            String fullLogFileName = Application.StartupPath + "\\runtime\\" + logFileName;
            fullLogFileName = String.Format(fullLogFileName, DateTime.Now.ToString("yyyyMMdd"));

            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                if (!System.IO.File.Exists(fullLogFileName))
                {
                    System.IO.File.CreateText(fullLogFileName).Close();
                }

                StreamWriter writer2 = System.IO.File.AppendText(fullLogFileName);

                writer2.WriteLine("[" + DateTime.Now.ToString() + "]" +  log);

                writer2.Close();
            }
            catch
            {}
        }

        public static String ReadLog(String fileName)
        {
            string logs = "";
            try
            {
                if (fileName == null)
                {
                    String fullLogFileName = Application.StartupPath + "\\runtime\\" + logFileName;
                    fileName = String.Format(fullLogFileName, DateTime.Now.ToString("yyyyMMdd"));
                }
                FileStream fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                StreamReader sr = new StreamReader(fs);
                logs = sr.ReadToEnd();

                sr.Close();
                fs.Close();
            }
            catch (Exception exp)
            {
                logs = "日志读取异常:" + exp.Message;
            }

            return logs;
        }
    }
}
