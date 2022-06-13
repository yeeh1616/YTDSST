namespace YTDSSTGenII.Forms
{
    partial class FormAdminMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdminMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当期接存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络信号测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数配置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.兑奖绑定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.兑奖设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.兑奖密码设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.明码兑奖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志拷贝ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解压到终端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSystemCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.票务管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加票ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加票汇总ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加票明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出票明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.本地报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当期结存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收钞报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售报表ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.退币统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售明细ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.找币器管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.现金预存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预存报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退币报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收币器管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取钞管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清钞管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收钞管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售汇总ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.兑奖明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.密码修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSysDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetSysMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStartLog = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出购彩程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.现金预存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.机头信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerMouseKey = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统管理ToolStripMenuItem,
            this.票务管理ToolStripMenuItem,
            this.本地报表ToolStripMenuItem,
            this.找币器管理ToolStripMenuItem,
            this.收币器管理ToolStripMenuItem,
            this.销售报表ToolStripMenuItem,
            this.用户管理ToolStripMenuItem,
            this.menuSysDebug,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1212, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 系统管理ToolStripMenuItem
            // 
            this.系统管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当期接存ToolStripMenuItem,
            this.网络信号测试ToolStripMenuItem,
            this.参数配置ToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.兑奖绑定ToolStripMenuItem,
            this.兑奖设置ToolStripMenuItem,
            this.兑奖密码设置ToolStripMenuItem,
            this.明码兑奖ToolStripMenuItem,
            this.网络测试ToolStripMenuItem,
            this.日志拷贝ToolStripMenuItem,
            this.解压到终端ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.menuItemSystemCommand});
            this.系统管理ToolStripMenuItem.Name = "系统管理ToolStripMenuItem";
            this.系统管理ToolStripMenuItem.Size = new System.Drawing.Size(122, 38);
            this.系统管理ToolStripMenuItem.Text = "系统管理";
            // 
            // 当期接存ToolStripMenuItem
            // 
            this.当期接存ToolStripMenuItem.Name = "当期接存ToolStripMenuItem";
            this.当期接存ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.当期接存ToolStripMenuItem.Text = "当期结存";
            this.当期接存ToolStripMenuItem.Click += new System.EventHandler(this.当期接存ToolStripMenuItem_Click);
            // 
            // 网络信号测试ToolStripMenuItem
            // 
            this.网络信号测试ToolStripMenuItem.Name = "网络信号测试ToolStripMenuItem";
            this.网络信号测试ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.网络信号测试ToolStripMenuItem.Text = "网络信号测试";
            this.网络信号测试ToolStripMenuItem.Click += new System.EventHandler(this.网络信号测试ToolStripMenuItem_Click);
            // 
            // 参数配置ToolStripMenuItem1
            // 
            this.参数配置ToolStripMenuItem1.Name = "参数配置ToolStripMenuItem1";
            this.参数配置ToolStripMenuItem1.Size = new System.Drawing.Size(269, 38);
            this.参数配置ToolStripMenuItem1.Text = "参数配置";
            this.参数配置ToolStripMenuItem1.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(266, 6);
            // 
            // 兑奖绑定ToolStripMenuItem
            // 
            this.兑奖绑定ToolStripMenuItem.Name = "兑奖绑定ToolStripMenuItem";
            this.兑奖绑定ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.兑奖绑定ToolStripMenuItem.Text = "兑奖绑定";
            this.兑奖绑定ToolStripMenuItem.Click += new System.EventHandler(this.兑奖绑定ToolStripMenuItem_Click);
            // 
            // 兑奖设置ToolStripMenuItem
            // 
            this.兑奖设置ToolStripMenuItem.Name = "兑奖设置ToolStripMenuItem";
            this.兑奖设置ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.兑奖设置ToolStripMenuItem.Text = "兑奖设置";
            this.兑奖设置ToolStripMenuItem.Click += new System.EventHandler(this.兑奖设置ToolStripMenuItem_Click);
            // 
            // 兑奖密码设置ToolStripMenuItem
            // 
            this.兑奖密码设置ToolStripMenuItem.Name = "兑奖密码设置ToolStripMenuItem";
            this.兑奖密码设置ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.兑奖密码设置ToolStripMenuItem.Text = "兑奖密码设置";
            this.兑奖密码设置ToolStripMenuItem.Click += new System.EventHandler(this.兑奖密码设置ToolStripMenuItem_Click);
            // 
            // 明码兑奖ToolStripMenuItem
            // 
            this.明码兑奖ToolStripMenuItem.Name = "明码兑奖ToolStripMenuItem";
            this.明码兑奖ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.明码兑奖ToolStripMenuItem.Text = "明码兑奖";
            this.明码兑奖ToolStripMenuItem.Visible = false;
            this.明码兑奖ToolStripMenuItem.Click += new System.EventHandler(this.明码兑奖ToolStripMenuItem_Click);
            // 
            // 网络测试ToolStripMenuItem
            // 
            this.网络测试ToolStripMenuItem.Name = "网络测试ToolStripMenuItem";
            this.网络测试ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.网络测试ToolStripMenuItem.Text = "网络测试";
            this.网络测试ToolStripMenuItem.Click += new System.EventHandler(this.网络测试ToolStripMenuItem_Click);
            // 
            // 日志拷贝ToolStripMenuItem
            // 
            this.日志拷贝ToolStripMenuItem.Name = "日志拷贝ToolStripMenuItem";
            this.日志拷贝ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.日志拷贝ToolStripMenuItem.Text = "日志拷贝";
            this.日志拷贝ToolStripMenuItem.Click += new System.EventHandler(this.日志拷贝ToolStripMenuItem_Click);
            // 
            // 解压到终端ToolStripMenuItem
            // 
            this.解压到终端ToolStripMenuItem.Name = "解压到终端ToolStripMenuItem";
            this.解压到终端ToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.解压到终端ToolStripMenuItem.Text = "解压广告";
            this.解压到终端ToolStripMenuItem.Click += new System.EventHandler(this.解压到终端ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(266, 6);
            // 
            // menuItemSystemCommand
            // 
            this.menuItemSystemCommand.Name = "menuItemSystemCommand";
            this.menuItemSystemCommand.Size = new System.Drawing.Size(269, 38);
            this.menuItemSystemCommand.Text = "系统命令";
            this.menuItemSystemCommand.Click += new System.EventHandler(this.menuItemSystemCommand_Click);
            // 
            // 票务管理ToolStripMenuItem
            // 
            this.票务管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加票ToolStripMenuItem,
            this.加票汇总ToolStripMenuItem,
            this.加票明细ToolStripMenuItem,
            this.出票明细ToolStripMenuItem});
            this.票务管理ToolStripMenuItem.Name = "票务管理ToolStripMenuItem";
            this.票务管理ToolStripMenuItem.Size = new System.Drawing.Size(122, 38);
            this.票务管理ToolStripMenuItem.Text = "票务管理";
            // 
            // 加票ToolStripMenuItem
            // 
            this.加票ToolStripMenuItem.Name = "加票ToolStripMenuItem";
            this.加票ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.加票ToolStripMenuItem.Text = "加票";
            this.加票ToolStripMenuItem.Click += new System.EventHandler(this.加票ToolStripMenuItem_Click);
            // 
            // 加票汇总ToolStripMenuItem
            // 
            this.加票汇总ToolStripMenuItem.Name = "加票汇总ToolStripMenuItem";
            this.加票汇总ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.加票汇总ToolStripMenuItem.Text = "加票汇总";
            this.加票汇总ToolStripMenuItem.Click += new System.EventHandler(this.加票汇总ToolStripMenuItem_Click);
            // 
            // 加票明细ToolStripMenuItem
            // 
            this.加票明细ToolStripMenuItem.Name = "加票明细ToolStripMenuItem";
            this.加票明细ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.加票明细ToolStripMenuItem.Text = "加票明细";
            this.加票明细ToolStripMenuItem.Click += new System.EventHandler(this.加票明细ToolStripMenuItem_Click);
            // 
            // 出票明细ToolStripMenuItem
            // 
            this.出票明细ToolStripMenuItem.Name = "出票明细ToolStripMenuItem";
            this.出票明细ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.出票明细ToolStripMenuItem.Text = "出票明细";
            this.出票明细ToolStripMenuItem.Click += new System.EventHandler(this.出票明细ToolStripMenuItem_Click);
            // 
            // 本地报表ToolStripMenuItem
            // 
            this.本地报表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当期结存ToolStripMenuItem,
            this.收钞报表ToolStripMenuItem,
            this.销售报表ToolStripMenuItem1,
            this.退币统计ToolStripMenuItem,
            this.销售明细ToolStripMenuItem1});
            this.本地报表ToolStripMenuItem.Name = "本地报表ToolStripMenuItem";
            this.本地报表ToolStripMenuItem.Size = new System.Drawing.Size(122, 38);
            this.本地报表ToolStripMenuItem.Text = "本地报表";
            // 
            // 当期结存ToolStripMenuItem
            // 
            this.当期结存ToolStripMenuItem.Name = "当期结存ToolStripMenuItem";
            this.当期结存ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.当期结存ToolStripMenuItem.Text = "当期结存";
            this.当期结存ToolStripMenuItem.Click += new System.EventHandler(this.当期结存ToolStripMenuItem_Click);
            // 
            // 收钞报表ToolStripMenuItem
            // 
            this.收钞报表ToolStripMenuItem.Name = "收钞报表ToolStripMenuItem";
            this.收钞报表ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.收钞报表ToolStripMenuItem.Text = "收钞报表";
            this.收钞报表ToolStripMenuItem.Click += new System.EventHandler(this.收钞报表ToolStripMenuItem_Click);
            // 
            // 销售报表ToolStripMenuItem1
            // 
            this.销售报表ToolStripMenuItem1.Name = "销售报表ToolStripMenuItem1";
            this.销售报表ToolStripMenuItem1.Size = new System.Drawing.Size(209, 38);
            this.销售报表ToolStripMenuItem1.Text = "销售报表";
            this.销售报表ToolStripMenuItem1.Click += new System.EventHandler(this.销售报表ToolStripMenuItem1_Click);
            // 
            // 退币统计ToolStripMenuItem
            // 
            this.退币统计ToolStripMenuItem.Name = "退币统计ToolStripMenuItem";
            this.退币统计ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.退币统计ToolStripMenuItem.Text = "退币统计";
            this.退币统计ToolStripMenuItem.Click += new System.EventHandler(this.退币统计ToolStripMenuItem_Click);
            // 
            // 销售明细ToolStripMenuItem1
            // 
            this.销售明细ToolStripMenuItem1.Name = "销售明细ToolStripMenuItem1";
            this.销售明细ToolStripMenuItem1.Size = new System.Drawing.Size(209, 38);
            this.销售明细ToolStripMenuItem1.Text = "销售明细";
            this.销售明细ToolStripMenuItem1.Click += new System.EventHandler(this.销售明细ToolStripMenuItem1_Click);
            // 
            // 找币器管理ToolStripMenuItem
            // 
            this.找币器管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.现金预存ToolStripMenuItem,
            this.预存报表ToolStripMenuItem,
            this.退币报表ToolStripMenuItem,
            this.清空报表ToolStripMenuItem});
            this.找币器管理ToolStripMenuItem.Name = "找币器管理ToolStripMenuItem";
            this.找币器管理ToolStripMenuItem.Size = new System.Drawing.Size(146, 38);
            this.找币器管理ToolStripMenuItem.Text = "找币器管理";
            // 
            // 现金预存ToolStripMenuItem
            // 
            this.现金预存ToolStripMenuItem.Name = "现金预存ToolStripMenuItem";
            this.现金预存ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.现金预存ToolStripMenuItem.Text = "现金预存";
            this.现金预存ToolStripMenuItem.Click += new System.EventHandler(this.现金预存ToolStripMenuItem_Click);
            // 
            // 预存报表ToolStripMenuItem
            // 
            this.预存报表ToolStripMenuItem.Name = "预存报表ToolStripMenuItem";
            this.预存报表ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.预存报表ToolStripMenuItem.Text = "预存报表";
            this.预存报表ToolStripMenuItem.Click += new System.EventHandler(this.预存报表ToolStripMenuItem_Click);
            // 
            // 退币报表ToolStripMenuItem
            // 
            this.退币报表ToolStripMenuItem.Name = "退币报表ToolStripMenuItem";
            this.退币报表ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.退币报表ToolStripMenuItem.Text = "退币报表";
            this.退币报表ToolStripMenuItem.Click += new System.EventHandler(this.退币报表ToolStripMenuItem_Click);
            // 
            // 清空报表ToolStripMenuItem
            // 
            this.清空报表ToolStripMenuItem.Name = "清空报表ToolStripMenuItem";
            this.清空报表ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.清空报表ToolStripMenuItem.Text = "清空报表";
            this.清空报表ToolStripMenuItem.Click += new System.EventHandler(this.清空报表ToolStripMenuItem_Click);
            // 
            // 收币器管理ToolStripMenuItem
            // 
            this.收币器管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.取钞管理ToolStripMenuItem,
            this.清钞管理ToolStripMenuItem,
            this.收钞管理ToolStripMenuItem});
            this.收币器管理ToolStripMenuItem.Name = "收币器管理ToolStripMenuItem";
            this.收币器管理ToolStripMenuItem.Size = new System.Drawing.Size(146, 38);
            this.收币器管理ToolStripMenuItem.Text = "收币器管理";
            // 
            // 取钞管理ToolStripMenuItem
            // 
            this.取钞管理ToolStripMenuItem.Name = "取钞管理ToolStripMenuItem";
            this.取钞管理ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.取钞管理ToolStripMenuItem.Text = "取钞管理";
            this.取钞管理ToolStripMenuItem.Click += new System.EventHandler(this.取钞管理ToolStripMenuItem_Click);
            // 
            // 清钞管理ToolStripMenuItem
            // 
            this.清钞管理ToolStripMenuItem.Name = "清钞管理ToolStripMenuItem";
            this.清钞管理ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.清钞管理ToolStripMenuItem.Text = "清钞明细";
            this.清钞管理ToolStripMenuItem.Click += new System.EventHandler(this.清钞管理ToolStripMenuItem_Click);
            // 
            // 收钞管理ToolStripMenuItem
            // 
            this.收钞管理ToolStripMenuItem.Name = "收钞管理ToolStripMenuItem";
            this.收钞管理ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.收钞管理ToolStripMenuItem.Text = "收钞明细";
            this.收钞管理ToolStripMenuItem.Click += new System.EventHandler(this.收钞管理ToolStripMenuItem_Click);
            // 
            // 销售报表ToolStripMenuItem
            // 
            this.销售报表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.销售明细ToolStripMenuItem,
            this.销售汇总ToolStripMenuItem,
            this.兑奖明细ToolStripMenuItem});
            this.销售报表ToolStripMenuItem.Name = "销售报表ToolStripMenuItem";
            this.销售报表ToolStripMenuItem.Size = new System.Drawing.Size(122, 38);
            this.销售报表ToolStripMenuItem.Text = "销售报表";
            // 
            // 销售明细ToolStripMenuItem
            // 
            this.销售明细ToolStripMenuItem.Name = "销售明细ToolStripMenuItem";
            this.销售明细ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.销售明细ToolStripMenuItem.Text = "销售明细";
            this.销售明细ToolStripMenuItem.Click += new System.EventHandler(this.销售明细ToolStripMenuItem1_Click);
            // 
            // 销售汇总ToolStripMenuItem
            // 
            this.销售汇总ToolStripMenuItem.Name = "销售汇总ToolStripMenuItem";
            this.销售汇总ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.销售汇总ToolStripMenuItem.Text = "销售汇总";
            this.销售汇总ToolStripMenuItem.Click += new System.EventHandler(this.销售报表ToolStripMenuItem1_Click);
            // 
            // 兑奖明细ToolStripMenuItem
            // 
            this.兑奖明细ToolStripMenuItem.Name = "兑奖明细ToolStripMenuItem";
            this.兑奖明细ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.兑奖明细ToolStripMenuItem.Text = "兑奖明细";
            this.兑奖明细ToolStripMenuItem.Click += new System.EventHandler(this.兑奖明细ToolStripMenuItem_Click);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.密码修改ToolStripMenuItem});
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(122, 38);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            // 
            // 密码修改ToolStripMenuItem
            // 
            this.密码修改ToolStripMenuItem.Name = "密码修改ToolStripMenuItem";
            this.密码修改ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.密码修改ToolStripMenuItem.Text = "密码修改";
            this.密码修改ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // menuSysDebug
            // 
            this.menuSysDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSetSysMode,
            this.menuStartLog});
            this.menuSysDebug.Name = "menuSysDebug";
            this.menuSysDebug.Size = new System.Drawing.Size(156, 38);
            this.menuSysDebug.Text = "系统调试(&D)";
            // 
            // menuSetSysMode
            // 
            this.menuSetSysMode.Name = "menuSetSysMode";
            this.menuSetSysMode.Size = new System.Drawing.Size(257, 38);
            this.menuSetSysMode.Text = "设置系统模式";
            this.menuSetSysMode.Click += new System.EventHandler(this.menuSetSysMode_Click);
            // 
            // menuStartLog
            // 
            this.menuStartLog.Name = "menuStartLog";
            this.menuStartLog.Size = new System.Drawing.Size(257, 38);
            this.menuStartLog.Text = "查看启动日志";
            this.menuStartLog.Click += new System.EventHandler(this.menuStartLog_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出系统管理ToolStripMenuItem,
            this.退出购彩程序ToolStripMenuItem});
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(74, 38);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 退出系统管理ToolStripMenuItem
            // 
            this.退出系统管理ToolStripMenuItem.Name = "退出系统管理ToolStripMenuItem";
            this.退出系统管理ToolStripMenuItem.Size = new System.Drawing.Size(257, 38);
            this.退出系统管理ToolStripMenuItem.Text = "退出系统管理";
            this.退出系统管理ToolStripMenuItem.Click += new System.EventHandler(this.退出系统管理ToolStripMenuItem_Click);
            // 
            // 退出购彩程序ToolStripMenuItem
            // 
            this.退出购彩程序ToolStripMenuItem.Name = "退出购彩程序ToolStripMenuItem";
            this.退出购彩程序ToolStripMenuItem.Size = new System.Drawing.Size(257, 38);
            this.退出购彩程序ToolStripMenuItem.Text = "退出购彩程序";
            this.退出购彩程序ToolStripMenuItem.Click += new System.EventHandler(this.退出购彩程序ToolStripMenuItem_Click);
            // 
            // 系统管理ToolStripMenuItem1
            // 
            this.系统管理ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.现金预存ToolStripMenuItem1,
            this.机头信息ToolStripMenuItem,
            this.参数设置ToolStripMenuItem});
            this.系统管理ToolStripMenuItem1.Name = "系统管理ToolStripMenuItem1";
            this.系统管理ToolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.系统管理ToolStripMenuItem1.Text = "系统管理";
            // 
            // 现金预存ToolStripMenuItem1
            // 
            this.现金预存ToolStripMenuItem1.Name = "现金预存ToolStripMenuItem1";
            this.现金预存ToolStripMenuItem1.Size = new System.Drawing.Size(209, 38);
            this.现金预存ToolStripMenuItem1.Text = "现金预存";
            this.现金预存ToolStripMenuItem1.Click += new System.EventHandler(this.现金预存ToolStripMenuItem_Click);
            // 
            // 机头信息ToolStripMenuItem
            // 
            this.机头信息ToolStripMenuItem.Name = "机头信息ToolStripMenuItem";
            this.机头信息ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.机头信息ToolStripMenuItem.Text = "机头信息";
            this.机头信息ToolStripMenuItem.Click += new System.EventHandler(this.机头信息ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(209, 38);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // timerMouseKey
            // 
            this.timerMouseKey.Tick += new System.EventHandler(this.timerMouseKey_Tick);
            // 
            // FormAdminMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1212, 681);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdminMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "系统管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAdminMain_FormClosing);
            this.Load += new System.EventHandler(this.FormAdminMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 票务管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加票ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加票汇总ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当期接存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络信号测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数配置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 日志拷贝ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 解压到终端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加票明细ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出票明细ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 找币器管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 现金预存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预存报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退币报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收币器管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取钞管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清钞管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收钞管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销售报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销售明细ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销售汇总ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 兑奖明细ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 密码修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出系统管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出购彩程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 现金预存ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 机头信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本地报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当期结存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收钞报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销售报表ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退币统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 销售明细ToolStripMenuItem1;
        private System.Windows.Forms.Timer timerMouseKey;
        private System.Windows.Forms.ToolStripMenuItem 兑奖绑定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 兑奖设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 兑奖密码设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSysDebug;
        private System.Windows.Forms.ToolStripMenuItem menuSetSysMode;
        private System.Windows.Forms.ToolStripMenuItem menuStartLog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemSystemCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 明码兑奖ToolStripMenuItem;
    }
}