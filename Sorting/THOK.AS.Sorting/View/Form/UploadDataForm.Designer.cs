namespace THOK.AS.Sorting.View
{
    partial class UploadDataForm
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
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.SORT_BILL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORG_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORTING_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_ORDER_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_BEGIN_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_END_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORT_COST_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISIMPORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IS_IMPORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnUpload);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(877, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(877, 409);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(877, 462);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvMain.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SORT_BILL_ID,
            this.ORG_CODE,
            this.ORDERDATE,
            this.SORTING_CODE,
            this.SORT_DATE,
            this.SORT_SPEC,
            this.SORT_QUANTITY,
            this.SORT_ORDER_NUM,
            this.SORT_BEGIN_DATE,
            this.SORT_END_DATE,
            this.SORT_COST_TIME,
            this.ISIMPORT,
            this.IS_IMPORT});
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(877, 409);
            this.dgvMain.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::THOK.AS.Sorting.Properties.Resources.Chart;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 51);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::THOK.AS.Sorting.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(96, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 51);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Image = global::THOK.AS.Sorting.Properties.Resources.Info;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpload.Location = new System.Drawing.Point(48, 1);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(48, 51);
            this.btnUpload.TabIndex = 15;
            this.btnUpload.Text = "上报";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // SORT_BILL_ID
            // 
            this.SORT_BILL_ID.DataPropertyName = "SORT_BILL_ID";
            this.SORT_BILL_ID.HeaderText = "分拣单编码";
            this.SORT_BILL_ID.Name = "SORT_BILL_ID";
            this.SORT_BILL_ID.ReadOnly = true;
            this.SORT_BILL_ID.Width = 92;
            // 
            // ORG_CODE
            // 
            this.ORG_CODE.DataPropertyName = "ORG_CODE";
            this.ORG_CODE.HeaderText = "所属单位编码";
            this.ORG_CODE.Name = "ORG_CODE";
            this.ORG_CODE.ReadOnly = true;
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.HeaderText = "订单日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Width = 80;
            // 
            // SORTING_CODE
            // 
            this.SORTING_CODE.DataPropertyName = "SORTING_CODE";
            this.SORTING_CODE.HeaderText = "分拣线编码";
            this.SORTING_CODE.Name = "SORTING_CODE";
            this.SORTING_CODE.ReadOnly = true;
            this.SORTING_CODE.Width = 92;
            // 
            // SORT_DATE
            // 
            this.SORT_DATE.DataPropertyName = "SORT_DATE";
            this.SORT_DATE.HeaderText = "分拣日期";
            this.SORT_DATE.Name = "SORT_DATE";
            this.SORT_DATE.ReadOnly = true;
            this.SORT_DATE.Width = 85;
            // 
            // SORT_SPEC
            // 
            this.SORT_SPEC.DataPropertyName = "SORT_SPEC";
            this.SORT_SPEC.HeaderText = "分拣规格总数";
            this.SORT_SPEC.Name = "SORT_SPEC";
            this.SORT_SPEC.ReadOnly = true;
            // 
            // SORT_QUANTITY
            // 
            this.SORT_QUANTITY.DataPropertyName = "SORT_QUANTITY";
            this.SORT_QUANTITY.HeaderText = "分拣总数量";
            this.SORT_QUANTITY.Name = "SORT_QUANTITY";
            this.SORT_QUANTITY.ReadOnly = true;
            this.SORT_QUANTITY.Width = 96;
            // 
            // SORT_ORDER_NUM
            // 
            this.SORT_ORDER_NUM.DataPropertyName = "SORT_ORDER_NUM";
            this.SORT_ORDER_NUM.HeaderText = "分拣订单数";
            this.SORT_ORDER_NUM.Name = "SORT_ORDER_NUM";
            this.SORT_ORDER_NUM.ReadOnly = true;
            this.SORT_ORDER_NUM.Width = 96;
            // 
            // SORT_BEGIN_DATE
            // 
            this.SORT_BEGIN_DATE.DataPropertyName = "SORT_BEGIN_DATE";
            this.SORT_BEGIN_DATE.HeaderText = "分拣开始时间";
            this.SORT_BEGIN_DATE.Name = "SORT_BEGIN_DATE";
            this.SORT_BEGIN_DATE.ReadOnly = true;
            // 
            // SORT_END_DATE
            // 
            this.SORT_END_DATE.DataPropertyName = "SORT_END_DATE";
            this.SORT_END_DATE.HeaderText = "分拣结束时间";
            this.SORT_END_DATE.Name = "SORT_END_DATE";
            this.SORT_END_DATE.ReadOnly = true;
            // 
            // SORT_COST_TIME
            // 
            this.SORT_COST_TIME.DataPropertyName = "SORT_COST_TIME";
            this.SORT_COST_TIME.HeaderText = "分拣用时(秒)";
            this.SORT_COST_TIME.Name = "SORT_COST_TIME";
            this.SORT_COST_TIME.ReadOnly = true;
            // 
            // ISIMPORT
            // 
            this.ISIMPORT.DataPropertyName = "ISIMPORT";
            this.ISIMPORT.HeaderText = "上报状态";
            this.ISIMPORT.Name = "ISIMPORT";
            this.ISIMPORT.ReadOnly = true;
            this.ISIMPORT.Width = 80;
            // 
            // IS_IMPORT
            // 
            this.IS_IMPORT.DataPropertyName = "IS_IMPORT";
            this.IS_IMPORT.HeaderText = "IS_IMPORT";
            this.IS_IMPORT.Name = "IS_IMPORT";
            this.IS_IMPORT.ReadOnly = true;
            this.IS_IMPORT.Visible = false;
            // 
            // UploadDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(877, 462);
            this.Name = "UploadDataForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_BILL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORG_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTING_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_ORDER_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_BEGIN_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_END_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORT_COST_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISIMPORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn IS_IMPORT;
    }
}
