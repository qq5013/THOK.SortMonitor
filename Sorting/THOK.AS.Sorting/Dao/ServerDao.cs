using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    public class ServerDao: BaseDao
    {
        public DataTable FindBatch(string lineCode)
        {
            string sql = string.Format("SELECT  TOP 1 BATCHID,ORDERDATE,BATCHNO  FROM AS_BI_BATCH WHERE ISUPTONOONEPRO='1' AND " +
                "BATCHID NOT IN (SELECT BATCHID FROM AS_BI_BATCHSTATUS WHERE LINECODE='{0}') ORDER BY ORDERDATE,BATCHNO", lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannel(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT A.* FROM AS_SC_CHANNELUSED A LEFT JOIN AS_BI_CHANNEL B ON A.CHANNELID = B.CHANNELID WHERE ORDERDATE='{0}' AND BATCHNO={1} AND A.LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderMaster(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_PALLETMASTER WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}' AND STATUS = '0'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderDetail(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_PALLETMASTER WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrder(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_ORDER WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindHandleSupply(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_HANDLESUPPLY WHERE ORDERDATE='{0}' AND BATCHNO={1} AND LINECODE='{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        ///  Ö£Ð¡Áú 20110904 Ìí¼Ó
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public DataTable FindOrderDateInfo(string orderDate)
        {
            string sql = string.Format("SELECT ORDERDATE,ORGCODE FROM AS_I_ORDERMASTER WHERE ORDERDATE='{0}' GROUP BY ORDERDATE,ORGCODE", orderDate);
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateOrderStatus(string sortNo,string channelGroup)
        {
            string sql = "";
            if(channelGroup.Equals("A"))
                 sql = string.Format("UPDATE AS_SC_PALLETMASTER SET STATUS = '1' WHERE SORTNO <= {0}", sortNo);
            else
                 sql = string.Format("UPDATE AS_SC_PALLETMASTER SET STATUS1 = '1' WHERE SORTNO <= {0}", sortNo);
            ExecuteNonQuery(sql);
        }

        public void UpdateBatchStatus(string batchID, string lineCode)
        {
            SqlCreate sqlCreate = new SqlCreate("AS_BI_BATCHSTATUS", SqlType.INSERT);
            sqlCreate.Append("BATCHID", batchID);
            sqlCreate.AppendQuote("LINECODE", lineCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
    }
}
