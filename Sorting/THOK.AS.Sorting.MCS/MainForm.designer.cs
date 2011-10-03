namespace THOK.AS.Sorting.MCS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.monitorView = new THOK.MCP.View.MonitorView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.tabLeft = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tpExport = new System.Windows.Forms.TabPage();
            this.sortingStatus = new THOK.AS.Sorting.View.SortingStatus();
            this.buttonArea = new THOK.AS.Sorting.View.ButtonArea();
            this.pnlTitle.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.tabLeft.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1016, 58);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTitle.Location = new System.Drawing.Point(405, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(375, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "天海欧康分拣监控系统";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.monitorView);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 58);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1016, 512);
            this.pnlMain.TabIndex = 2;
            // 
            // monitorView
            // 
            this.monitorView.BackColor = System.Drawing.SystemColors.ControlDark;
            this.monitorView.BackgroundImage = global::THOK.AS.Sorting.MCS.Properties.Resources.S13;
            this.monitorView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.monitorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorView.Location = new System.Drawing.Point(0, 0);
            this.monitorView.Name = "monitorView";
            this.monitorView.Size = new System.Drawing.Size(1016, 512);
            this.monitorView.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.scBottom);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 570);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1016, 130);
            this.pnlBottom.TabIndex = 1;
            // 
            // scBottom
            // 
            this.scBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBottom.IsSplitterFixed = true;
            this.scBottom.Location = new System.Drawing.Point(0, 0);
            this.scBottom.Name = "scBottom";
            // 
            // scBottom.Panel1
            // 
            this.scBottom.Panel1.Controls.Add(this.tabLeft);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.buttonArea);
            this.scBottom.Size = new System.Drawing.Size(1016, 130);
            this.scBottom.SplitterDistance = 518;
            this.scBottom.SplitterWidth = 2;
            this.scBottom.TabIndex = 0;
            // 
            // tabLeft
            // 
            this.tabLeft.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabLeft.Controls.Add(this.tpLog);
            this.tabLeft.Controls.Add(this.tpExport);
            this.tabLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLeft.Location = new System.Drawing.Point(0, 0);
            this.tabLeft.Multiline = true;
            this.tabLeft.Name = "tabLeft";
            this.tabLeft.SelectedIndex = 0;
            this.tabLeft.Size = new System.Drawing.Size(518, 130);
            this.tabLeft.TabIndex = 3;
            this.tabLeft.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabLeft_DrawItem);
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.lbLog);
            this.tpLog.Location = new System.Drawing.Point(4, 4);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(492, 122);
            this.tpLog.TabIndex = 0;
            this.tpLog.Text = "日志";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.Location = new System.Drawing.Point(3, 3);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(486, 108);
            this.lbLog.TabIndex = 0;
            // 
            // tpExport
            // 
            this.tpExport.Controls.Add(this.sortingStatus);
            this.tpExport.Location = new System.Drawing.Point(4, 4);
            this.tpExport.Name = "tpExport";
            this.tpExport.Size = new System.Drawing.Size(492, 122);
            this.tpExport.TabIndex = 1;
            this.tpExport.Text = "状态";
            this.tpExport.UseVisualStyleBackColor = true;
            // 
            // sortingStatus
            // 
            this.sortingStatus.BackColor = System.Drawing.SystemColors.Control;
            this.sortingStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortingStatus.Location = new System.Drawing.Point(0, 0);
            this.sortingStatus.Name = "sortingStatus";
            this.sortingStatus.Size = new System.Drawing.Size(492, 122);
            this.sortingStatus.TabIndex = 0;
            // 
            // buttonArea
            // 
            this.buttonArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonArea.Location = new System.Drawing.Point(0, 0);
            this.buttonArea.Name = "buttonArea";
            this.buttonArea.Size = new System.Drawing.Size(496, 130);
            this.buttonArea.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 700);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "卷烟分拣监控系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.ResumeLayout(false);
            this.tabLeft.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.tpExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private THOK.MCP.View.MonitorView monitorView;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.SplitContainer scBottom;
        private System.Windows.Forms.TabControl tabLeft;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TabPage tpExport;
        private THOK.AS.Sorting.View.SortingStatus sortingStatus;
        private THOK.AS.Sorting.View.ButtonArea buttonArea;
    }
}

