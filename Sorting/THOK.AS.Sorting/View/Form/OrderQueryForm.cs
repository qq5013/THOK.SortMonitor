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
    public partial class OrderQueryForm : THOK.AF.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();

        public OrderQueryForm()
        {
            InitializeComponent();
            this.Column2.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
            this.Column8.FilteringEnabled = true;
            this.Column9.FilteringEnabled = true;
            this.Column10.FilteringEnabled = true;
            this.PACKQUANTITY1.FilteringEnabled = true;
            this.STATUS1.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMaster.DataSource == null)
            {
                bsMaster.DataSource = orderDal.GetOrderMaster();
            }
            else
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = orderDal.GetOrderDetail(dgvMaster.Rows[e.RowIndex].Cells[2].Value.ToString());
        }
    }
}

