using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.Util;
using THOK.AS.Sorting.Dao;

namespace THOK.AS.Sorting.View
{
    public partial class SortSpeedCrystalForm : THOK.AF.View.ToolbarForm 
    {
        public SortSpeedCrystalForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                Report.CrystalSortSpeed crp = new THOK.AS.Sorting.Report.CrystalSortSpeed();
                crp.SetDataSource(orderDao.FindSortSpeed());
                this.crystalReportViewer1.ReportSource = crp;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}