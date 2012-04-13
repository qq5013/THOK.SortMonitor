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
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                lock (this)
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
                                if (ExportTable1.Rows.Count > 0 && !IsPackNoUpload1)
                                {
                                    foreach (DataRow inRow in ExportTable1.Rows)
                                    {
                                        int CustomerSumQuantity1 = orderDao.FindCustomerQuantity(Convert.ToInt32(inRow["PACKNO"].ToString()));
                                        int BagSumQuantity1 = orderDao.FindBagQuantity(Convert.ToInt32(inRow["PACKNO"].ToString()));
                                        orderDao.InsertPackExport(inRow, 1, CustomerSumQuantity1, BagSumQuantity1);
                                    }
                                }
                                if (ExportTable2.Rows.Count > 0 && !IsPackNoUpload2)
                                {
                                    foreach (DataRow inRow2 in ExportTable2.Rows)
                                    {
                                        int CustomerSumQuantity2 = orderDao.FindCustomerQuantity(Convert.ToInt32(inRow2["PACKNO"].ToString()));
                                        int BagSumQuantity2 = orderDao.FindBagQuantity(Convert.ToInt32(inRow2["PACKNO"].ToString()));
                                        orderDao.InsertPackExport(inRow2, 2, CustomerSumQuantity2, BagSumQuantity2);
                                    }
                                }
                                pm.Commit();
                                WriteToService("SortPLC", "ExportPackNoWrite", ExportPackNo);
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
