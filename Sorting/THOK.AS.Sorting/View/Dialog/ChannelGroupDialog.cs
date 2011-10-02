using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.AS.Sorting.View
{
    public partial class ChannelGroupDialog : Form
    {
        public string ChannelGroup
        {
            get { return cmbChannelNo.SelectedItem.ToString(); }
        }

        public ChannelGroupDialog()
        {
            InitializeComponent();
            cmbChannelNo.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
           ¡¡DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}