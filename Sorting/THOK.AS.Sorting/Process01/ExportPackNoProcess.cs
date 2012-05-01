using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.AS.Sorting.Dao;
using THOK.Util;
using THOK.AS.Sorting.Util;

namespace THOK.AS.Sorting.Process
{
    class ExportPackNoProcess : AbstractProcess
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
                Logger.Error("ExportPackNoProcess 初始化失败！原因：" + e.Message);
            }
        }

        private static string processlock = "";
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                lock (processlock)
                {
                    int[] ExportPackNo = new int[2];
                    object stateExportPackNo = Context.Services["SortPLC"].Read("ExportPackNoRead");
                    if (stateExportPackNo is Array)
                    {
                        Array arrayExportPackNo = (Array)stateExportPackNo;
                        if (arrayExportPackNo.Length == 2)
                        {
                            arrayExportPackNo.CopyTo(ExportPackNo, 0);
                            int exportPackNo1 = ExportPackNo[0];
                            int exportPackNo2 = ExportPackNo[1];

                            using (PersistentManager pm = new PersistentManager())
                            {
                                pm.BeginTransaction();
                                OrderDao orderDao = new OrderDao();
                                DataTable ExportTable1 = new DataTable();
                                ExportTable1 = orderDao.packOrderToExport(exportPackNo1);
                                DataTable ExportTable2 = new DataTable();
                                ExportTable2 = orderDao.packOrderToExport(exportPackNo2);

                                if (ExportTable1.Rows.Count > 0)
                                {
                                    foreach (DataRow orderRow in ExportTable1.Rows)
                                    {
                                        int CustomerSumQuantity = orderDao.FindCustomerQuantity(Convert.ToInt32(orderRow["PACKNO"].ToString()));
                                        int BagSumQuantity = orderDao.FindBagQuantity(Convert.ToInt32(orderRow["PACKNO"].ToString()));
                                        int packOrderMaxId = orderDao.GetPackOrderMaxId(1);
                                        orderDao.InsertPackExport(orderRow, 1, CustomerSumQuantity, BagSumQuantity, packOrderMaxId);
                                    }
                                    string packNo = exportPackNo1.ToString();
                                    messageUtil.SendToExport1(packNo);
                                }
                                if (ExportTable2.Rows.Count > 0)
                                {
                                    foreach (DataRow orderRow in ExportTable2.Rows)
                                    {
                                        int CustomerSumQuantity = orderDao.FindCustomerQuantity(Convert.ToInt32(orderRow["PACKNO"].ToString()));
                                        int BagSumQuantity = orderDao.FindBagQuantity(Convert.ToInt32(orderRow["PACKNO"].ToString()));
                                        int packOrderMaxId = orderDao.GetPackOrderMaxId(2);
                                        orderDao.InsertPackExport(orderRow, 2, CustomerSumQuantity, BagSumQuantity, packOrderMaxId);
                                    }
                                    string packNo = exportPackNo1.ToString();
                                    messageUtil.SendToExport2(packNo);
                                }
                                pm.Commit();

                                List<int> routeMaxPackNoList = new List<int>();
                                routeMaxPackNoList = orderDao.FindRouteMaxPackNoList();
                                if (routeMaxPackNoList.Contains(exportPackNo1) || routeMaxPackNoList.Contains(exportPackNo2))
                                {
                                    WriteToService("SortPLC", "RouteChannageTag", 1);
                                }

                                WriteToService("SortPLC", "ExportPackNoWrite", ExportPackNo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                THOK.MCP.Logger.Info(System.Threading.Thread.CurrentThread.Name);
                Logger.Error("包号获取传输错误！原因：" + e.Message);
            }
        }
    }
}
