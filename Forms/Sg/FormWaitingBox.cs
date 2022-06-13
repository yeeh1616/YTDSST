using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace YTDSSTGenII.Forms.Sg
{
    public partial class FormWaitingBox : Form
    {
        private IAsyncResult _AsyncResult;
        private bool _CancelEnable;
        private readonly int _EffectCount;
        private readonly int _EffectTime;
        private bool _IsShown;
        private int _MaxWaitTime;
        private EventHandler<EventArgs> _Method;
        private int _WaitTime;
        private Timer _Timer;

        public FormWaitingBox(EventHandler<EventArgs> method)
        {
            this._IsShown = true;
            
            this._EffectCount = 10;
            this._EffectTime = 500;
            
            int maxWaitTime = 0xea60;
            
            string waitMessage = "正在处理数据，请稍后...";
            
            bool cancelEnable = true;
            bool timerVisable = true;

            this.Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        public FormWaitingBox(EventHandler<EventArgs> method, string waitMessage)
        {
            this._IsShown = true;
            this._EffectCount = 10;
            
            this._EffectTime = 500;
            int maxWaitTime = 0xea60;
            
            bool cancelEnable = true;
            bool timerVisable = true;

            this.Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        public FormWaitingBox(EventHandler<EventArgs> method, bool cancelEnable, bool timerVisable)
        {
            this._IsShown = true;
            
            this._EffectCount = 10;
            this._EffectTime = 500;
            int maxWaitTime = 0xea60;
            
            string waitMessage = "正在处理数据，请稍后...";

            this.Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        public FormWaitingBox(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            this._IsShown = true;
            
            this._EffectCount = 10;
            this._EffectTime = 500;
            
            maxWaitTime *= 0x3e8;

            this.Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        private void Initialize(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            this.InitializeComponent();
            base.FormBorderStyle = FormBorderStyle.None;
            base.StartPosition = FormStartPosition.CenterParent;
            base.ShowInTaskbar = false;
            Color[] randColor = this.GetRandColor();

            this.panel1.BackColor = randColor[0];

            this.BackColor = randColor[1];
            this.labMessage.Text = waitMessage;
            this._Timer = new Timer();
            this._Timer.Interval = this._EffectTime / this._EffectCount;
            this._Timer.Tick += new EventHandler(this._Timer_Tick);
            base.Opacity = 0.0;
            this.FormEffectEnable = true;
            this.TimeSpan = 500;
            this.Message = string.Empty;
            this._CancelEnable = cancelEnable;
            this._MaxWaitTime = maxWaitTime;
            this._WaitTime = 0;
            this._Method = method;

            this.btnCancel.Visible = this._CancelEnable;

            this.labTimer.Visible = timerVisable;
            this.timer1.Interval = this.TimeSpan;

            this.timer1.Start();
        }

        private void FormWaitingBox_Load(object sender, EventArgs e)
        {

        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            if (this._IsShown)
            {
                if (base.Opacity >= 1.0)
                {
                    this._Timer.Stop();
                    this._IsShown = false;
                }
                base.Opacity += 1.0 / ((double)this._EffectCount);
            }
            else
            {
                if (base.Opacity <= 0.0)
                {
                    this._Timer.Stop();
                    this._IsShown = true;
                    base.Close();
                }
                base.Opacity -= 1.0 / ((double)this._EffectCount);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Message = "您结束了当前操作！";
            base.Close();
        }

        private void frmWaitingBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.FormEffectEnable)
            {
                if (base.Opacity >= 1.0)
                {
                    e.Cancel = true;
                }
                this._Timer.Start();
            }
        }

        private void frmWaitingBox_Shown(object sender, EventArgs e)
        {
            this._AsyncResult = this._Method.BeginInvoke(null, null, null, null);
            if (this.FormEffectEnable)
            {
                this._Timer.Start();
            }
            else
            {
                base.Opacity = 1.0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this._WaitTime += this.TimeSpan;
            this.labTimer.Text = this._WaitTime / 0x3e8 + "秒";

            if (!this._AsyncResult.IsCompleted)
            {
                if (this._WaitTime > this._MaxWaitTime)
                {
                    this.Message = "处理数据超时" + this._MaxWaitTime / 0x3e8 + "秒，结束当前操作！";
                    base.Close();
                }
            }
            else
            {
                this.Message = string.Empty;
                base.Close();
            }
        }

        private Color[] GetRandColor()
        {
            int maxValue = 0xf8;
            int minValue = 0xcc;
            int num3 = 250;
            int num4 = 0xd7;
            int num5 = 250;
            int num6 = 240;
            Random random = new Random(DateTime.Now.Millisecond);
            int red = random.Next(minValue, maxValue);
            int num8 = red + 5;
            int green = random.Next(num4, num3);
            int num10 = green + 5;
            int blue = random.Next(num6, num5);
            int num12 = blue + 5;
            Color color = Color.FromArgb(red, green, blue);
            Color color2 = Color.FromArgb(num8, num10, num12);
            return new Color[] { color, color2 };
        }

        public bool FormEffectEnable { get; set; }

        public string Message { get; private set; }

        public int TimeSpan { get; set; }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
