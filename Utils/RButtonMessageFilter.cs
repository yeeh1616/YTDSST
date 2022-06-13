using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YTDSSTGenII.Utils
{
    class RButtonMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x204:
                case 0x205:
                case 0x206:
                    MessageBox.Show("右键被拦截！");
                    return true;
            }
            return false;
        }
    }
}
