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
        private int sortNoStart = 0;
        private int beforeQuantity = 0;
        private int afterQuantity = 0;
        private int sumQuantity = 0;
        private int deviceNo = 0;
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
        /// <param name="beforeQuantity"></param>
        /// <param name="afterQuantity"></param>
        public CacheOrderQueryForm(int deviceNo, int channelGroup, int sortNoStart, int beforeQuantity,int afterQuantity)
        {
            InitializeComponent();
            this.deviceNo = deviceNo;
            this.channelGroup = channelGroup;
            this.sortNoStart = sortNoStart;
            this.beforeQuantity = beforeQuantity;
            this.afterQuantity = afterQuantity;
            this.sumQuantity = beforeQuantity + afterQuantity;
            string strhead = "";
            DataTable beforeTable = new DataTable();
            CreatTableForCacheOrder(beforeTable);
            DataTable afterTable = new DataTable();
            CreatTableForCacheOrder(afterTable);


            DataTable orderTable = orderDal.GetAllOrderDetailForCacheOrderQuery(channelGroup, sortNoStart);
            if (orderTable.Rows.Count != 0)
            {
                int tempQuantity = 0;
                int flag = 0;
                foreach (DataRow orderDetailRow in orderTable.Rows)
                {
                    int orderQuantity = Convert.ToInt32(orderDetailRow["QUANTITY"]);
                    tempQuantity = tempQuantity + orderQuantity;
                    if (tempQuantity <= beforeQuantity)
                    {
                        AddCacheOrderTableRow(beforeTable, orderDetailRow);
                    }
                    else
                    {
                        if (flag == 0)
                        {
                            orderDetailRow["QUANTITY"] = orderQuantity + beforeQuantity - tempQuantity;//更改最后一单数量
                            AddCacheOrderTableRow(beforeTable, orderDetailRow);
                            orderDetailRow["QUANTITY"] = tempQuantity - beforeQuantity;//补上最后一单余数
                            AddCacheOrderTableRow(beforeTable, orderDetailRow);
                            orderDetailRow["QUANTITY"] = tempQuantity - beforeQuantity;//余数归到后表
                            AddCacheOrderTableRow(afterTable, orderDetailRow);
                            flag = 1;
                        }
                        else
                        {
                            if (tempQuantity - beforeQuantity <= afterQuantity)
                            {
                                AddCacheOrderTableRow(afterTable, orderDetailRow);
                            }
                            else
                            {
                                orderDetailRow["QUANTITY"] = orderQuantity + sumQuantity - tempQuantity;//更改最后一单数量
                                AddCacheOrderTableRow(afterTable, orderDetailRow);
                                orderDetailRow["QUANTITY"] = tempQuantity - sumQuantity;//补上最后一单余数
                                AddCacheOrderTableRow(afterTable, orderDetailRow);
                                break;
                            }
                        }
                      }
                    } 
                }
                if (deviceNo == 1 || deviceNo == 3)
                {
                    dgvDetail.DataSource = afterTable;
                    strhead = string.Format("[{0}线多沟带缓存{1}][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart,beforeQuantity);
                    
                }

                if (deviceNo == 2 || deviceNo == 4)
                {
                    dgvDetail.DataSource = beforeTable;
                    strhead = string.Format("[{0}线多沟带缓存{1}][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart, afterQuantity);
                }      
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
            string cigaretteCode1 =dgvDetail.Rows[dgvDetail.Rows.Count - 2].Cells["CIGARETTECODE"].Value.ToString();
            string cigaretteCode2 =dgvDetail.Rows[dgvDetail.Rows.Count - 1].Cells["CIGARETTECODE"].Value.ToString();
            if (cigaretteCode1 == cigaretteCode2)
            {
                dgvDetail.Rows[dgvDetail.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Green;
                dgvDetail.Rows[dgvDetail.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
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
        public void CreatTableForCacheOrder(DataTable Table)
        {
            Table.Columns.Add("SORTNO");
            Table.Columns.Add("ORDERID");
            Table.Columns.Add("CIGARETTECODE");
            Table.Columns.Add("CIGARETTENAME");
            Table.Columns.Add("QUANTITY");
            Table.Columns.Add("CUSTOMERNAME");
            Table.Columns.Add("CHANNELNAME");
            Table.Columns.Add("CHANNELTYPE");
            Table.Columns.Add("CHANNELLINE");
            Table.Columns.Add("PACKNO0");
            Table.Columns.Add("PACKNO1");
            Table.Columns.Add("PACKNO2");
        }
        /// <summary>
        /// 增加行数据
        /// </summary>
        /// <param name="Table">订单表</param>
        /// <param name="orderDetailRow">订单行</param>
        public void AddCacheOrderTableRow(DataTable Table,DataRow orderDetailRow)
        {
            Table.Rows.Add();
            Table.Rows[Table.Rows.Count - 1]["SORTNO"] = orderDetailRow["SORTNO"];
            Table.Rows[Table.Rows.Count - 1]["ORDERID"] = orderDetailRow["ORDERID"];
            Table.Rows[Table.Rows.Count - 1]["CIGARETTECODE"] = orderDetailRow["CIGARETTECODE"];
            Table.Rows[Table.Rows.Count - 1]["CIGARETTENAME"] = orderDetailRow["CIGARETTENAME"];
            Table.Rows[Table.Rows.Count - 1]["QUANTITY"] = orderDetailRow["QUANTITY"];
            Table.Rows[Table.Rows.Count - 1]["CUSTOMERNAME"] = orderDetailRow["CUSTOMERNAME"];
            Table.Rows[Table.Rows.Count - 1]["CHANNELNAME"] = orderDetailRow["CHANNELNAME"];
            Table.Rows[Table.Rows.Count - 1]["CHANNELTYPE"] = orderDetailRow["CHANNELTYPE"];
            Table.Rows[Table.Rows.Count - 1]["CHANNELLINE"] = orderDetailRow["CHANNELLINE"];
            Table.Rows[Table.Rows.Count - 1]["PACKNO0"] = orderDetailRow["PACKNO0"];
            Table.Rows[Table.Rows.Count - 1]["PACKNO1"] = orderDetailRow["PACKNO1"];
            Table.Rows[Table.Rows.Count - 1]["PACKNO2"] = orderDetailRow["PACKNO2"];
        }
    }
}