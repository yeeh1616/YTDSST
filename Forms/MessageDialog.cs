using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YTDSSTGenII.Forms
{
    public partial class MessageDialog : Form
    {
        private Boolean selected;

        public MessageDialog()
        {
            InitializeComponent();
        }

        public void setMessage(String msg) {
            lblMessage.Text = msg;
        }

        private void lblYes_Click(object sender, EventArgs e)
        {
            selected = true;
            base.Close();
        }

        private void lblNo_Click(object sender, EventArgs e)
        {
            selected = false;
            base.Close();
        }

        public bool Result {
            get {
                return selected;
            }
        }
    }
}
