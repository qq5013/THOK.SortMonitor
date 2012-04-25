using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using THOK.MCP;
using System.Windows.Forms;
using THOK.AS.Sorting.Dal;

namespace THOK.AS.Sorting.View
{
    public partial class CustomerOrderQueryForm : THOK.AF.View.ToolbarForm
    {
        private DataTable masterTable = null;
        private DataTable detailTable = null;


        public Context context = new Context();
        private OrderDal orderDal = new OrderDal();
        public CustomerOrderQueryForm()
        {
            InitializeComponent();
            context = new Context();

            try
            {
                ContextInitialize initialize = new ContextInitialize();
                initialize.InitializeContext(context);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            masterTable = orderDal.GetPackMaster();
            detailTable = orderDal.GetPackDetail();
            dgvMaster.DataSource = masterTable;
            dgvDetail.DataSource = detailTable;
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
                    masterTable = orderDal.GetPackMaster(filter);
                    dgvMaster.DataSource = masterTable;
                }
            }
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detailTable.DefaultView.RowFilter = string.Format("ORDERID = '{0}'", dgvMaster.Rows[e.RowIndex].Cells[3].Value);
        }
    }
}

