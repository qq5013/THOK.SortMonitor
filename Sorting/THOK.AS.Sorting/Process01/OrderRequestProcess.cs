using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.MCP;
using THOK.AS.Sorting.Dao;
using THOK.Util;
using THOK.AS.Sorting.Util;

namespace THOK.AS.Sorting.Process
{
    public class OrderRequestProcess: AbstractProcess
    {
        private MessageUtil messageUtil = null;

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                messageUtil = new MessageUtil(context.Attributes);
            }
            catch (Exception e)
            {
                Logger.Error("OrderRequestProcess 初始化失败！原因：" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string channelGroup = "";

                switch (stateItem.ItemName)
                {
                    case "OrderRequestA":
                        channelGroup = "A";
                        break;
                    case "OrderRequestB":
                        channelGroup = "B";
                        break;
                    default:
                        return;
                }
              
                //读取A线订单明细并写给PLC
                object o = ObjectUtil.GetObject(stateItem.State);

                if (o != null && o.ToString() == "1")
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();
                        try
                        {
                            //要根据分拣线组查询数据
                            DataTable masterTable = orderDao.FindSortMaster(channelGroup);

                            if (masterTable.Rows.Count != 0)
                            {
                                //当前流水号
                                string sortNo = masterTable.Rows[0]["SORTNO"].ToString();
                                //获取这个订单ID的流水号，判断是否换户
                                string maxSortNo = orderDao.FindMaxSortNoFromMasterByOrderID(masterTable.Rows[0]["ORDERID"].ToString(), channelGroup);
                                //查询订单明细                                
                                DataTable detailTable = orderDao.FindSortDetail(sortNo, channelGroup);
                                //查询本分拣线组最后流水号，判断是否结束
                                string endSortNo = orderDao.FindEndSortNoForChannelGroup(channelGroup);
                                int exportNo = Convert.ToInt32(masterTable.Rows[0]["EXPORTNO" + (channelGroup == "A" ? "" : "1")]);

                                int[] orderData = new int[37];
                                if (detailTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i < detailTable.Rows.Count; i++)
                                    {
                                        orderData[Convert.ToInt32(detailTable.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(detailTable.Rows[i]["QUANTITY"]);
                                    }
                                }

                                //分拣流水号
                                orderData[30] = Convert.ToInt32(sortNo);
                                //订单数量
                                orderData[31] = Convert.ToInt32(masterTable.Rows[0]["QUANTITY" + (channelGroup == "A" ? "" : "1")]);
                                //是否换户
                                orderData[32] = maxSortNo == sortNo ? 1 : 0;
                                //客户分拣流水号
                                orderData[33] = Convert.ToInt32(masterTable.Rows[0]["CUSTOMERSORTNO"].ToString());
                                //包装机号
                                orderData[34] = exportNo;
                                //本分拣线路是否结束
                                orderData[35] = endSortNo == sortNo ? 1 : 0;
                                //完成标志
                                orderData[36] = 1;
                                if (WriteToService("SortPLC", "OrderData" + channelGroup, orderData))
                                {
                                    orderDao.UpdateOrderStatus(sortNo, "1", channelGroup);
                                    Logger.Info(string.Format(channelGroup + " 线 写订单数据成功,分拣订单号[{0}]。", sortNo));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(string.Format(channelGroup + " 线 写订单数据失败，原因：{0}！ {1}", e.Message, "OrderRequestProcess.cs 行号：100！"));
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(string.Format("分拣订单请求操作失败！原因：{0}！ {1}", ee.Message, "OrderRequestProcess.cs 行号：108！"));
            }
        }
    }
}
