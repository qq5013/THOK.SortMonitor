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
        public bool IsPackNoUpload(int packNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable exportPack1 = orderDao.FindexportPack(1);
                DataTable exportPack2 = orderDao.FindexportPack(2);
                foreach (DataRow exportPackrow1 in exportPack1.Rows)
                {
                    int exportPackNo1 = Convert.ToInt32(exportPackrow1["PACKNO"].ToString());
                    if (exportPackNo1 == packNo)
                        return true;
                    else
                        continue;
                }
                foreach (DataRow exportPackrow2 in exportPack2.Rows)
                {
                    int exportPackNo2 = Convert.ToInt32(exportPackrow2["PACKNO"].ToString());
                    if (exportPackNo2 == packNo)
                        return true;
                    else
                        continue;
                }
                return false;
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
                            int ExportPackNo1 = ExportPackNo[0];
                            int ExportPackNo2 = ExportPackNo[1];
                            bool GreenLight = true;
                            using (PersistentManager pm = new PersistentManager())
                            {
                                pm.BeginTransaction();
                                OrderDao orderDao = new OrderDao();
                                DataTable ExportTable1 = new DataTable();
                                ExportTable1 = orderDao.packOrderToExport(ExportPackNo1);
                                DataTable ExportTable2 = new DataTable();
                                ExportTable2 = orderDao.packOrderToExport(ExportPackNo2);
                                bool IsPackNoUpload1 = IsPackNoUpload(ExportPackNo1);
                                bool IsPackNoUpload2 = IsPackNoUpload(ExportPackNo2);
                                if (ExportTable1.Rows.Count > 0 && !IsPackNoUpload1 && orderDao.FindCountDataByPackNo(1, ExportPackNo1) == 0)
                                {
                                    int CountData1 = ExportTable1.Rows.Count;
                                    foreach (DataRow inRow in ExportTable1.Rows)
                                    {
                                        int CustomerSumQuantity1 = orderDao.FindCustomerQuantity(Convert.ToInt32(inRow["PACKNO"].ToString()));
                                        int BagSumQuantity1 = orderDao.FindBagQuantity(Convert.ToInt32(inRow["PACKNO"].ToString()));
                                        orderDao.InsertPackExport(inRow, 1, CustomerSumQuantity1, BagSumQuantity1);
                                    }
                                    int DataCount1 = orderDao.FindCountDataByPackNo(1, ExportPackNo1);
                                    if (CountData1 != DataCount1)
                                    {
                                        GreenLight = false;
                                        Logger.Error("包号数据行数不符合，包号为"+ExportPackNo1.ToString());
                                    }
                                    string packNo = ExportPackNo1.ToString();
                                    messageUtil.SendToExport1(packNo);
                                }
                                if (ExportTable2.Rows.Count > 0 && !IsPackNoUpload2 && orderDao.FindCountDataByPackNo(2, ExportPackNo2) == 0)
                                {
                                    int CountData2 = ExportTable2.Rows.Count;
                                    foreach (DataRow inRow2 in ExportTable2.Rows)
                                    {
                                        int CustomerSumQuantity2 = orderDao.FindCustomerQuantity(Convert.ToInt32(inRow2["PACKNO"].ToString()));
                                        int BagSumQuantity2 = orderDao.FindBagQuantity(Convert.ToInt32(inRow2["PACKNO"].ToString()));
                                        orderDao.InsertPackExport(inRow2, 2, CustomerSumQuantity2, BagSumQuantity2);
                                    }
                                    int DataCount2 = orderDao.FindCountDataByPackNo(2, ExportPackNo2);
                                    if (CountData2 != DataCount2)
                                    {
                                        GreenLight = false;
                                        Logger.Error("包号数据行数不符合，包号为" + ExportPackNo2.ToString());
                                    }
                                    string packNo = ExportPackNo1.ToString();
                                    messageUtil.SendToExport2(packNo);
                                }
                                pm.Commit();
                                if (GreenLight)
                                {
                                    WriteToService("SortPLC", "ExportPackNoWrite", ExportPackNo);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("包号获取传输错误！原因：" + e.Message);
            }
        }
    }
}
