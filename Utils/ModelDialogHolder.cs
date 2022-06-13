using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YTDSSTGenII.Forms;

namespace YTDSSTGenII.Utils
{
    public class ModelDialogHolder
    {
        private FormMaskLayer maskLayer;
        private MessageDialog dialog;

        private String msg;

        public static ModelDialogHolder newHolder(String msg) {
            return new ModelDialogHolder(msg);
        }

        public ModelDialogHolder(String msg) {
            this.msg = msg;
        }

        public  void ShowDialog() {
            if (maskLayer == null) {
                maskLayer = new FormMaskLayer();
            }
            maskLayer.Show();

            dialog = new MessageDialog();
            dialog.setMessage(msg);
            dialog.ShowDialog();
            maskLayer.Hide();
        }

        public void closeDialog() {
            if (dialog != null && !dialog.IsDisposed) {
                dialog.Close();
            }
        }

        public Boolean Result {
            get {
                if (dialog != null)
                    return dialog.Result;
                else
                    return false;
            }
        }
    }
}
