namespace THOK.AS.Sorting.View
{
    partial class CustomerOrderQueryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.dgvMaster = new System.Windows.Forms.DataGridView();
            this.ORDERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROUTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROUTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMERCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORTNO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACKNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btQuery = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SORTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELLINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btQuery);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(1041, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.scMain);
            this.pnlContent.Size = new System.Drawing.Size(1041, 405);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1041, 458);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.dgvMaster);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvDetail);
            this.scMain.Size = new System.Drawing.Size(1041, 405);
            this.scMain.SplitterDistance = 191;
            this.scMain.TabIndex = 0;
            // 
            // dgvMaster
            // 
            this.dgvMaster.AllowUserToAddRows = false;
            this.dgvMaster.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvMaster.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaster.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERDATE,
            this.ORDERID1,
            this.ROUTECODE,
            this.ROUTENAME,
            this.CUSTOMERCODE,
            this.CUSTOMERNAME,
            this.QUANTITY1,
            this.QUANTITY2,
            this.SORTNO1,
            this.PACKNO});
            this.dgvMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaster.Location = new System.Drawing.Point(0, 0);
            this.dgvMaster.MultiSelect = false;
            this.dgvMaster.Name = "dgvMaster";
            this.dgvMaster.ReadOnly = true;
            this.dgvMaster.RowHeadersWidth = 30;
            this.dgvMaster.RowTemplate.Height = 23;
            this.dgvMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaster.Size = new System.Drawing.Size(1041, 191);
            this.dgvMaster.TabIndex = 2;
            this.dgvMaster.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaster_RowEnter);
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.HeaderText = "订单日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            // 
            // ORDERID1
            // 
            this.ORDERID1.DataPropertyName = "ORDERID";
            this.ORDERID1.HeaderText = "订单号";
            this.ORDERID1.Name = "ORDERID1";
            this.ORDERID1.ReadOnly = true;
            // 
            // ROUTECODE
            // 
            this.ROUTECODE.DataPropertyName = "ROUTECODE";
            this.ROUTECODE.HeaderText = "线路代码";
            this.ROUTECODE.Name = "ROUTECODE";
            this.ROUTECODE.ReadOnly = true;
            // 
            // ROUTENAME
            // 
            this.ROUTENAME.DataPropertyName = "ROUTENAME";
            this.ROUTENAME.HeaderText = "线路名称";
            this.ROUTENAME.Name = "ROUTENAME";
            this.ROUTENAME.ReadOnly = true;
            // 
            // CUSTOMERCODE
            // 
            this.CUSTOMERCODE.DataPropertyName = "CUSTOMERCODE";
            this.CUSTOMERCODE.HeaderText = "客户代码";
            this.CUSTOMERCODE.Name = "CUSTOMERCODE";
            this.CUSTOMERCODE.ReadOnly = true;
            // 
            // CUSTOMERNAME
            // 
            this.CUSTOMERNAME.DataPropertyName = "CUSTOMERNAME";
            this.CUSTOMERNAME.HeaderText = "客户名称";
            this.CUSTOMERNAME.Name = "CUSTOMERNAME";
            this.CUSTOMERNAME.ReadOnly = true;
            // 
            // QUANTITY1
            // 
            this.QUANTITY1.DataPropertyName = "QUANTITY";
            this.QUANTITY1.HeaderText = "A线总数量";
            this.QUANTITY1.Name = "QUANTITY1";
            this.QUANTITY1.ReadOnly = true;
            // 
            // QUANTITY2
            // 
            this.QUANTITY2.DataPropertyName = "QUANTITY1";
            this.QUANTITY2.HeaderText = "B线总数量";
            this.QUANTITY2.Name = "QUANTITY2";
            this.QUANTITY2.ReadOnly = true;
            // 
            // SORTNO1
            // 
            this.SORTNO1.DataPropertyName = "SORTNO";
            this.SORTNO1.HeaderText = "分拣流水号";
            this.SORTNO1.Name = "SORTNO1";
            this.SORTNO1.ReadOnly = true;
            // 
            // PACKNO
            // 
            this.PACKNO.DataPropertyName = "PACKNO";
            this.PACKNO.HeaderText = "客户顺序号";
            this.PACKNO.Name = "PACKNO";
            this.PACKNO.ReadOnly = true;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SORTNO,
            this.Pack,
            this.ORDERID,
            this.CHANNELNAME,
            this.CHANNELTYPE,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.CHANNELLINE});
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersWidth = 30;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1041, 210);
            this.dgvDetail.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::THOK.AS.Sorting.Properties.Resources.Chart;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btQuery
            // 
            this.btQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.btQuery.Image = global::THOK.AS.Sorting.Properties.Resources.Info;
            this.btQuery.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btQuery.Location = new System.Drawing.Point(48, 0);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(48, 51);
            this.btQuery.TabIndex = 19;
            this.btQuery.Text = "查询";
            this.btQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::THOK.AS.Sorting.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(96, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "SORTNO";
            this.SORTNO.HeaderText = "流水号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SORTNO.Width = 70;
            // 
            // Pack
            // 
            this.Pack.DataPropertyName = "PACK";
            this.Pack.HeaderText = "包号";
            this.Pack.Name = "Pack";
            this.Pack.ReadOnly = true;
            this.Pack.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ORDERID
            // 
            this.ORDERID.DataPropertyName = "ORDERID";
            this.ORDERID.HeaderText = "订单号";
            this.ORDERID.Name = "ORDERID";
            this.ORDERID.ReadOnly = true;
            this.ORDERID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            this.CHANNELNAME.HeaderText = "烟道名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            this.CHANNELNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CHANNELNAME.Width = 80;
            // 
            // CHANNELTYPE
            // 
            this.CHANNELTYPE.DataPropertyName = "CHANNELTYPE";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CHANNELTYPE.DefaultCellStyle = dataGridViewCellStyle5;
            this.CHANNELTYPE.HeaderText = "烟道类型";
            this.CHANNELTYPE.Name = "CHANNELTYPE";
            this.CHANNELTYPE.ReadOnly = true;
            this.CHANNELTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CHANNELTYPE.Width = 80;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.HeaderText = "卷烟代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            this.CIGARETTECODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CIGARETTENAME.Width = 200;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle6;
            this.QUANTITY.HeaderText = "数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QUANTITY.Width = 80;
            // 
            // CHANNELLINE
            // 
            this.CHANNELLINE.DataPropertyName = "CHANNELLINE";
            this.CHANNELLINE.HeaderText = "线组";
            this.CHANNELLINE.Name = "CHANNELLINE";
            this.CHANNELLINE.ReadOnly = true;
            this.CHANNELLINE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CustomerOrderQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1041, 458);
            this.Name = "CustomerOrderQueryForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Button btQuery;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.DataGridView dgvMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERID1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROUTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROUTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMERCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMERNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY1;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACKNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pack;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELLINE;
    }
}
