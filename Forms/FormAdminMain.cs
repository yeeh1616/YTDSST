using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Runtime;
using System.Diagnostics;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Service;

using YTDSSTGenII.Forms.Sg;
using YTDSSTGenII.Forms.Sys;
using PointingDevice;

namespace YTDSSTGenII.Forms
{
    public partial class FormAdminMain : Form
    {
        private int timeOutCount;
        private IniFile ini;

        //收钞明细
        private FormReceCash receCash;
        //日志拷贝
        private FormCopyLog copyLog;
        //终端信息
        private FormTerminalInfo terminal;
        //退币报表
        private FormPayoutReport payout;
        //参数设置
        private FormConfig config;
        //出票明细
        private FormCutPaperReport cutPaper;
        //兑奖明细
        private FormLotteryPrize lotteryPrize;
        //机头信息
        private FormMotorList motorList;
        //加票汇总
        private FormAddLotteryCount AddCount;
        //加票明细
        private FormAddLotteryDetailReport addLottDetail;
        //清钞明细
        private FormCleanCashTime CleanCash;
        //解压广告
        private FormUnzipToTerminal frmUnzipToTerminal;
        //清空明细（清空报表）
        private FormEmptyCashReport empty;
        //取钞管理
        private FormGetCash frmGetCash;
        //网络信号测试
        private FormNetworkSpeed netWork;
        //现金预存
        private FormCashDeposit cashDeposit;
        //销售报表
        private FormTradeReport trade;
        //销售明细
        private FormTradeDetailReport tradeDetail;
        //修改密码
        private FormChangePwd password;
        //预存报表
        private FormCashDepositReport depositReport;

        //兑奖
        private FormSGBind frmBind;
        private FormSGChangePassword frmChangePassword;
        private FormSGFactoryTest frmFactoryTest;
        private FormSGSet frmSet;

        private FormMingMaPrize frmPrize;


        public FormAdminMain()
        {
            InitializeComponent();
        }

        private void timerMouseKey_Tick(object sender, EventArgs e)
        {
            try {
                long lastInputTime = MouseKeyBoardOperate.GetLastInputTime();
                if(!GlobalParmeters.isUnzip && (lastInputTime >= this.timeOutCount)) {
                    lastInputTime = 0L;
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        if (processes[i].ProcessName == "ScreenKey")
                        {
                            processes[i].Kill();
                        }
                    }
                    base.Close();
                    base.Dispose();
                }
            } catch(Exception exception) {
                CommonUtils.WriteExceptionInfo(exception);
            }

        }

        private void 清钞管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.CleanCash == null) || !this.CleanCash.Enabled)
            {
                this.CleanCash = new FormCleanCashTime();
                this.CleanCash.MdiParent = this;
                this.CleanCash.WindowState = FormWindowState.Maximized;
                this.CleanCash.Show();
            }
            this.CleanCash.BringToFront();
        }

        private void 收钞管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((this.receCash == null) || !this.receCash.Enabled)
            {
                this.receCash = new FormReceCash();
                this.receCash.MdiParent = this;
                this.receCash.WindowState = FormWindowState.Maximized;
                this.receCash.Show();
            }
            this.receCash.BringToFront();
        }

        private void FormAdminMain_Load(object sender, EventArgs e)
        {
            this.ini = new IniFile("D://advitise//1.ini");
            this.timeOutCount = int.Parse(this.ini.ReadIniValue("配置", "超时间隔"));
            this.Size = new Size(768, 884);
            this.StartPosition = FormStartPosition.CenterScreen;
            try
            {
                if ((FormLogin.UserName == "admin") || (FormLogin.UserName == "bjfulei"))
                {
                    this.用户管理ToolStripMenuItem.Visible = false;
                }
                if (Convert.ToInt32(SQLiteHelper.GetSingle("SELECT Role FROM Users WHERE name='" + FormLogin.UserName + "'")) == 1)
                {
                    this.票务管理ToolStripMenuItem.Visible = false;
                    this.收币器管理ToolStripMenuItem.Visible = false;
                    this.找币器管理ToolStripMenuItem.Visible = false;
                    this.本地报表ToolStripMenuItem.Visible = false;
                    this.销售报表ToolStripMenuItem.Visible = false;
                    
                    this.网络信号测试ToolStripMenuItem.Visible = false;
                    this.当期结存ToolStripMenuItem.Visible = false;
                    this.解压到终端ToolStripMenuItem.Visible = false;

                    if ((this.copyLog == null) || !this.copyLog.Enabled)
                    {
                        this.copyLog = new FormCopyLog();
                        this.copyLog.MdiParent = this;
                        this.copyLog.WindowState = FormWindowState.Maximized;
                        this.copyLog.Show();
                    }
                }
                else
                {
                    this.退出购彩程序ToolStripMenuItem.Visible = false;
                    this.参数配置ToolStripMenuItem1.Visible = false;
                    this.日志拷贝ToolStripMenuItem.Visible = false;
                    
                    this.兑奖绑定ToolStripMenuItem.Visible = false;
                    this.兑奖设置ToolStripMenuItem.Visible = false;
                    this.兑奖密码设置ToolStripMenuItem.Visible = false;

                    if ((this.terminal == null) || !this.terminal.Enabled)
                    {
                        this.terminal = new FormTerminalInfo();
                        this.terminal.MdiParent = this;
                        this.terminal.WindowState = FormWindowState.Maximized;
                        this.terminal.Show();
                    }
                }
                ControlPanel.SendCommand(0, 0);
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }

            if (GlobalParmeters.DuiJiangMode == Service.Enums.EnumDuiJiangMode.INTRADAK_SG)
            {
                this.兑奖绑定ToolStripMenuItem.Visible = true;
                this.兑奖设置ToolStripMenuItem.Visible = true;
                this.兑奖密码设置ToolStripMenuItem.Visible = true;
                this.网络测试ToolStripMenuItem.Visible = true;
            }
            else
            {
                this.兑奖绑定ToolStripMenuItem.Visible = false;
                this.兑奖设置ToolStripMenuItem.Visible = false;
                this.兑奖密码设置ToolStripMenuItem.Visible = false;
                this.网络测试ToolStripMenuItem.Visible = false;
            }
        }

        private void FormAdminMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i].ProcessName == "ScreenKey")
                    {
                        processes[i].Kill();
                    }
                }

                ControlPanel.SendCommand(0, 1);
                ControlPanel.ClosePort();
                ControlPanel.MyPort = null;
                JCMBillAcceptorClass.ClosePort();
                JCMBillAcceptorClass.MyPort = null;

                GlobalParmeters.IsOpenScreenKey = false;
                GlobalParmeters.IsStartHook = false;
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        private void 解压到终端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.frmUnzipToTerminal == null) || !this.frmUnzipToTerminal.Enabled)
            {
                this.frmUnzipToTerminal = new FormUnzipToTerminal();
                this.frmUnzipToTerminal.MdiParent = this;
                this.frmUnzipToTerminal.WindowState = FormWindowState.Maximized;
                this.frmUnzipToTerminal.Show();
            }
            this.frmUnzipToTerminal.BringToFront();
        }

        private void 退币报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                this.payout = new FormPayoutReport();
                this.payout.MdiParent = this;
                this.payout.WindowState = FormWindowState.Maximized;
                this.payout.Show();
            }
            this.payout.BringToFront();

        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.config == null) || !this.config.Enabled)
            {
                this.config = new FormConfig();
                this.config.MdiParent = this;
                this.config.WindowState = FormWindowState.Maximized;
                this.config.Show();
            }
            this.config.BringToFront();
        }

        private void 出票明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.cutPaper == null) || !this.cutPaper.Enabled)
            {
                this.cutPaper = new FormCutPaperReport();
                this.cutPaper.MdiParent = this;
                this.cutPaper.WindowState = FormWindowState.Maximized;
                this.cutPaper.Show();
            }
            this.cutPaper.BringToFront();
        }

        private void 当期接存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.terminal == null) || !this.terminal.Enabled)
            {
                this.terminal = new FormTerminalInfo();
                this.terminal.MdiParent = this;
                this.terminal.WindowState = FormWindowState.Maximized;
                this.terminal.Show();
            }
            
            this.terminal.BringToFront();
        }

        private void 兑奖明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.lotteryPrize == null) || !this.lotteryPrize.Enabled)
            {
                this.lotteryPrize = new FormLotteryPrize();
                this.lotteryPrize.MdiParent = this;
                this.lotteryPrize.WindowState = FormWindowState.Maximized;
                this.lotteryPrize.Show();
            }
            this.lotteryPrize.BringToFront();
        }

        private void 机头信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.motorList == null) || !this.motorList.Enabled)
            {
                this.motorList = new FormMotorList();
                this.motorList.MdiParent = this;
                this.motorList.WindowState = FormWindowState.Maximized;
                this.motorList.Show();
            }
            this.terminal.Close();
            this.motorList.BringToFront();
        }

        private void 加票汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.AddCount == null) || !this.AddCount.Enabled)
            {
                this.AddCount = new FormAddLotteryCount();
                this.AddCount.MdiParent = this;
                this.AddCount.WindowState = FormWindowState.Maximized;
                this.AddCount.Show();
            }
            this.AddCount.BringToFront();
        }

        private void 加票明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.addLottDetail == null) || !this.addLottDetail.Enabled)
            {
                this.addLottDetail = new FormAddLotteryDetailReport();
                this.addLottDetail.MdiParent = this;
                this.addLottDetail.WindowState = FormWindowState.Maximized;
                this.addLottDetail.Show();
            }
            this.addLottDetail.BringToFront();
        }

        private void 清空报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.empty == null) || !this.empty.Enabled)
            {
                this.empty = new FormEmptyCashReport();
                this.empty.MdiParent = this;
                this.empty.WindowState = FormWindowState.Maximized;
                this.empty.Show();
            }
            this.empty.BringToFront();
        }

        private void 取钞管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.frmGetCash == null) || !this.frmGetCash.Enabled)
            {
                this.frmGetCash = new FormGetCash();
                this.frmGetCash.MdiParent = this;
                this.frmGetCash.WindowState = FormWindowState.Maximized;
                this.frmGetCash.Show();
            }
            this.terminal.Close();
            this.frmGetCash.BringToFront();
        }

        private void 日志拷贝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.copyLog == null) || !this.copyLog.Enabled || this.copyLog.IsDisposed)
            {
                this.copyLog = new FormCopyLog();
                this.copyLog.MdiParent = this;
                this.copyLog.WindowState = FormWindowState.Maximized;
                this.copyLog.Show();
            }
            this.copyLog.BringToFront();
        }

        private void 退出购彩程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormShowDialog dialog = new FormShowDialog();
                GlobalParmeters.frmShowDialog = "真的要退出吗？";
                dialog.ShowDialog();
                dialog.Dispose();
                if (GlobalParmeters.frmShowDialog == "是")
                {
                    try
                    {
                        Process[] processes = Process.GetProcesses();
                        for (int i = 0; i < processes.Length; i++)
                        {
                            if ((processes[i].ProcessName == "TestMeidiaPlayWithDirectX") || (processes[i].ProcessName == "WinVideoPlayByAPlater"))
                            {
                                processes[i].Kill();
                            }
                        }
                        ControlPanel.SendCommand(0, 1);
                        JCMBillAcceptorClass.InhibitAllBillType();
                    }
                    catch
                    {
                    }
                    Environment.Exit(0);
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        private void 退出系统管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void 网络信号测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.netWork == null) || !this.netWork.Enabled)
            {
                this.netWork = new FormNetworkSpeed();
                this.netWork.MdiParent = this;
                this.netWork.WindowState = FormWindowState.Maximized;
                this.netWork.Show();
            }
            this.netWork.BringToFront();
        }

        private void 现金预存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.cashDeposit == null) || !this.cashDeposit.Enabled)
            {
                this.cashDeposit = new FormCashDeposit();
                this.cashDeposit.MdiParent = this;
                this.cashDeposit.WindowState = FormWindowState.Maximized;
                this.cashDeposit.Show();
            }
            this.terminal.Close();
            this.cashDeposit.BringToFront();
        }

        private void 销售报表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((this.trade == null) || !this.trade.Enabled)
            {
                this.trade = new FormTradeReport();
                this.trade.MdiParent = this;
                this.trade.WindowState = FormWindowState.Maximized;
                this.trade.Show();
            }
            this.trade.BringToFront();
        }

        private void 销售明细ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((this.tradeDetail == null) || !this.tradeDetail.Enabled)
            {
                this.tradeDetail = new FormTradeDetailReport();
                this.tradeDetail.MdiParent = this;
                this.tradeDetail.WindowState = FormWindowState.Maximized;
                this.tradeDetail.Show();
            }
            this.tradeDetail.BringToFront();

        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.password == null) || !this.password.Enabled)
            {
                this.password = new FormChangePwd(this);
                this.password.MdiParent = this;
                this.password.WindowState = FormWindowState.Maximized;
                this.password.Show();
            }
            this.password.BringToFront();
        }

        private void 预存报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.depositReport == null) || !this.depositReport.Enabled)
            {
                this.depositReport = new FormCashDepositReport();
                this.depositReport.MdiParent = this;
                this.depositReport.WindowState = FormWindowState.Maximized;
                this.depositReport.Show();
            }
            this.depositReport.BringToFront();
        }

        private void 当期结存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.terminal == null) || !this.terminal.Enabled)
            {
                this.terminal = new FormTerminalInfo();
                this.terminal.MdiParent = this;
                this.terminal.WindowState = FormWindowState.Maximized;
                this.terminal.Show();
            }
            this.terminal.BringToFront();
        }

        private void 收钞报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.receCash == null) || !this.receCash.Enabled)
            {
                this.receCash = new FormReceCash();
                this.receCash.MdiParent = this;
                this.receCash.WindowState = FormWindowState.Maximized;
                this.receCash.Show();
            }
            this.receCash.BringToFront();
        }

        private void 退币统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void 加票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.motorList == null) || !this.motorList.Enabled)
            {
                this.motorList = new FormMotorList();
                this.motorList.MdiParent = this;
                this.motorList.WindowState = FormWindowState.Maximized;
                this.motorList.Show();
            }
            this.terminal.Close();
            this.motorList.BringToFront();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 兑奖绑定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.frmBind == null) || !this.frmBind.Enabled)
            {
                this.frmBind = new FormSGBind();
                this.frmBind.MdiParent = this;
                this.frmBind.WindowState = FormWindowState.Maximized;
                this.frmBind.Show();
            }
            this.frmBind.BringToFront();
        }

        private void 兑奖设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.frmSet == null) || !this.frmSet.Enabled)
            {
                this.frmSet = new FormSGSet();
                this.frmSet.MdiParent = this;
                this.frmSet.WindowState = FormWindowState.Maximized;
                this.frmSet.Show();
            }
            this.frmSet.BringToFront();

        }

        private void 兑奖密码设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.frmChangePassword == null) || !this.frmChangePassword.Enabled)
            {
                this.frmChangePassword = new FormSGChangePassword();
                this.frmChangePassword.MdiParent = this;
                this.frmChangePassword.WindowState = FormWindowState.Maximized;
                this.frmChangePassword.Show();
            }
            this.frmChangePassword.BringToFront();
        }

        private void 网络测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.frmFactoryTest == null) || !this.frmFactoryTest.Enabled)
            {
                this.frmFactoryTest = new FormSGFactoryTest();
                this.frmFactoryTest.MdiParent = this;
                this.frmFactoryTest.WindowState = FormWindowState.Maximized;
                this.frmFactoryTest.Show();
            }
            this.frmFactoryTest.BringToFront();
        }

        private void menuSetSysMode_Click(object sender, EventArgs e)
        {
            FormSetSysMode frmSetSysMode = new FormSetSysMode();
            frmSetSysMode.ShowDialog();

        }

        private void menuStartLog_Click(object sender, EventArgs e)
        {
            FormConsole formConsole = new FormConsole();
            formConsole.ShowDialog();
        }

        private void menuItemSystemCommand_Click(object sender, EventArgs e)
        {
            FormCommand formCommand = new FormCommand();
            formCommand.ShowDialog();
        }

        private void 明码兑奖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!((this.frmPrize != null) && this.frmPrize.Enabled))
            {
                this.frmPrize = new FormMingMaPrize();
                this.frmPrize.MdiParent = this;
                this.frmPrize.WindowState = FormWindowState.Maximized;
                this.frmPrize.Show();
            }
            this.frmPrize.BringToFront();
        }
    }
}
