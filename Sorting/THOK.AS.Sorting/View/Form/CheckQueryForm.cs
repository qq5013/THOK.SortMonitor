using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AS.Sorting.Dal;
using THOK.AS.Sorting.Dao;
using THOK.MCP;
using THOK.Util;

namespace THOK.AS.Sorting.View
{
    public partial class CheckQueryForm : THOK.AF.View.ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();
        private string channelGroup = "A";

        public CheckQueryForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            SortNoDialog sortnoDialog = new SortNoDialog();
            if (sortnoDialog.ShowDialog() == DialogResult.OK)
            {
                dgvMain.DataSource = channelDal.GetChannel(sortnoDialog.SortNo, channelGroup);
                channelGroup = channelGroup == "A" ? "B" : "A";
            }
            else
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    string sortNo_A = "";
                    string sortNo_B = "";
                    OrderDao orderDao = new OrderDao();
                    sortNo_A = orderDao.FindMaxSortedMaster("A");
                    sortNo_B = orderDao.FindMaxSortedMaster("B");
                    ChannelDao channelDao = new ChannelDao();
                    dgvMain.DataSource = channelDao.FindAllChannelQuantity(sortNo_A, sortNo_B);
                }
            }
        }
    }
}

