using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.MCP;
using THOK.AS.Sorting.View;
using THOK.AS.Sorting.Dao;
using THOK.Util;

namespace THOK.AS.Sorting.Process
{
    public class PackRequestProcess: AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                if (stateItem.State.ToString() == "1")
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();
                        DataTable table = orderDao.FindPackInfo();
                        if (table.Rows.Count != 0)
                        {
                            int[] packageData = new int[table.Rows.Count];

                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                packageData[i] = Convert.ToInt32(table.Rows[i]["QUANTITY"]);
                            }

                            if (dispatcher.WriteToService("PackPLC", "PackageData", packageData))
                            {
                                if (dispatcher.WriteToService("PackPLC", "OrderCount", table.Rows.Count))
                                {
                                    if (dispatcher.WriteToService("PackPLC", "WriteFlag", 1))
                                    {
                                        try
                                        {
                                            pm.BeginTransaction();
                                            foreach (DataRow row in table.Rows)
                                                orderDao.UpdatePackQuantityByOrderID(row["ORDERID"].ToString());
                                            pm.Commit();
                                            Logger.Info(string.Format("写包装数据成功,共 {0} 个订单。第一单数量：{1} 条。", table.Rows.Count, packageData[0]));
                                        }
                                        catch (Exception e)
                                        {
                                            pm.Rollback();
                                            Logger.Error("写包装数据失败，原因：" + e.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("包装订单请求操作失败！原因：" + ee.Message);
            }

        }
    }
}
