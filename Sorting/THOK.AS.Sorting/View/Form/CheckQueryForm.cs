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
        }
    }
}

