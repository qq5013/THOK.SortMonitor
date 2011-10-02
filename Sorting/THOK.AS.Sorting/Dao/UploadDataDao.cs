using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.AS.Sorting.Dao
{
   public class UploadDataDao:BaseDao
    {
       /// <summary>
       /// 查询上报信息
       /// </summary>
       /// <returns></returns>
       public DataTable FindSortUploadInfo()
       {
           string sql = "SELECT *,CASE WHEN IS_IMPORT=1 THEN '已上报' ELSE '未上报' END AS ISIMPORT FROM DWV_IORD_SORT_STATUS ORDER BY IS_IMPORT";
           return this.ExecuteQuery(sql).Tables[0];
       }

       public DataTable FindEfficiency()
       {
           string sql = "SELECT TOP 1 * FROM AS_SORT_EFFICIENCY ORDER BY ID DESC";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 根据状态查询上报数据
       /// </summary>
       /// <returns></returns>
       public DataTable GetSortUploadData(string status)
       {
           string sql = "SELECT ORDERDATE FROM AS_SC_PALLETMASTER GROUP BY ORDERDATE";
           if (this.ExecuteScalar(sql).ToString() != "")
           {
               string orderDate = Convert.ToDateTime(this.ExecuteScalar(sql).ToString()).ToString("yyyyMMdd");
               sql = string.Format("SELECT * FROM DWV_IORD_SORT_STATUS WHERE ORDERDATE='{0}' AND IS_IMPORT='{1}'", orderDate, status);
               return this.ExecuteQuery(sql).Tables[0];
           }
           else
               return null;
       }

       /// <summary>
       /// 查询上报数据
       /// </summary>
       /// <returns></returns>
       public DataTable GetSortUploadData()
       {
           string sql = @"SELECT ORDERDATE,LINECODE AS SORTING_CODE,
                        (SELECT MIN(BEGINTIME) FROM AS_SORT_STATUS) AS SORT_DATE,
                        (SELECT COUNT(*) FROM(SELECT CIGARETTECODE FROM AS_SC_ORDER GROUP BY CIGARETTECODE) A) AS SORT_SPEC,
                        (SELECT SUM(QUANTITY) FROM AS_SC_ORDER) AS SORT_QUANTITY,
                        (SELECT COUNT(*) FROM(SELECT ORDERID FROM AS_SC_PALLETMASTER GROUP BY ORDERID) A) AS SORT_ORDER_NUM,
                        (SELECT MIN(BEGINTIME) FROM AS_SORT_STATUS) AS SORT_BEGIN_DATE,
                        (SELECT MAX(ENDTIME) FROM AS_SORT_STATUS) AS SORT_END_DATE,
                        (SELECT MAX(STATUS_1) FROM AS_SORT_EFFICIENCY) AS SORT_COST_TIME,0 AS IS_IMPORT  
                        FROM AS_SC_PALLETMASTER A GROUP BY LINECODE,ORDERDATE";
          return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// 修改上报数据
       /// </summary>
       /// <param name="sortingTable"></param>
       public void InsertSortingDate(DataTable sortingTable)
       {
           foreach (DataRow row in sortingTable.Rows)
           {
               string sql = @"UPDATE DWV_IORD_SORT_STATUS SET SORTING_CODE='{0}',SORT_DATE='{1}',SORT_SPEC={2},SORT_QUANTITY={3},SORT_ORDER_NUM={4},
                               SORT_BEGIN_DATE='{5}',SORT_END_DATE='{6}',SORT_COST_TIME='{7}' WHERE ORDERDATE='{8}'";
               sql = string.Format(sql, row["SORTING_CODE"].ToString(), Convert.ToDateTime(row["SORT_DATE"]).ToString("yyyyMMdd"), row["SORT_SPEC"].ToString(), row["SORT_QUANTITY"], row["SORT_ORDER_NUM"],
                    Convert.ToDateTime(row["SORT_BEGIN_DATE"]).ToString("yyyyMMddHHmmss"), Convert.ToDateTime(row["SORT_END_DATE"]).ToString("yyyyMMddHHmmss"), row["SORT_COST_TIME"].ToString(), Convert.ToDateTime(row["ORDERDATE"]).ToString("yyyyMMdd"));
               this.ExecuteNonQuery(sql);               
           }
       }

       /// <summary>
       /// 执行sql语句
       /// </summary>
       /// <param name="sql"></param>
       public void setDate(string sql)
       {
           this.ExecuteNonQuery(sql);
       }
   }
}
