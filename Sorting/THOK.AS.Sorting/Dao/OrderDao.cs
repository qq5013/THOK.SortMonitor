using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    public class OrderDao: BaseDao
    {
        public List<string> FindRouteMaxSortNoList()
        {
            List<string> routeMaxSortNoList = new List<string>();
            string sql = "SELECT MAX(SORTNO) AS ROUTE_MAX_SORTNO FROM AS_SC_PALLETMASTER GROUP BY ROUTECODE";
            DataTable table = ExecuteQuery(sql).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                routeMaxSortNoList.Add(row["ROUTE_MAX_SORTNO"].ToString());
            }
            return routeMaxSortNoList;
        }
        public DataTable FindMaster()
        {
            string sql = "SELECT ORDERDATE,BATCHNO,SORTNO, ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME, " +
                "CASE STATUS WHEN '0' THEN '未下单' ELSE '已下单' END STATUS,CASE STATUS1 WHEN '0' THEN '未下单' ELSE '已下单' END STATUS1,"+
            "CASE WHEN PACKQUANTITY=QUANTITY THEN '已发送' ELSE '未发送' END PACKAGE,CASE WHEN PACKQUANTITY1=QUANTITY1 THEN '已发送' ELSE '未发送' END PACKAGE1 FROM AS_SC_PALLETMASTER";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindDetail(string sortNo)
        {
            string sql = string.Format("SELECT A.SORTNO, ORDERID, B.CHANNELNAME, "+
                                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                                            " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                                            " CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE "+
                                            " FROM AS_SC_ORDER A "+
                                            " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                                            " WHERE A.SORTNO={0} ORDER BY A.CHANNELGROUP,B.CHANNELADDRESS DESC", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }
        
        public DataTable FindCigarettes()
        {
            string sql = "SELECT CIGARETTECODE,CIGARETTENAME,SUM(QUANTITY) FROM AS_SC_ORDER GROUP BY CIGARETTECODE,CIGARETTENAME ORDER BY CIGARETTECODE";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderWithCigarette(string cigaretteCode)
        {
            string sql = "SELECT MIN(A.SORTNO) SORTNO,A.ORDERDATE,A.BATCHNO,A.LINECODE,A.ORDERID,B.ROUTENAME,B.CUSTOMERNAME,A.CIGARETTECODE,A.CIGARETTENAME,SUM(A.QUANTITY) QUANTITY,CHANNELCODE" +
                            " FROM AS_SC_ORDER A" +
                            " LEFT JOIN AS_SC_PALLETMASTER B ON A.ORDERID = B.ORDERID"+
                            " WHERE CIGARETTECODE = '{0}'" +
                            " GROUP BY A.ORDERDATE,A.BATCHNO,A.LINECODE,A.ORDERID,B.ORDERID,A.CIGARETTECODE,A.CIGARETTENAME,A.CHANNELCODE,B.ROUTENAME,B.CUSTOMERNAME" +
                            " ORDER BY A.ORDERDATE,A.BATCHNO,A.LINECODE,MIN(A.SORTNO)";
            sql = string.Format(sql,cigaretteCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindPackMaster()
        {
            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY MIN(SORTNO)) AS PACKNO,MIN(SORTNO) AS SORTNO ,ORDERDATE,ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,SUM(QUANTITY) AS QUANTITY, SUM(QUANTITY1) AS QUANTITY1 " +
                            "FROM AS_SC_PALLETMASTER GROUP BY ORDERDATE,ROUTECODE,ROUTENAME,ORDERID,CUSTOMERCODE,CUSTOMERNAME ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindPackMaster(string [] filter)
        {
            string sql = "SELECT B.* FROM " +
                            " (" +
	                            " SELECT ROW_NUMBER() OVER(ORDER BY MIN(A.SORTNO)) AS PACKNO," +
	                            " MIN(A.SORTNO) AS SORTNO ,A.ORDERDATE,A.ORDERID,A.ROUTECODE,A.ROUTENAME," +
	                            " A.CUSTOMERCODE,A.CUSTOMERNAME,SUM(A.QUANTITY) AS QUANTITY" +
	                            " FROM AS_SC_PALLETMASTER A" +
	                            " GROUP BY A.ORDERDATE,A.ROUTECODE,A.ROUTENAME,A.ORDERID,A.CUSTOMERCODE,A.CUSTOMERNAME " +
                            " ) B " +
                            " LEFT JOIN AS_SC_ORDER C ON B.ORDERID = C.ORDERID " +
                            " WHERE {0} " +
                            " GROUP BY B.PACKNO,B.SORTNO,B.ORDERDATE,B.ROUTECODE,B.ROUTENAME,B.ORDERID,B.CUSTOMERCODE,B.CUSTOMERNAME,B.QUANTITY" +
                            " {1} " +
                            " ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql,filter)).Tables[0];
        }


        public DataTable FindPackDetail()
        {
            //string sql = string.Format("SELECT A.ORDERID, A.CIGARETTECODE,A.CIGARETTENAME, SUM(A.QUANTITY) QUANTITY ,CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE,A.CHANNELGROUP " +
            //                        "FROM AS_SC_ORDER A  LEFT JOIN DBO.AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE A.ORDERID='{0}' "+
            //                        " GROUP BY ORDERID,A.CHANNELGROUP,A.SORTNO ,B.CHANNELNAME,A.CIGARETTECODE,A.CIGARETTENAME ORDER BY A.CHANNELGROUP,A.SORTNO,B.CHANNELNAME", orderId);
            //return ExecuteQuery(sql).Tables[0];
            string sql = string.Format("SELECT A.SORTNO, ORDERID, B.CHANNELNAME, " +
                                " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                                " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                                " CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE " +
                                " FROM AS_SC_ORDER A " +
                                " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                                " ORDER BY A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC");
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindSortMaster(string channelGroup)
        {
            string sql = "SELECT TOP 1 * FROM AS_SC_PALLETMASTER WHERE STATUS{0}=0 ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql,channelGroup == "A" ? "" : "1")).Tables[0];
        }

        public DataTable FindSortSpeed()
        {
            string sql = "SELECT * FROM 效率报表";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据订单ID和A线或者B线查询换户的流水号
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个换户的流水号</returns>
        public string FindMaxSortNoFromMasterByOrderID(string orderID,string channelGroup)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER WHERE ORDERID='{0}' AND QUANTITY{1} != 0 ";
            return ExecuteScalar(string.Format(sql, orderID,channelGroup == "A" ? "" : "1")).ToString();
        }

        /// <summary>
        /// 查询本分拣线组是否结束
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindEndSortNoForChannelGroup(string channelGroup)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER WHERE QUANTITY{0} != 0 ";
            return ExecuteScalar(string.Format(sql, channelGroup == "A" ? "" : "1")).ToString();
        }

        /// <summary>
        /// 根据A线或B线查询当前缺烟的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindMaxSortedMaster(string channelGroup)
        {

            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER WHERE STATUS{0} ='1'";
            return ExecuteScalar(string.Format(sql, channelGroup == "A" ? "" : "1")).ToString();
        }

        /// <summary>
        /// 根据当前要分拣的流水号查询数据
        /// </summary>
        /// <param name="sortNo">当前要分拣的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个表的数据</returns>
        public DataTable FindSortDetail(string sortNo, string channelGroup)
        {
            string sql = "SELECT A.CHANNELADDRESS,A.CHANNELCODE, A.CHANNELTYPE, ISNULL(B.QUANTITY,0) QUANTITY" +
                " FROM AS_SC_CHANNELUSED A "+
                " LEFT JOIN (SELECT SORTNO,CHANNELCODE,SUM(QUANTITY) QUANTITY FROM AS_SC_ORDER GROUP BY SORTNO,CHANNELCODE) B "+
                        " ON A.CHANNELCODE = B.CHANNELCODE AND B.SORTNO = '{0}' "+
                " WHERE A.CHANNELGROUP = {1} " + 
                " ORDER BY A.CHANNELADDRESS, A.CHANNELTYPE , A.CHANNELCODE";
            return ExecuteQuery(string.Format(sql, sortNo,channelGroup == "A" ? "1" : "2")).Tables[0];
        }

        public DataTable FindSortingMasterPack(string sortNo)
        { 
            string sql =string.Format( "SELECT * FROM AS_SC_PALLETMASTER WHERE SORTNO ='{0}' ",sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindMasterInfo(string sortNo)
        {
            string sql = string.Format("SELECT ROUTENAME, CUSTOMERNAME FROM AS_SC_PALLETMASTER WHERE SORTNO={0}", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindCigaretteDetail(string sortNo)
        {
            string sql = string.Format("SELECT CIGARETTENAME, SUM(QUANTITY) QUANTITY FROM AS_SC_ORDER WHERE SORTNO={0} " +
                "GROUP BY CIGARETTENAME ORDER BY SUM(QUANTITY) DESC", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据流水号查询分拣完成的数量等数据
        /// </summary>
        /// <param name="sortNo">完成的流水号</param>
        /// <returns>返回查询到的数据表</returns>
        public DataTable FindOrderInfo(string sortNo)
        {
            string sql = "";
            if (sortNo != null)
                sql = "SELECT COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, " +
                         " (SELECT ISNULL(SUM(QUANTITY),0) FROM AS_SC_PALLETMASTER WHERE FINISHEDTIME <= GETDATE() AND STATUS=1 ) QUANTITY, " +
                         " (SELECT ISNULL(SUM(QUANTITY1),0) FROM AS_SC_PALLETMASTER WHERE FINISHEDTIME1 <= GETDATE() AND STATUS1=1 ) QUANTITY1 " +
                         " FROM AS_SC_PALLETMASTER WHERE FINISHEDTIME <= GETDATE() OR FINISHEDTIME1 <= GETDATE()";
            else
                sql = "SELECT COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, "+
                        " ISNULL(SUM(QUANTITY),0) QUANTITY, ISNULL(SUM(QUANTITY1),0) QUANTITY1 " +
                        " FROM AS_SC_PALLETMASTER ";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询A线或B线已经完成的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindLastSortNo(string channelGroup)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER WHERE STATUS{0}='1'";
            return ExecuteScalar(string.Format(sql,channelGroup == "A"? "":"1" )).ToString();
        }

        /// <summary>
        /// 修改A线或者B线已分拣完的流水号的时间
        /// </summary>
        /// <param name="sortNo">完成的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        public void UpdateFinisheTime(string sortNo, string channelGroup)
        {
            string sql = "UPDATE AS_SC_PALLETMASTER SET FINISHEDTIME{0} = GETDATE() WHERE STATUS{0}='1' AND SORTNO <= '{1}' ";
            ExecuteNonQuery(string.Format(sql, channelGroup == "A" ? "" : "1", sortNo));
        }

        public DataTable FindPackInfo()
        {
            //string sql = "SELECT TOP 1 SORTNO, (CEILING(QUANTITY/25.0)-CEILING((QUANTITY-PACKQUANTITY)/25.0)+1)  " +
            //    "AS BAGSN,CASE WHEN QUANTITY-PACKQUANTITY>=30 THEN 25 WHEN QUANTITY-PACKQUANTITY>25 THEN 20 ELSE QUANTITY-PACKQUANTITY " +
            //    "END AS QUANTITY FROM AS_SC_PALLETMASTER WHERE QUANTITY-PACKQUANTITY>0 ORDER BY SORTNO";
            string sql = "SELECT TOP 400 MIN(SORTNO) SORTNO, ORDERID,SUM(QUANTITY) QUANTITY FROM AS_SC_PALLETMASTER WHERE QUANTITY-PACKQUANTITY>0 GROUP BY ORDERID ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdatePackQuantity(string sortNo, int quantity)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PACKQUANTITY = {0} WHERE SORTNO = {1}", quantity, sortNo);
            ExecuteNonQuery(sql);
        }

        public void UpdatePackQuantityByOrderID(string orderID)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PACKQUANTITY = QUANTITY WHERE ORDERID = '{0}'", orderID);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 把发送的流水号状态修改为已发送
        /// </summary>
        /// <param name="sortNo">已发送的流水号</param>
        /// <param name="status">状态</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateOrderStatus(string sortNo, string status, string channelGroup)
        {
            string sql = "UPDATE AS_SC_PALLETMASTER SET STATUS{0} = '{1}' WHERE SORTNO = {2}";
            ExecuteNonQuery(string.Format(sql,channelGroup == "A" ? "" : "1",status, sortNo));
        }

        public void UpdateChannel(string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE AS_SC_ORDER SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}'", targetChannel, sourceChannel);
            ExecuteNonQuery(sql);
        }
        //清除选中的那行之后的为未包装
        public void ClearPackQuantity(string orderID)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PACKQUANTITY=0,PACKQUANTITY1=0 WHERE SORTNO>= (SELECT MIN(SORTNO) FROM AS_SC_PALLETMASTER WHERE ORDERID = '{0}' )", orderID);
            ExecuteNonQuery(sql);
        }
        //标识选中的那行之前的为已包装
        public void UpdatePackQuantity(string orderID)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PACKQUANTITY = QUANTITY,PACKQUANTITY1= QUANTITY1 WHERE SORTNO <= (SELECT MAX(SORTNO) FROM AS_SC_PALLETMASTER WHERE ORDERID = '{0}')", orderID);
            ExecuteNonQuery(sql);
        }

        public void InsertMaster(DataTable masterTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SC_PALLETMASTER");
            BatchInsert(masterTable, "AS_SC_PALLETMASTER");
        }

        public void InsertDetail(DataTable detailTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SC_PALLETDETAIL");
            BatchInsert(detailTable, "AS_SC_PALLETDETAIL");
        }

        public void InsertOrder(DataTable orderTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SC_ORDER");
            BatchInsert(orderTable, "AS_SC_ORDER");
        }

        public void InsertHandleSupply(DataTable handleSupplyOrderTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SC_HANDLESUPPLY");
            BatchInsert(handleSupplyOrderTable, "AS_SC_HANDLESUPPLY");
        }

        /// <summary>
        /// 郑小龙 20110904 添加
        /// </summary>
        /// <param name="sortTable"></param>
        public void InsertSortingUpload(DataTable sortTable,string lineCode)
        {
            string sortid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            sortid = lineCode.Substring(1, 1) + sortid.Substring(3, 5) + sortid.Substring(13, 4);
            string sql = "INSERT INTO DWV_IORD_SORT_STATUS(SORT_BILL_ID,ORG_CODE,ORDERDATE,IS_IMPORT)VALUES('{0}','{1}','{2}','0')";
            sql = string.Format(sql, sortid, sortTable.Rows[0]["ORGCODE"].ToString(), Convert.ToDateTime(sortTable.Rows[0]["ORDERDATE"].ToString()).ToString("yyyyMMdd"));
            this.ExecuteNonQuery(sql);
        }

        public int FindUnsortCount()
        {
            string sql = "SELECT COUNT(*) FROM AS_SC_PALLETMASTER WHERE STATUS='0'";
            return Convert.ToInt32(ExecuteScalar(sql));
        }
        /// <summary>
        /// 矫正订单，更改订单的状态
        /// </summary>
        /// <param name="sortNo">PLC发来要矫正的流水号</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateMissOrderStatus(string sortNo, string channelGroup)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET STATUS{0} = '{1}' WHERE SORTNO >= {2}",channelGroup == "A"?"":"1","0", sortNo);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE AS_SC_PALLETMASTER SET STATUS{0} = '{1}' WHERE SORTNO <  {2}",channelGroup == "A"?"":"1","1", sortNo);
            ExecuteNonQuery(sql);
        }

        public void UpdateQuantity(string sortNo, string orderId,string channelName,string cigaretteCode, int quantity)
        {
            string sql = string.Format("UPDATE AS_SC_ORDER SET QUANTITY = {0} WHERE SORTNO = {1} AND ORDERID = '{2}' AND CIGARETTECODE = '{3}' ", quantity,sortNo, orderId, cigaretteCode);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE AS_SC_HANDLESUPPLY SET QUANTITY = {0} WHERE SORTNO = {1} AND ORDERID = '{2}' AND CIGARETTECODE = '{3}' ", quantity, sortNo, orderId, cigaretteCode);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE AS_SC_CHANNELUSED SET QUANTITY = (SELECT SUM(QUANTITY) FROM AS_SC_ORDER WHERE AS_SC_ORDER.CHANNELCODE = AS_SC_CHANNELUSED.CHANNELCODE) WHERE CHANNELNAME = '{0}' ", channelName);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE AS_SC_PALLETMASTER SET QUANTITY = (SELECT SUM(QUANTITY) FROM AS_SC_ORDER WHERE SORTNO = {0}) WHERE SORTNO = {0} ", sortNo);
            ExecuteNonQuery(sql);

            sql = string.Format("SELECT * FROM AS_SC_CHANNELUSED WHERE CHANNELNAME = '{0}' " , channelName);
            DataTable channelTable = ExecuteQuery(sql).Tables[0];
            sql = string.Format("SELECT A.* FROM AS_SC_ORDER A LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE B.CHANNELNAME = '{0}' ", channelName);
            DataTable orderTable = ExecuteQuery(sql).Tables[0];
            Util.SetChannelSortNoUtil.SetChannelSortNo(channelTable,orderTable);
            sql = string.Format("UPDATE AS_SC_CHANNELUSED SET SORTNO = {0} WHERE CHANNELNAME = '{0}' ",channelTable.Rows[0]["SORTNO"] ,channelName);
            ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询分拣效率
        /// </summary>
        /// <returns>返回一个整数效率</returns>
        public int FindSortingAverage()
        {
            string sql = "SELECT ISNULL((SELECT TOP 1 分拣运行效率 AS AVERAGE FROM 效率报表 ORDER BY ID DESC),0)";
            return Convert.ToInt32(ExecuteQuery(sql).Tables[0].Rows[0][0]);
        }

        public DataTable FindOrderDetail(string orderID)
        {
            string sql = string.Format("SELECT A.CIGARETTENAME, SUM(A.QUANTITY) QUANTITY FROM AS_SC_ORDER A  LEFT JOIN dbo.AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE ORDERID = '{0}' GROUP BY A.SORTNO ,B.CHANNELNAME,A.CIGARETTECODE,A.CIGARETTENAME ORDER BY A.SORTNO,B.CHANNELNAME", orderID);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderMaster()
        {
            string sql = "SELECT A.ORDERDATE,GETDATE() CDATE,A.BATCHNO,A.LINECODE,"+
                            " A.ORDERID,AREACODE,AREANAME,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,LICENSENO,ADDRESS,"+
                            " CUSTOMERSORTNO,A.ORDERNO,ABNORMITY_QUANTITY,"+
                            " SUM(B.QUANTITY) QUANTITY0 ,"+
                            " ISNULL((SELECT SUM(QUANTITY) FROM AS_SC_ORDER WHERE ORDERID = A.ORDERID AND EXPORTNO = 1),0) AS QUANTITY1," +
                            " ISNULL((SELECT SUM(QUANTITY) FROM AS_SC_ORDER WHERE ORDERID = A.ORDERID AND EXPORTNO = 2),0) AS QUANTITY2" +
                            " FROM AS_SC_PALLETMASTER A"+
                            " LEFT JOIN AS_SC_ORDER B ON A.SORTNO = B.SORTNO"+
                            " GROUP BY A.ORDERDATE,A.BATCHNO,A.LINECODE,"+
                            " A.ORDERID,AREACODE,AREANAME,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,LICENSENO,ADDRESS,"+
                            " CUSTOMERSORTNO,A.ORDERNO,ABNORMITY_QUANTITY"+
                            " ORDER BY CUSTOMERSORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdatePackCount(string orderId,int exportNo,int packCount)
        {
            string sql = "UPDATE AS_SORT_PACKORDER SET PACKCOUNT = {0} WHERE EXPORTNO = {1} AND ORDERID = '{2}'";
            ExecuteNonQuery(string.Format(sql,packCount,exportNo,orderId));
        }

        public void InsertPackOrder(DataRow orderMasterRow, DataRow orderDetailRow, int splitQuantity, int orderNo, int packNo, int exportNo, int packCount)
        {
            SqlCreate sql = new SqlCreate("AS_SORT_PACKORDER", SqlType.INSERT);

            sql.AppendQuote("ORDERDATE", orderMasterRow["ORDERDATE"]);
            sql.AppendQuote("CDATE", orderMasterRow["CDATE"]);
            sql.Append("BATCHNO", orderMasterRow["BATCHNO"]);
            sql.AppendQuote("LINECODE", orderMasterRow["LINECODE"]);                      
            
            sql.AppendQuote("ORDERID", orderMasterRow["ORDERID"]);
            sql.AppendQuote("AREACODE", orderMasterRow["AREACODE"]);
            sql.AppendQuote("AREANAME", orderMasterRow["AREANAME"]);
            sql.AppendQuote("ROUTECODE", orderMasterRow["ROUTECODE"]);
            sql.AppendQuote("ROUTENAME", orderMasterRow["ROUTENAME"]);
            sql.AppendQuote("CUSTOMERCODE", orderMasterRow["CUSTOMERCODE"]);
            sql.AppendQuote("CUSTOMERNAME", orderMasterRow["CUSTOMERNAME"]);

            sql.AppendQuote("LICENSENO", orderMasterRow["LICENSENO"]);
            sql.AppendQuote("ADDRESS", orderMasterRow["ADDRESS"]);
            sql.AppendQuote("CUSTOMERSORTNO", orderMasterRow["CUSTOMERSORTNO"]);
            sql.AppendQuote("CUSTOMERORDERNO", orderMasterRow["ORDERNO"]);

            sql.Append("SUMQUANTITY", orderMasterRow["QUANTITY0"]);
            sql.Append("ABNORMITY_QUANTITY", orderMasterRow["ABNORMITY_QUANTITY"]);

            sql.Append("SORTNO", orderDetailRow["SORTNO"]);
            sql.Append("CHANNELGROUP", orderDetailRow["CHANNELGROUP"]);
            sql.AppendQuote("CIGARETTECODE", orderDetailRow["CIGARETTECODE"]);
            sql.AppendQuote("CIGARETTENAME", orderDetailRow["CIGARETTENAME"]);

            sql.Append("QUANTITY", splitQuantity);
            sql.Append("ORDERNO", orderNo);
            sql.Append("PACKNO", packNo);
            sql.Append("PACKCOUNT",0);
            sql.Append("PACKINDEX", packCount);

            sql.Append("EXPORTNO", exportNo);            

            ExecuteNonQuery(sql.GetSQL());
        }

        public void UpdatePackOrderStatus(int exportNo)
        {
            ExecuteNonQuery(string.Format("UPDATE AS_SORT_PACKORDERSTATUS SET STATUS ='1' WHERE PACKCODE = '{0}' ", exportNo));
        }

        internal DataTable FindStartNoForCacheOrderQuery(int channelGroup, int startNo)
        {
            string sql = @"SELECT SORTNO FROM AS_SC_ORDER WHERE CHANNELGROUP ={0} 
                        AND ORDERID IN 
                        (SELECT ORDERID FROM AS_SC_ORDER WHERE SORTNO ={1}) 
                        AND ORDERNO IN
                        (SELECT ORDERNO FROM AS_SC_ORDER WHERE SORTNO ={1})
                        ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql, channelGroup, startNo)).Tables[0];
        }

        internal DataTable FindOrderIDAndOrderNoForCacheOrderQuery(int channelGroup, int sortNo)
        {
            string sql = "SELECT ORDERID,ORDERNO FROM AS_SC_ORDER WHERE  CHANNELGROUP = {0} AND SORTNO ={1}";
            return ExecuteQuery(string.Format(sql,channelGroup,sortNo)).Tables[0];
        }

        public DataTable FindDetailForCacheOrderQuery(string orderId, int orderNo, int channelGroup)
        {
            string sql = @"SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME,
                             CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,
                             A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ,
                                CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE, 
                                  ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0,
                                  ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1,
                                  ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2
                                    FROM AS_SC_ORDER A
                                      LEFT JOIN AS_SC_CHANNELUSED B 
                                        ON A.CHANNELCODE=B.CHANNELCODE
                                          LEFT JOIN AS_SC_PALLETMASTER C
                                            ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE
                                              WHERE A.ORDERID ='{0}' AND A.ORDERNO ={1} AND A.CHANNELGROUP ={2} ORDER BY A.SORTNO DESC,B.CHANNELADDRESS DESC";
            return ExecuteQuery(string.Format(sql, orderId, orderNo,channelGroup)).Tables[0];
        }

        /// <summary>
        /// 查询多沟带缓存段
        /// </summary>
        /// <param name="channelGroup">通道组</param>
        /// <param name="sortNoStart">前端订单号</param>
        /// <returns>返回多沟带缓存段起所有订单数据</returns>         
        public DataTable FindDetailForCacheOrderQuery(int channelGroup, int sortNoStart)
        {
            string sql = @"SELECT A.SORTNO,A.ORDERID,A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ,C.CUSTOMERNAME,B.CHANNELNAME,   
                            CASE B.CHANNELTYPE 
                        	  WHEN '2' 
	                            THEN '立式机' 
                              WHEN '5' 
	                            THEN '立式机' 
	                          ELSE '通道机' 
                            END CHANNELTYPE,   
                            CASE 
	                          WHEN A.CHANNELGROUP=1 
	                            THEN 'A线' 
	                          ELSE 'B线' 
                            END  CHANNELLINE,  
                              ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0,  
                              ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1,  
                              ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2  
                                FROM AS_SC_ORDER A  
                                  LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE   
                                    LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE
                                      WHERE A.CHANNELGROUP ={0} AND A.SORTNO >={1} ORDER BY A.SORTNO,B.CHANNELADDRESS";
            return ExecuteQuery(string.Format(sql, channelGroup, sortNoStart)).Tables[0];
        }

        public DataTable FindDetailForCacheOrderQuery(int exportNo, int sortNo, string packMode)
        {
            string sql = "";
            switch (packMode)
            {
                case "0":
                    sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                            " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                            " CASE WHEN A.CHANNELGROUP = 1 THEN 'A线' ELSE 'B线' END  CHANNELLINE, " +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2, " +
                            "  CASE WHEN " +
                            " 	    A.CHANNELGROUP = 1" +
                            "  THEN" +
                            " 	    CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER " +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER " +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  ELSE" +
                            "      CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER" +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER" +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  END" +
                            "  ORDERNO_PACKNO" +
                            " FROM AS_SC_ORDER A " +
                            " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                            " LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
                            " WHERE A.EXPORTNO = {0} AND A.SORTNO <= {1}  ORDER BY C.CUSTOMERSORTNO DESC,ORDERNO_PACKNO DESC,A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC";
                    sql = string.Format(sql, exportNo, sortNo);
                    break;
                case "1":
                    sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                            " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                            " CASE WHEN A.CHANNELGROUP = 1 THEN 'A线' ELSE 'B线' END  CHANNELLINE, " +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2," +
                            "  CASE WHEN " +
                            " 	    A.CHANNELGROUP = 1" +
                            "  THEN" +
                            " 	    CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER " +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER " +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  ELSE" +
                            "      CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER" +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER" +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  END" +
                            "  ORDERNO_PACKNO" +
                            " FROM AS_SC_ORDER A " +
                            " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                            " LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
                            " WHERE A.SORTNO <= {0}  ORDER BY C.CUSTOMERSORTNO DESC,ORDERNO_PACKNO DESC,A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC";
                    sql = string.Format(sql,sortNo);
                    break;
                default:
                    return (new DataTable());
            }
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindPackDataOrder(int exportNo)
        {
            switch (exportNo)
            {
                case 0:
                    return ExecuteQuery("SELECT * FROM V_SORT_PACKORDER_ALL ORDER BY ORDERNO").Tables[0];
                case 1:
                    return ExecuteQuery("SELECT * FROM V_SORT_PACKORDER_PACKER_ONE ORDER BY ORDERNO").Tables[0];
                case 2:
                    return ExecuteQuery("SELECT * FROM V_SORT_PACKORDER_PACKER_TWO ORDER BY ORDERNO").Tables[0];
                default:
                    return new DataTable();
            }
        }

        public void DeletePackData()
        {
            ExecuteQuery("TRUNCATE TABLE AS_SORT_PACKORDER");
            ExecuteQuery("TRUNCATE TABLE AS_SC_EXPORTPACK1");
            ExecuteQuery("TRUNCATE TABLE AS_SC_EXPORTPACK2");
            ExecuteQuery("TRUNCATE TABLE AS_SC_PACKTEAR1");
            ExecuteQuery("TRUNCATE TABLE AS_SC_PACKTEAR2");

            ExecuteQuery("UPDATE AS_SORT_PACKORDERSTATUS SET STATUS = '0'");
        }

        internal DataTable FindOrderDetailForPack(string orderId, string exportNo)
        {
            string sql = "SELECT A.* ,B.CHANNELADDRESS," +
                 "  CASE WHEN " +
                 " 	    A.CHANNELGROUP = 1" +
                 "  THEN" +
                 " 	    CASE WHEN " +
                 " 	    (" +
                 " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER " +
                 " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                 " 	    ) = (" +
                 " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER " +
                 " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                 "      )" +
                 " 	    THEN 10000 " +
                 " 	    ELSE ORDERNO " +
                 "      END " +
                 "  ELSE" +
                 "      CASE WHEN " +
                 " 	    (" +
                 " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER" +
                 " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                 " 	    ) = (" +
                 " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER" +
                 " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                 "      )" +
                 " 	    THEN 10000 " +
                 " 	    ELSE ORDERNO " +
                 "      END " +
                 "  END" +
                 "  ORDERNO_PACKNO" +
            " FROM AS_SC_ORDER A " +
            " LEFT JOIN AS_SC_CHANNELUSED B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE " +
            " WHERE A.ORDERID = '{0}' AND A.EXPORTNO IN ({1})" +
            " ORDER BY ORDERNO_PACKNO,CHANNELGROUP DESC,SORTNO,CHANNELADDRESS";
            return ExecuteQuery(string.Format(sql,orderId,exportNo)).Tables[0];
        }

        public DataTable packOrderToExport(int packNo)
        {
            string sql = @"SELECT CONVERT(NVARCHAR(10), A.ORDERDATE, 120)AS ORDERDATE,A.BATCHNO,A.LINECODE,A.SORTNO,
                                A.ORDERID,B.PACKNO,A.ROUTECODE,A.ROUTENAME,A.CUSTOMERCODE,A.CUSTOMERNAME,
                                A.ADDRESS AS CUSTOMERADDRESS ,A.ORDERNO,
                                B.CIGARETTECODE,B.CIGARETTENAME,SUM(B.QUANTITY)AS QUANTITY,
                                B.CHANNELGROUP,B.CHANNELCODE
                            FROM AS_SC_PALLETMASTER  A 
                            LEFT JOIN AS_SC_ORDER B ON A.SORTNO=B.SORTNO 
                            WHERE B.PACKNO={0}
								AND B.PACKNO NOT IN (SELECT PACKNO FROM AS_SC_EXPORTPACK1)
								AND B.PACKNO NOT IN (SELECT PACKNO FROM AS_SC_EXPORTPACK2)
                            GROUP BY A.ORDERDATE,A.BATCHNO,A.LINECODE,A.SORTNO,
                                A.ORDERID,B.PACKNO,A.ROUTECODE,A.ROUTENAME,A.CUSTOMERCODE,A.CUSTOMERNAME,
                                A.ADDRESS,A.ORDERNO,
                                B.CIGARETTECODE,B.CIGARETTENAME,
                                B.CHANNELGROUP,B.CHANNELCODE 
                            ORDER BY B.CHANNELGROUP DESC,B.CHANNELCODE ASC";
            return ExecuteQuery(string.Format(sql, packNo)).Tables[0];
        }

        private static string strsql = "";
        public void InsertPackExport(DataRow InRow,int exportNo,int customerSumQuantity,int bagSumQuantity)
        {
            SqlCreate sql = new SqlCreate(string.Format("AS_SC_EXPORTPACK{0}", exportNo), SqlType.INSERT);
            sql.AppendQuote("ORDERDATE", InRow["ORDERDATE"]);
            sql.Append("BATCHNO", InRow["BATCHNO"]);
            sql.AppendQuote("LINECODE", InRow["LINECODE"]);
            sql.AppendQuote("SORTNO", InRow["SORTNO"]);
            sql.AppendQuote("ORDERID", InRow["ORDERID"]);
            sql.AppendQuote("ROUTECODE", InRow["ROUTECODE"]);
            sql.AppendQuote("ROUTENAME", InRow["ROUTENAME"]);
            sql.AppendQuote("CUSTOMERCODE", InRow["CUSTOMERCODE"]);
            sql.AppendQuote("CUSTOMERNAME", InRow["CUSTOMERNAME"]);
            sql.AppendQuote("CUSTOMERSORTNO", InRow["ORDERNO"]);
            sql.AppendQuote("CUSTOMERADDRESS", InRow["CUSTOMERADDRESS"]);
            sql.AppendQuote("CIGARETTECODE", InRow["CIGARETTECODE"]);
            sql.AppendQuote("CIGARETTENAME", InRow["CIGARETTENAME"]);
            sql.AppendQuote("BAGQUANTITY", bagSumQuantity);
            sql.AppendQuote("TQUANTITY", customerSumQuantity);
            sql.AppendQuote("PACKNO", InRow["PACKNO"]);
            sql.AppendQuote("QUANTITY", InRow["QUANTITY"]);
            if (strsql == sql.GetSQL())
            {
                THOK.MCP.Logger.Info(System.Threading.Thread.CurrentThread.Name);
                THOK.MCP.Logger.Info(sql.GetSQL());
            }

            strsql = sql.GetSQL();
            ExecuteNonQuery(sql.GetSQL());
//            string sql = @"INSERT INTO dbo.AS_SC_EXPORTPACK{0}(ORDERDATE,BATCHNO,LINECODE,
//                            SORTNO,ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,
//                                CUSTOMERSORTNO,CUSTOMERADDRESS,CIGARETTECODE,CIGARETTENAME,BAGQUANTITY,
//                                TQUANTITY,PACKNO,QUANTITY) VALUES ('{1}','{2}','{3}','{4}','{5}',
//                                   '{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
//            ExecuteQuery(string.Format(sql, exportNo, InRow["ORDERDATE"], InRow["BATCHNO"], InRow["LINECODE"], InRow["SORTNO"],
//                InRow["ORDERID"], InRow["ROUTECODE"], InRow["ROUTENAME"], InRow["CUSTOMERCODE"], InRow["CUSTOMERNAME"],
//                InRow["ORDERNO"], InRow["CUSTOMERADDRESS"], InRow["CIGARETTECODE"], InRow["CIGARETTENAME"],
//                bagSumQuantity, customerSumQuantity, InRow["PACKNO"], InRow["QUANTITY"]));
        }
        /// <summary>
        /// 客户卷烟量
        /// </summary>
        /// <param name="customerCode">客户包</param>
        /// <returns>客户分拣卷烟总量</returns>
        public int FindCustomerQuantity(int packNo)
        {
            string sql = "SELECT SUM(QUANTITY)AS TQUANTITY FROM AS_SC_ORDER WHERE ORDERID IN(SELECT ORDERID FROM AS_SC_ORDER WHERE PACKNO={0})";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql, packNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 烟包卷烟烟量
        /// </summary>
        /// <param name="customerCode">客户包</param>
        /// <returns>烟包卷烟量</returns>
        public int FindBagQuantity(int packNo)
        {
            string sql = "SELECT SUM(QUANTITY)AS TQUANTITY FROM AS_SC_ORDER WHERE PACKNO={0}";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql, packNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 查找已传给贴标的包号
        /// </summary>
        /// <param name="exportNo">包装机号</param>
        /// <returns>包号表</returns>
        public DataTable FindexportPack(int exportNo)
        {
            string sql = " SELECT DISTINCT PACKNO FROM dbo.AS_SC_EXPORTPACK{0}";
            return ExecuteQuery(string.Format(sql,exportNo)).Tables[0];
        }

        /// <summary>
        /// 查找烟包的数据行
        /// </summary>
        /// <param name="exportNo">出口终端号</param>
        /// <param name="packNo">包号</param>
        /// <returns></returns>
        public int FindCountDataByPackNo(int exportNo,int packNo)
        {
            string sql = "SELECT COUNT(*) FROM dbo.AS_SC_EXPORTPACK{0} WHERE PACKNO={1}";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql,exportNo, packNo)).Tables[0].Rows[0][0]);
        }
    }
}
