using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    public class StockDao:BaseDao
    {
        /// <summary>
        ///  查询补货监控系统已经出库且已过2号扫码器和3号扫码器的卷烟的信息
        /// </summary>
        /// <param name="Linecode">分拣线代码</param>
        /// <returns></returns>
        public DataTable FindOutData(string Linecode)
        {
            string sql = string.Format("SELECT * FROM V_STOCKOUT WHERE LINECODE='{0}'",Linecode);
            return ExecuteQuery(sql).Tables[0];
        }
    }
}
