using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Service.Exception;

namespace YTDSSTGenII.Forms
{
    public partial class FormMotorList : Form
    {
        private IniFile ini;

        public FormMotorList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.DGV.SelectedRows.Count == 1)
            {
                FormAddLottery.MotorNumber = int.Parse(this.DGV.SelectedRows[0].Cells["机头号"].Value.ToString());
                FormAddLottery.LotteryCount = int.Parse(this.DGV.SelectedRows[0].Cells["余票"].Value.ToString());
                FormAddLottery.lName = this.DGV.SelectedRows[0].Cells["名称"].Value.ToString();

                new FormAddLottery().ShowDialog();
                this.BindDGV();
            }
            
        }

        private void FormMotorList_Load(object sender, EventArgs e)
        {
            this.ini = new IniFile("D://advitise//1.ini");
            this.timer1.Enabled = true;
        }


        private void BindDGV()
        {
            string sql = "SELECT MotorId as 机头号, LotteryName AS 名称,case LotteryLength WHEN 51 then '2英寸' WHEN 102 then '4英寸' WHEN 153 then '6英寸' WHEN 203 then '8英寸' WHEN 255 then '10英寸' end AS 票长,LotteryPrice AS 价格,LotteryCount AS 余票 FROM tblMotorInfo where motorId<9";
            this.DGV.DataSource = SQLiteHelper.Query(sql).Tables[0];
            int num = 0;
            for (int i = 0; i < this.DGV.Columns.Count; i++)
            {
                this.DGV.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                num += this.DGV.Columns[i].Width;
            }
            if (num > this.DGV.Size.Width)
            {
                this.DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                this.DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void DGV_DoubleClick(object sender, EventArgs e)
        {
            if (this.DGV.SelectedRows.Count == 1)
            {
                FormAddLottery.MotorNumber = int.Parse(this.DGV.SelectedRows[0].Cells["机头号"].Value.ToString());
                FormAddLottery.LotteryCount = int.Parse(this.DGV.SelectedRows[0].Cells["余票"].Value.ToString());
                FormAddLottery.lName = this.DGV.SelectedRows[0].Cells["名称"].Value.ToString();
                new FormAddLottery().ShowDialog();
                this.BindDGV();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.BindDGV();
        }

        private void FormMotorList_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalParmeters.isLoadDataAgin = true;
            base.Enabled = false;
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == "ScreenKey")
                {
                    processes[i].Kill();
                }
            }
            GlobalParmeters.IsOpenScreenKey = false;
        }
    }
}
