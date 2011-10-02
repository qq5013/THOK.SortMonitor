using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AS.Sorting.Dal;

namespace THOK.AS.Sorting.View
{
    public partial class CacheOrderQueryForm :Form
    {
        private int sortNo = 0;
        private int channelGroup = 0;
        private OrderDal orderDal = new OrderDal();
        public CacheOrderQueryForm(int deviceNo,int channelGroup, int sortNo)
        {
            InitializeComponent();
            this.sortNo = sortNo;
            this.channelGroup = channelGroup;

            int sumQutity = 0;

            DataTable table = orderDal.GetOrderDetailForCacheOrderQuery(channelGroup, sortNo);
            if (table.Rows.Count != 0)
            {
                dgvDetail.DataSource = table;
                sumQutity = Convert.ToInt32(table.Compute("SUM(QUANTITY)", ""));
            }

            this.Text = this.Text + string.Format("[{0}线-{1}号缓存段-{2}号流水号][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNo, sumQutity);

        }

        public CacheOrderQueryForm(string packMode, int exportNo,int sortNo,int channelGroup)
        {
            InitializeComponent();
            this.sortNo = sortNo;
            this.channelGroup = channelGroup;

            int sumQutity = 0;
            DataTable table  = orderDal.GetOrderDetailForCacheOrderQuery(packMode, exportNo, sortNo);

            if (table.Rows.Count != 0)
            {
                dgvDetail.DataSource = table;
                sumQutity = Convert.ToInt32(table.Compute("SUM(QUANTITY)", ""));
            }

            this.Text = this.Text + string.Format("[{0}号包装机缓存段-{1}号流水号][总数量：{2}]",exportNo, sortNo, sumQutity);
        }

        public void LoadColor(int sortNo,int channelGroup)
        {
            DataTable table = orderDal.GetOrderDetailForCacheOrderQuery(channelGroup, sortNo);

            foreach (DataGridViewRow row in dgvDetail.Rows)
            {
                string sChannelGroup = row.Cells["CHANNELLINE"].Value.ToString();
                int iSortNo = Convert.ToInt32(row.Cells["SORTNO"].Value);
                DataRow[] dataRow = table.Select(string.Format("CHANNELLINE = '{0}' AND SORTNO = {1}", sChannelGroup, iSortNo));

                if (dataRow.Length > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        public void CacheOrderQueryForm_Paint(object sender, PaintEventArgs e)
        {
            LoadColor(this.sortNo, this.channelGroup);
        }
    }
}