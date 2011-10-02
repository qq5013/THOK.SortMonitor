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
    public partial class ChannelQueryForm : THOK.AF.View.ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();

        public ChannelQueryForm()
        {
            InitializeComponent();
            this.Column1.FilteringEnabled = true;
            this.Column2.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMain.DataSource == null)
                bsMain.DataSource = channelDal.GetChannel();
            else
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count > 0 && dgvMain.SelectedRows[0].Cells[6].Value.ToString() != "0")
            {
                string channelCode = dgvMain.SelectedRows[0].Cells["Column7"].Value.ToString();
                DataTable channeltable = channelDal.GetChannelCode(channelCode);
                if (channeltable.Rows[0]["CHANNELTYPE"].ToString() == "5")
                    return;
                DataTable table = channelDal.GetEmptyChannel(channelCode,channeltable.Rows[0]["CHANNELTYPE"].ToString(), Convert.ToInt32(channeltable.Rows[0]["CHANNELGROUP"].ToString()));
                    
                if (table.Rows.Count != 0)
                {
                    ChannelDialog channelDailog = new ChannelDialog(table);
                    if (channelDailog.ShowDialog() == DialogResult.OK)
                    {
                        int sourceChannelAddress = 0;
                        int targetChannelAddress = 0;

                        if (channelDal.ExechangeChannel(channelCode, channelDailog.SelectedChannelCode,out sourceChannelAddress,out targetChannelAddress))
                        {
                            int [] data = new int [3];
                            data[0] = sourceChannelAddress;
                            data[1] = targetChannelAddress;
                            data[2] = 1;
                            if (Convert.ToInt32(channeltable.Rows[0]["CHANNELGROUP"].ToString()) == 1)
                                this.mainFrame.Context.ProcessDispatcher.WriteToService("SortPLC", "ChannelChangeDataA", data);
                            else
                                this.mainFrame.Context.ProcessDispatcher.WriteToService("SortPLC", "ChannelChangeDataB", data);
                            THOK.MCP.Logger.Info(string.Format("{0}线{1}号烟道与{2}号烟道交换！",Convert.ToInt32(channeltable.Rows[0]["CHANNELGROUP"]) == 1?"A":"B", sourceChannelAddress, targetChannelAddress));
                        }
                        DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
                        bsMain.DataSource = channelDal.GetChannel();
                    }
                }
                else
                    MessageBox.Show("无法调整烟道，原因：没有可用烟道。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

