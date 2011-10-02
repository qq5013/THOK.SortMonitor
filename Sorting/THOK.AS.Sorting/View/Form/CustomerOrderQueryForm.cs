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
    public partial class CustomerOrderQueryForm : THOK.AF.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();
        public CustomerOrderQueryForm()
        {
            InitializeComponent();
            this.Column2.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
            this.Column8.FilteringEnabled = true;
            this.Column10.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bsMaster.DataSource = orderDal.GetPackMaster();
            if (bsMaster.DataSource != null)
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = orderDal.GetPackDetail(dgvMaster.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            DataTable table = orderDal.GetCigarettes();
            if (table.Rows.Count != 0)
            {
                CigaretteQueryDialog cigaretteQueryDialog = new CigaretteQueryDialog(table);
                if (cigaretteQueryDialog.ShowDialog() == DialogResult.OK)
                {
                    string [] filter = cigaretteQueryDialog.Filter;
                    bsMaster.DataSource = orderDal.GetPackMaster(filter);
                }
            }

            if (bsMaster.DataSource != null)
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }
    }
}

