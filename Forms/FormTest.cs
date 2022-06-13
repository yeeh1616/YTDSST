using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           ModelDialogHolder holder =  ModelDialogHolder.newHolder("测试消息");

           holder.ShowDialog();

            Console.WriteLine("Test Over, Selected :" + holder.Result);
        }
    }
}
