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
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
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
                            OrderDao orderDao = new OrderDao();
                            DataTable ExportTable1 = new DataTable();
                            ExportTable1 = orderDao.packOrderToExport1(ExportPackNo1);
                            DataTable ExportTable2 = new DataTable();
                            ExportTable2 = orderDao.packOrderToExport2(ExportPackNo2);
                            if (ExportTable1.Rows.Count > 0)
                            {
                                foreach (DataRow inRow in ExportTable1.Rows)
                                {
                                    int Customer1 = orderDao.FindCustomerSortNo(inRow["CUSTOMERCODE"].ToString());
                                    orderDao.InsertPackExport(inRow, 1,Customer1);
                                }
                            }
                            if (ExportTable2.Rows.Count >= 1)
                            {
                                foreach (DataRow inRow2 in ExportTable2.Rows)
                                {
                                    int Customer2 = orderDao.FindCustomerSortNo(inRow2["CUSTOMERCODE"].ToString());
                                    orderDao.InsertPackExport(inRow2, 2,Customer2);
                                }
                            }

                            WriteToService("SortPLC", "ExportPackNoWrite", ExportPackNo);
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
