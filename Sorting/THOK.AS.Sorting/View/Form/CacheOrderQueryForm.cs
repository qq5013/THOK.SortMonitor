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

            this.Text = this.Text + string.Format("[{0}��-{1}�Ż����-{2}����ˮ��][��������{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNo, sumQutity);

        }
        /// <summary>
        /// ����������ط�����ѯ���๵������ε����ж�������
        /// </summary>
        /// <param name="deviceNo">�豸��</param>
        /// <param name="channelGroup">ͨ����</param>
        /// <param name="sortNoStart">�׸���ˮ��</param>
        /// <param name="sumQuantity">����ξ�������</param>
        public CacheOrderQueryForm(int deviceNo, int channelGroup, int sortNoStart, int sumQuantity)
        {
            InitializeComponent();
            this.channelGroup = channelGroup;
            this.sortNoStart = sortNoStart;
            this.sumQuantity = sumQuantity;
            string strhead = "";
            DataTable Table = new DataTable();//�������������Ӧ�ֶ�

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

            DataTable orderTable = orderDal.GetAllOrderDetailForCacheOrderQuery(channelGroup, sortNoStart);
            if (orderTable.Rows.Count != 0)
            {
                int tempQuantity = 0;
                foreach (DataRow orderDetailRow in orderTable.Rows)
                {
                    int orderQuantity = Convert.ToInt32(orderDetailRow["QUANTITY"]);
                    tempQuantity = tempQuantity + orderQuantity;
                    if (tempQuantity >= sumQuantity)
                    {
                        orderDetailRow["QUANTITY"] = orderQuantity + sumQuantity - tempQuantity;//�������һ������
                        Table.Rows.Add();//����һ�в���ֵ
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

                        orderDetailRow["QUANTITY"] = tempQuantity - sumQuantity;//�������һ������
                        Table.Rows.Add();//����һ�в���ֵ
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
                        break;
                    }
                    else
                    {
                        Table.Rows.Add();//����һ�в���ֵ
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
                dgvDetail.DataSource = Table;

            }
            strhead = string.Format("[{0}�߶๵������{1}][��ˮ�ţ�{2}][��������{3}]", channelGroup == 1 ? "A" : "B", deviceNo, sortNoStart, sumQuantity);
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

            this.Text = this.Text + string.Format("[{0}�Ű�װ�������-{1}����ˮ��][��������{2}]",exportNo, sortNo, sumQutity);
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
            string cigaretteCode1 = Convert.ToString(dgvDetail.Rows[dgvDetail.Rows.Count - 2].Cells["CIGARETTECODE"]);
            string cigaretteCode2 = Convert.ToString(dgvDetail.Rows[dgvDetail.Rows.Count - 1].Cells["CIGARETTECODE"]);
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
    }
}