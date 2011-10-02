using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.AS.Sorting.Util;
using THOK.AS.Sorting.Dao;
using THOK.AS.Sorting.View;
using THOK.Util;

namespace THOK.AS.Sorting.Process
{
    class SortRunStatusProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                WriteToProcess("CacheOrderProcess", "CacheOrderSortNoes", null);
                
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o != null)
                {
                    string sortStatusTag = o.ToString();
                    if (sortStatusTag == "1")
                    {
                        using (PersistentManager pm = new PersistentManager())
                        {
                            SortStatusDao sortStatusDao = new SortStatusDao();
                            sortStatusDao.UpdateSortStatus(sortStatusTag);
                            sortStatusDao.InsertEfficiency();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("分拣设备运行状态信息处理失败！原因：" + e.Message);
            }
        }
    }
}
