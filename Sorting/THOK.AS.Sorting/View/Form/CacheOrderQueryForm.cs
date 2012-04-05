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
        #region 缓存段参数
        private int sortNo = 0;
        private int deviceNo = 0;
        private int sortNoStart = 0;
        private int frontQuantity = 0;
        private int laterQuantity = 0;
        private int sumQuantity = 0;
        private int channelGroup = 0;
        private OrderDal orderDal = new OrderDal();
        #endregion

        #region 打码段和摆动段数据显示
        /// <summary>
        /// 打码段和摆动段数据显示
        /// </summary>
        /// <param name="deviceNo">设备号</param>
        /// <param name="channelGroup">烟道组号</param>
        /// <param name="sortNo">分拣流水号</param>
        public CacheOrderQueryForm(int deviceNo,int channelGroup, int sortNo)
        {
            InitializeComponent();
            string cacheName = "";
            int sumQutity = 0;
            if (deviceNo == 5)
            {
                cacheName = "打码段";
            }
            else if (deviceNo == 6)
            {
                cacheName = "摆动段";
            }
            else if (deviceNo == 7)
            {
                cacheName = "1号包装机";
            }
            else if (deviceNo == 8)
            {
                cacheName = "2号包装机";
            }
            this.sortNo = sortNo;
            this.channelGroup = channelGroup;
            DataTable table = orderDal.GetOrderDetailForCacheOrderQuery(channelGroup, sortNo);
            if (table.Rows.Count != 0)
            {
                dgvDetail.DataSource = table;
                sumQutity = Convert.ToInt32(table.Compute("SUM(QUANTITY)", ""));
            }
            this.Text = this.Text + string.Format("[{0}线{1}缓存段][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", cacheName, sortNo, sumQutity);

        }
        #endregion

        #region 多沟带缓存段数据显示
        /// <summary>
        /// 窗体加载重载方法查询出多沟带缓存段的所有订单数据
        /// </summary>
        /// <param name="deviceNo">设备号</param>
        /// <param name="channelGroup">烟道组号</param>
        /// <param name="sortNoStart">开始流水号</param>
        /// <param name="beforeQuantity">小皮带数量</param>
        /// <param name="afterQuantity">后端数量</param>
        public CacheOrderQueryForm(int deviceNo, int channelGroup, int sortNoStart, int beforeQuantity,int afterQuantity)
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

            if (deviceNo == 2 || deviceNo == 4)
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

                            orderDetailRow["QUANTITY"] = tempQuantity - frontQuantity;
                            AddCacheOrderTableRow(Table, orderDetailRow);
                            break;
                        }
                        else
                        {
                            AddCacheOrderTableRow(Table, orderDetailRow);
                        }
                    }
                    strhead = string.Format("[{0}线多沟带前端小皮带][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart, frontQuantity);
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
                    strhead = string.Format("[{0}线多沟带后端缓存段][流水号：{2}][总数量：{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart, laterQuantity);
                }
            }
            dgvDetail.DataSource = Table;
            this.Text = this.Text + strhead;
            }
        #endregion

        #region 包装机缓存段数据显示(未完成)

        /// <summary>
        /// 包装机缓存段数据显示
        /// </summary>
        /// <param name="exportNo">包装机号</param>
        /// <param name="deviceNo">设备号（仅作为方法重载区分字段，没太大意义）</param>
        /// <param name="sortNo">流水号</param>
        /// <param name="channelGroup">烟道组号</param>
        public CacheOrderQueryForm(int exportNo, int deviceNo, int sortNo, int channelGroup)
        {
            InitializeComponent();
            this.sortNo = sortNo;
            this.channelGroup = channelGroup;
        }
        #endregion

        #region 实时包装前包装段
        /// <summary>
        /// 实时包装前，包装段缓存数据显示
        /// </summary>
        /// <param name="packMode"></param>
        /// <param name="exportNo"></param>
        /// <param name="sortNo"></param>
        /// <param name="channelGroup"></param>
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
        #endregion

        #region 窗体颜色加载
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
        #endregion

        #region 缓存段表列创建及行添加
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
        #endregion

    }
}