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
        private int deviceNo = 0;
        private int sortNoStart = 0;
        private int frontQuantity = 0;
        private int laterQuantity = 0;
        private int sumQuantity = 0;
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
        /// <summary>
        /// 窗体加载重载方法查询出多沟带缓存段的所有订单数据
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="channelGroup"></param>
        /// <param name="sortNoStart"></param>
        /// <param name="frontQuantity"></param>
        /// <param name="laterQuantity"></param>
        public CacheOrderQueryForm(int deviceNo, int channelGroup, int sortNoStart,int frontQuantity,int laterQuantity)
        {
            InitializeComponent();
            this.deviceNo = deviceNo;
            this.channelGroup = channelGroup;
            this.sortNoStart = sortNoStart;
            this.frontQuantity = frontQuantity;
            this.laterQuantity = laterQuantity;
            this.sumQuantity = frontQuantity + laterQuantity;
            string strhead = "";

            DataTable orderTable = orderDal.GetAllOrderDetailForCacheOrderQuery(channelGroup, sortNoStart);

            DataTable Table = new DataTable();
            CreatTableForCacheOrder(Table);

            if (deviceNo == 2 || deviceNo== 4)
            {
                if (orderTable.Rows.Count != 0)
                {
                    int tempQuantity = 0;
                    foreach (DataRow orderDetailRow in orderTable.Rows)
                    {
                        int orderQuantity = Convert.ToInt32(orderDetailRow["QUANTITY"]);
                        tempQuantity = tempQuantity + orderQuantity;

                        if (tempQuantity >= frontQuantity)
                        {
                            orderDetailRow["QUANTITY"] = orderQuantity + frontQuantity - tempQuantity;
                            AddCacheOrderTableRow(Table, orderDetailRow);

                            orderDetailRow["QUANTITY"]= tempQuantity - frontQuantity;
                            AddCacheOrderTableRow(Table, orderDetailRow);
                            break;
                        }
                        else
                        {
                            AddCacheOrderTableRow(Table, orderDetailRow);
                        }
                    }
                    strhead = string.Format("[{0}线多沟带缓存{1}][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart, frontQuantity);
                }

            }
            else
            {
                if (orderTable.Rows.Count != 0)
                {
                    int tempQuantity = 0;
                    bool flag = false;
                    foreach (DataRow orderDetailRow in orderTable.Rows)
                    {
                        int orderQuantity = Convert.ToInt32(orderDetailRow["QUANTITY"]);
                        tempQuantity = tempQuantity + orderQuantity;

                        if (flag == false)
                        {
                            if (tempQuantity >= frontQuantity)
                            {
                                if (laterQuantity != 0)
                                {
                                    orderDetailRow["QUANTITY"] = tempQuantity - frontQuantity;
                                    AddCacheOrderTableRow(Table, orderDetailRow);
                                    flag = true;
                                }
                                else
                                {
                                    orderDetailRow["QUANTITY"] = orderQuantity + frontQuantity - tempQuantity;
                                    AddCacheOrderTableRow(Table, orderDetailRow);

                                    orderDetailRow["QUANTITY"] = tempQuantity - frontQuantity;
                                    AddCacheOrderTableRow(Table, orderDetailRow);
                                    break;
                                }
                                
                            }
                        }
                        else
                        {
                            if (tempQuantity >= sumQuantity)
                            {
                                orderDetailRow["QUANTITY"] = orderQuantity + sumQuantity - tempQuantity;
                                AddCacheOrderTableRow(Table, orderDetailRow);

                                orderDetailRow["QUANTITY"] = tempQuantity - sumQuantity;
                                AddCacheOrderTableRow(Table, orderDetailRow);
                                break;

                            }
                            else
                            { 
                                AddCacheOrderTableRow(Table, orderDetailRow);
                            }
                        }       
                    }
                    strhead = string.Format("[{0}线多沟带缓存{1}][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart,laterQuantity);
                }
            }
            dgvDetail.DataSource = Table;
            this.Text = this.Text + strhead;
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

        public void LoadColor()
        {
            if (dgvDetail.Rows.Count == 2)
            {
                string cigaretteCode1 = dgvDetail.Rows[dgvDetail.Rows.Count - 2].Cells["CIGARETTECODE"].Value.ToString();
                string cigaretteCode2 = dgvDetail.Rows[dgvDetail.Rows.Count - 1].Cells["CIGARETTECODE"].Value.ToString();
                int quantity = Convert.ToInt32(dgvDetail.Rows[dgvDetail.Rows.Count - 1].Cells["CIGARETTECODE"].Value);
                if (cigaretteCode1 == cigaretteCode2 && quantity != 0)
                {
                    dgvDetail.Rows[dgvDetail.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Blue;
                    dgvDetail.Rows[dgvDetail.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                }
                if (cigaretteCode1 == cigaretteCode2 && quantity == 0)
                {
                    foreach (DataGridViewRow row in dgvDetail.Rows)
                    {
                        int quantity1 = Convert.ToInt32(row.Cells["QUANTITY"].Value);
                        if (quantity1 == 0)
                        {
                            dgvDetail.Rows.Remove(row);
                        }
                    }
                }
            }
            if (dgvDetail.Rows.Count >= 3)
            {
                string cigaretteCode1 = dgvDetail.Rows[dgvDetail.Rows.Count - 2].Cells["CIGARETTECODE"].Value.ToString();
                string cigaretteCode2 = dgvDetail.Rows[dgvDetail.Rows.Count - 1].Cells["CIGARETTECODE"].Value.ToString();
                if (cigaretteCode1 == cigaretteCode2)
                {
                    dgvDetail.Rows[dgvDetail.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Blue;
                    dgvDetail.Rows[dgvDetail.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                }
                foreach (DataGridViewRow row in dgvDetail.Rows)
                {
                    int quantity = Convert.ToInt32(row.Cells["QUANTITY"].Value);
                    if (quantity == 0)
                    {
                        dgvDetail.Rows.Remove(row);
                    }
                }
            }
        }

        public void CacheOrderQueryForm_Paint(object sender, PaintEventArgs e)
        {
            LoadColor(this.sortNo, this.channelGroup);
        }

        public void CacheOrderQueryFormPaint(object send, PaintEventArgs e)
        {
            LoadColor();
        }
        /// <summary>
        /// 添加虚拟表并添加相应字段
        /// </summary>
        /// <returns>订单表</returns>
        public void CreatTableForCacheOrder(DataTable table)
        {
            table.Columns.Add("SORTNO");
            table.Columns.Add("ORDERID");
            table.Columns.Add("CIGARETTECODE");
            table.Columns.Add("CIGARETTENAME");
            table.Columns.Add("QUANTITY");
            table.Columns.Add("CUSTOMERNAME");
            table.Columns.Add("CHANNELNAME");
            table.Columns.Add("CHANNELTYPE");
            table.Columns.Add("CHANNELLINE");
            table.Columns.Add("PACKNO0");
            table.Columns.Add("PACKNO1");
            table.Columns.Add("PACKNO2");
        }
        /// <summary>
        /// 增加行数据
        /// </summary>
        /// <param name="Table">订单表</param>
        /// <param name="orderDetailRow">订单行</param>
        public void AddCacheOrderTableRow(DataTable table, DataRow row)
        {
            table.Rows.Add();
            table.Rows[table.Rows.Count - 1]["SORTNO"] = row["SORTNO"];
            table.Rows[table.Rows.Count - 1]["ORDERID"] = row["ORDERID"];
            table.Rows[table.Rows.Count - 1]["CIGARETTECODE"] = row["CIGARETTECODE"];
            table.Rows[table.Rows.Count - 1]["CIGARETTENAME"] = row["CIGARETTENAME"];
            table.Rows[table.Rows.Count - 1]["QUANTITY"] = row["QUANTITY"];
            table.Rows[table.Rows.Count - 1]["CUSTOMERNAME"] = row["CUSTOMERNAME"];
            table.Rows[table.Rows.Count - 1]["CHANNELNAME"] = row["CHANNELNAME"];
            table.Rows[table.Rows.Count - 1]["CHANNELTYPE"] = row["CHANNELTYPE"];
            table.Rows[table.Rows.Count - 1]["CHANNELLINE"] = row["CHANNELLINE"];
            table.Rows[table.Rows.Count - 1]["PACKNO0"] = row["PACKNO0"];
            table.Rows[table.Rows.Count - 1]["PACKNO1"] = row["PACKNO1"];
            table.Rows[table.Rows.Count - 1]["PACKNO2"] = row["PACKNO2"];
        }
    }
}