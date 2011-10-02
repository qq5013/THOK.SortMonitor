using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AS.Sorting.Dal;
using THOK.MCP;

namespace THOK.AS.Sorting.View
{
    public partial class UploadDataForm : THOK.AF.View.ToolbarForm
    {
        UploadDataDal udal = new UploadDataDal();

        public UploadDataForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = udal.FindSortUploadInfo();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable sort = udal.FindEfficiency();
                if (sort.Rows[0]["SUMQUANTITY"].ToString() == sort.Rows[0]["SORTQUANTITY"].ToString())
                {
                    DataTable sortTable = udal.GetSortUploadData("0");
                    if (sortTable.Rows.Count > 0)
                    {
                        udal.GetSortingUploadData();
                        sortTable = udal.GetSortUploadData("0");
                        udal.UploadSortingData(sortTable);
                        MessageBox.Show("上报分拣情况表数据完成！", "消息");
                    }
                    else
                        MessageBox.Show("没有分拣情况数据要上报！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("分拣数据没有分拣完，不能上报！", "消息");
            }
            catch (Exception exp)
            {
                MessageBox.Show("上报分拣情况表数据出错！原因："+exp.Message,"消息");
                Logger.Error(exp.Message);
            }
        }
    }
}

