using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Sorting.Dao;
using THOK.AS.Sorting.Util;
using System.Data;
using THOK.AS.Sorting.Dal;
using System.IO;

namespace THOK.AS.Sorting.Process
{
    class CreatePackAndPrintDataProcess : AbstractProcess
    {
        private void DeleteFiles()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Context.Attributes["PackDataPath"].ToString() != "" ? Context.Attributes["PackDataPath"].ToString() : @"D:/PACKDATA/");

                if (!dir.Exists)
                    dir.Create();

                FileInfo[] files = dir.GetFiles("*.*");

                if (files != null)
                {
                    foreach (FileInfo file in files)
                        file.Delete();
                }

            }
            catch (Exception)
            {
            }
        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            int exportNo = 0;
            //顺序号
            int orderNo = 1;
            //当前客户包序号及总包数
            int packCount = 1;

            //单独包装机包装参数信息            
            int packNo = 1;
            //一号包装机包装参数信息
            int packNo1 = 1;
            //二号包装机包装参数信息
            int packNo2 = 2;
            
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                
                try
                {
                    pm.BeginTransaction();
                    orderDao.DeletePackData();
                    DeleteFiles();

                    //一号包装机处理
                    //二号包装机处理
                    DataTable orderMasterTable = orderDao.FindOrderMaster();
                    DataTable orderDetailTable;
                    DataRow[] orderDetailRows;

                    if (orderMasterTable.Rows.Count > 0)
                    {
                        orderNo = 1;
                        foreach (DataRow orderMasterRow in orderMasterTable.Rows)
                        {
                            string orderId = orderMasterRow["ORDERID"].ToString();
                            orderDetailTable = orderDao.FindOrderDetailForPack(orderId, "1,2");

                            packCount = 1;

                            //一号包装机处理
                            exportNo = 1;
                            orderDetailRows = orderDetailTable.Select(string.Format("ORDERID = '{0}' AND EXPORTNO = '1'", orderId), "ORDERNO_PACKNO,CHANNELGROUP DESC,SORTNO,CHANNELADDRESS");
                            SplitOrder(orderDao, orderMasterRow, orderDetailRows, ref orderNo, ref  packNo1, exportNo, ref packCount);

                            //二号包装机处理
                            exportNo = 2;
                            orderDetailRows = orderDetailTable.Select(string.Format("ORDERID = '{0}' AND EXPORTNO = '2'", orderId), "ORDERNO_PACKNO,CHANNELGROUP DESC,SORTNO,CHANNELADDRESS");
                            SplitOrder(orderDao, orderMasterRow, orderDetailRows, ref orderNo, ref packNo2, exportNo, ref packCount);
                            
                            //更新总包数[packCount-1]
                            orderDao.UpdatePackCount(orderMasterRow["ORDERID"].ToString(), 1, packCount - 1);
                            orderDao.UpdatePackCount(orderMasterRow["ORDERID"].ToString(), 2, packCount - 1);
                        }                       
                    }

                    orderDao.UpdatePackOrderStatus(1);
                    orderDao.UpdatePackOrderStatus(2);
                    CreatePackAndPrintDataFile(1);
                    CreatePackAndPrintDataFile(2);
                    Logger.Info("01号包装机数据生成成功！");                    
                    Logger.Info("02号包装机数据生成成功！");

                    //单独包装机处理                    
                    if (orderMasterTable.Rows.Count > 0)
                    {
                        orderNo = 1;
                        exportNo = 0;
                        foreach (DataRow orderMasterRow in orderMasterTable.Rows)
                        {
                            string orderId = orderMasterRow["ORDERID"].ToString();
                            orderDetailTable = orderDao.FindOrderDetailForPack(orderId, "1,2");

                            packCount = 1;

                            //单独包装机处理                            
                            orderDetailRows = orderDetailTable.Select(string.Format("ORDERID = '{0}' ", orderId), "ORDERNO_PACKNO,CHANNELGROUP DESC,SORTNO,CHANNELADDRESS");
                            SplitOrder(orderDao, orderMasterRow, orderDetailRows, ref orderNo, ref packNo, exportNo, ref packCount);
                            
                            //更新总包数[packCount-1]
                            orderDao.UpdatePackCount(orderMasterRow["ORDERID"].ToString(),0,packCount - 1);
                        }                        
                    }

                    orderDao.UpdatePackOrderStatus(0);
                    CreatePackAndPrintDataFile(0);

                    Logger.Info("单独包装机数据生成成功！");

                    pm.Commit();                    
                }
                catch (Exception e)
                {                    
                    pm.Rollback();
                    Logger.Error("包装机数据生成失败！原因：" + e.Message);
                }                
            }
        }

        private void SplitOrder(OrderDao orderDao, DataRow orderMasterRow, DataRow[] orderDetailRows, ref int orderNo, ref int packNo, int exportNo, ref int packCount)
        {
            int maxPackQuantity = 25;
            int tempQuantity = 0;            
            int splitQuantity = 0;
            int sumQuantity = Convert.ToInt32(orderMasterRow["QUANTITY" + exportNo]);

            if (orderDetailRows.Length > 0)
            {
                foreach (DataRow orderDetailRow in orderDetailRows)
                {
                    while (true)
                    {
                        int orderQuantity = Convert.ToInt32(orderDetailRow["QUANTITY"]);
                        if (orderQuantity == 0)
                        {
                            break;
                        }

                        if (tempQuantity % maxPackQuantity == 0 && sumQuantity > 25 && sumQuantity < 30 )
                        {
                            tempQuantity = 0;
                            maxPackQuantity = 20;
                        }

                        int limitQuantity = maxPackQuantity - tempQuantity % maxPackQuantity;
                        splitQuantity = orderQuantity <= limitQuantity ? orderQuantity : limitQuantity;

                        tempQuantity += splitQuantity;
                        orderDetailRow["QUANTITY"] = orderQuantity - splitQuantity;
                        sumQuantity -= splitQuantity;

                        //组号［channelGroup］，分拣流水号［sortNo］
                        //顺序号［orderNo］，分拣号［packNo］，送货顺序［orderNo］
                        //客户相关信息
                        //卷烟代码，卷烟名称，数量 
                        //保存到数据库

                        orderDao.InsertPackOrder(orderMasterRow, orderDetailRow, splitQuantity, orderNo, packNo, exportNo, packCount);

                        orderNo++;
                        if (tempQuantity % maxPackQuantity == 0 )
                        {
                            packCount++;
                            packNo = exportNo == 0 ? packNo + 1 : packNo + 2;                            
                        }
                    }
                }

                if (tempQuantity % maxPackQuantity > 0)
                {
                    packCount++;
                    packNo = exportNo == 0 ? packNo + 1 : packNo + 2; 
                }
            }
        }

        private void CreatePackAndPrintDataFile(int exportNo)
        {
            try
            {
                string packDataPath = Context.Attributes["PackDataPath"].ToString() != "" ? Context.Attributes["PackDataPath"].ToString() : @"D:/PACKDATA/";
                string packDataFileName = System.DateTime.Now.ToShortDateString() + " [" + (exportNo == 0 ? "All" : (exportNo == 1 ? "ONE" : "TWO")) + "].ORDER";
                string s = "";

                FileStream file = new FileStream(packDataPath + packDataFileName, FileMode.Create);

                StreamWriter writer = new StreamWriter(file, Encoding.Default);

                OrderDal orderDal = new OrderDal();
                DataTable table = orderDal.GetPackDataOrder(exportNo);
                int columnCount = table.Columns.Count;

                foreach (DataRow row in table.Rows)
                {
                    s = row["ORDERNO"].ToString();
                    for (int i = 1; i < columnCount; i++)
                        s += ("," + row[i].ToString().Trim());
                    writer.WriteLine(s);
                    writer.Flush();
                }

                file.Close();
            }
            catch (Exception)
            {
            }
        }        
    }
}
