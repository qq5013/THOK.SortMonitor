using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    public class ChannelDao: BaseDao
    {

        public DataTable FindChannel()
        {
            string sql = "SELECT CHANNELCODE, CHANNELNAME, "+
                            " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                            " LINECODE, CIGARETTECODE, CIGARETTENAME, QUANTITY "+
                            " FROM AS_SC_CHANNELUSED ORDER BY CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据烟道代码查询烟道类型和烟道组号
        /// </summary>
        /// <param name="channelcode">烟道代码</param>
        /// <returns>返回烟道类型和烟道组号</returns>
        public DataTable FindChannelCode(string channelcode)
        {
            string sql = string.Format("SELECT CHANNELADDRESS,CHANNELTYPE,CHANNELGROUP FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelcode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据流水号查询烟道信息
        /// </summary>
        /// <param name="sortNo">流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回查询到的烟道信息表</returns>
        public DataTable FindChannelQuantity(string sortNo, string channelGroup)
        {
            string sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY "+
                                        " FROM (SELECT A.CHANNELNAME, A.LEDGROUP, A.LEDNO, "+
                                                " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE," +
                                                " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,"+
                                                " A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,"+
                                                " A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP "+
                                                " FROM AS_SC_CHANNELUSED A " +
                                                " LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY "+
                                                            " FROM AS_SC_ORDER WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
                                                            " ON A.CHANNELCODE = B.CHANNELCODE) C  "+
                                        " WHERE CHANNELGROUP = {1} ORDER BY CHANNELNAME ",sortNo,channelGroup=="A"?"1":"2");
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据流水号查询烟道信息
        /// </summary>
        /// <param name="sortNo">A线流水号</param>
        /// <param name="channelGroup">B线流水号</param>
        /// <returns>返回查询到的烟道信息表</returns>
        public DataTable FindAllChannelQuantity(string sortNo_A, string sortNo_B)
        {
            string sql = string.Format(@"SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY    
                                          FROM (SELECT A.CHANNELNAME, A.LEDGROUP, A.LEDNO,    
                                                  CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,   
                                                  A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,   
                                                  A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,   
                                                  A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP    
                                                  FROM AS_SC_CHANNELUSED A    
                                                  LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY    
                                                              FROM AS_SC_ORDER WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B    
                                                              ON A.CHANNELCODE = B.CHANNELCODE) C     
                                          WHERE CHANNELGROUP =1
                                       union all
                                        SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY    
                                          FROM (SELECT A.CHANNELNAME, A.LEDGROUP, A.LEDNO,    
                                                  CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,   
                                                  A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,   
                                                  A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,   
                                                  A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP    
                                                  FROM AS_SC_CHANNELUSED A    
                                                  LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY    
                                                              FROM AS_SC_ORDER WHERE SORTNO <= '{1}' GROUP BY CHANNELCODE) B    
                                                              ON A.CHANNELCODE = B.CHANNELCODE) C     
                                          WHERE CHANNELGROUP =2 ORDER BY CHANNELNAME", sortNo_A,sortNo_B);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 实时查询分拣烟道内所剩的卷烟数量
        /// </summary>
        /// <param name="sortNo">分拣流水号</param>
        /// <param name="channelGroup">烟道组号</param>
        /// <returns>返回查询到分拣烟道信息表</returns>
        public DataTable FindChannelRealtimeQuantity(string sortNo, string channelGroup)
        {
            string sql = string.Format(@"SELECT C.*,   
                                             (QUANTITY - SORTQUANTITY) AS NOSORTQUANTITY,            
                                             ( ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) ALLQUANTITY,
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) AS REMAINQUANTITY,                                             
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) / 50 BOXQUANTITY, 
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) % 50 ITEMQUANTITY		                                     
                                        FROM (SELECT A.LINECODE,A.CHANNELGROUP,A.CHANNELCODE,A.CHANNELNAME,A.CHANNELADDRESS,
                                                CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,
                                                CASE CHANNELTYPE WHEN '2' THEN A.QUANTITY % 50 WHEN '5' THEN A.QUANTITY ELSE A.QUANTITY % 50 END HANDLESUPPLYQUANTITY,
                                                A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY                                                
                                              FROM AS_SC_CHANNELUSED A 
                                                LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY FROM AS_SC_ORDER 
                                                            WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B 
                                                ON A.CHANNELCODE = B.CHANNELCODE) C  
                                        LEFT JOIN AS_CHANNEL_CHECK D ON C.CHANNELCODE=D.CHANNELCODE
                                        WHERE C.CHANNELGROUP = '{1}' ORDER BY C.CHANNELNAME", sortNo, channelGroup);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        ///  根据烟道组号和烟道类型查询出烟道信息表
        /// </summary>
        /// <param name="channelType">烟道类型</param>
        /// <param name="channelGroup">烟道组号</param>
        /// <returns>返回烟道信息表</returns>
        public DataTable FindEmptyChannel(string channelCode,string channelType,int channelGroup)
        {
            string sql = string.Format("SELECT CHANNELCODE, "+
                                        " CHANNELNAME + ' ' + CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机'  ELSE '通道机' END CHANNELNAME " +
                                        " FROM AS_SC_CHANNELUSED "+
                                        " WHERE CHANNELTYPE IN ('{0}') AND CHANNELTYPE != '5' AND CHANNELGROUP = {1} AND CHANNELCODE != {2} " +
                                        " ORDER BY CHANNELNAME", channelType, channelGroup, channelCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannel(string channelCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 查询空仓烟道信息
        /// </summary>
        /// <returns>返回空仓信息表</returns>
        public DataTable FindLastSortNo(int channelgroup)
        {
            string sql = string.Format("SELECT CHANNELADDRESS,CHANNELCODE,SORTNO FROM AS_SC_CHANNELUSED WHERE CHANNELGROUP={0} ORDER BY CHANNELGROUP,CHANNELCODE",channelgroup);
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateChannel(string channelCode, string cigaretteCode, string cigaretteName, int quantity, string sortNo)
        {
            string sql = string.Format("UPDATE AS_SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2}, SORTNO={3} WHERE CHANNELCODE='{4}'",
                cigaretteCode, cigaretteName, quantity, sortNo, channelCode);
            ExecuteNonQuery(sql);
        }

        public void InsertChannel(DataTable channelTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SORT_STATUS");
            ExecuteQuery("TRUNCATE TABLE AS_SORT_EFFICIENCY");
            ExecuteQuery("TRUNCATE TABLE AS_SC_CHANNELUSED");
            BatchInsert(channelTable, "AS_SC_CHANNELUSED");
        }

        /// <summary>
        /// 将补货监控系统的出库数据插入分拣监控系统（已经过2号扫码器和已经过3号扫码器的卷烟）
        /// </summary>
        /// <param name="channelCheckTable"></param>
        public void InsertChannelCheck(DataTable channelCheckTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_CHANNEL_CHECK");
            BatchInsert(channelCheckTable, "AS_CHANNEL_CHECK");
        }
    }
}
