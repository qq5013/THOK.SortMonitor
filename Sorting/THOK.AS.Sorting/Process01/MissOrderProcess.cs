using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Sorting.Dao;

namespace THOK.AS.Sorting.Process
{
    public class MissOrderProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string channelGroup = "";

                switch (stateItem.ItemName)
                {
                    case "MissOrderA":
                        channelGroup = "A";
                        break;
                    case "MissOrderB":
                        channelGroup = "B";
                        break;
                    default:
                        return;
                }

                object sortNo = ObjectUtil.GetObject(stateItem.State);
                //校正订单
                if (sortNo != null)
                {
                    if (sortNo.ToString() != "0")
                    {
                        using (PersistentManager pm = new PersistentManager())
                        {
                            OrderDao orderDao = new OrderDao();
                            orderDao.UpdateMissOrderStatus(sortNo.ToString(), channelGroup);
                            dispatcher.WriteToService("SortPLC", "UpdateMissOrder" + channelGroup, 1);
                            Logger.Info(channelGroup + " 线 校正定单" + sortNo.ToString() + "成功！");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("校正定单失败！原因：" + e.Message);
            }
        }
    }
}
