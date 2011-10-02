using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    class SortStatusDao : BaseDao
    {
        public void UpdateSortStatus(string sortStatusTag)
        {
            try
            {
                string sql = " UPDATE AS_SORT_STATUS " +
                                " SET ENDTIME = GETDATE() ," +
                                " 	  USETIME = DATEDIFF(second,BEGINTIME, GETDATE())" +
                                " WHERE ENDTIME IS NULL" +
                                " INSERT AS_SORT_STATUS ([TYPE],BEGINTIME,ENDTIME,USETIME) " + 
                                " VALUES ('{0}',GETDATE(),NULL,NULL)";
                ExecuteNonQuery(string.Format(sql, sortStatusTag));
            }
            catch (Exception)
            {
            }
        }

        public void InsertEfficiency()
        {
            try
            {
                string sql = " DECLARE	@return_value int " +
                             " EXEC	@return_value = [dbo].[INSERTEFFICIENCY]";
                ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
            }
        }
    }
}
