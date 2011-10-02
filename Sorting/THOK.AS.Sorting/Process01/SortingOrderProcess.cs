using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.AS.Sorting.Dao;
using THOK.Util;
using THOK.AS.Sorting.Util;

namespace THOK.AS.Sorting.Process
{
    class SortingOrderProcess : AbstractProcess
    { 
        [Serializable]
        public class OrderInfo
        {
            public string orderDate = "";
            public string batchNo = "";
        }

        private MessageUtil messageUtil = null;
        private bool isInit = true;
        private OrderInfo orderInfo = new OrderInfo();

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                messageUtil = new MessageUtil(context.Attributes);
                isInit = true;
                orderInfo = Util.SerializableUtil.Deserialize<OrderInfo>(true, @".\orderInfo.sl");
            }
            catch (Exception e)
            {
                Logger.Error("SortingOrderProcess 初始化失败！原因：" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                //获取将要分拣的流水号发给补货系统
                string channelGroup = "";
                object o = null;

                switch (stateItem.ItemName)
                {
                    case "OrderInfo":
                        o = stateItem.State;
                        if (o is Array)
                        {
                            Array array = (Array)o;
                            if (array.Length == 2)
                            {
                                string[] orderinfo = new string[2];
                                array.CopyTo(orderinfo, 0);
                                orderInfo.orderDate = orderinfo[0];
                                orderInfo.batchNo = orderinfo[1];
                            }
                        }

                        Util.SerializableUtil.Serialize(true, @".\orderInfo.sl", orderInfo);
                        return;
                        break;
                    case "SortingOrderA":
                        channelGroup = "A";
                        break;
                    case "SortingOrderB":
                        channelGroup = "B";
                        break;
                    default:
                        return;
                }

                o = ObjectUtil.GetObject(stateItem.State);
                if (o != null)
                {
                    string sortNo = o.ToString();
                    if (Convert.ToInt32(sortNo) > 0)
                    {
                        if (isInit)
                        {
                            isInit = false;
                        }
                        else
                        {
                            messageUtil.SendToSupply(orderInfo.orderDate, orderInfo.batchNo, sortNo, channelGroup);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("开始分拣订单信息处理失败！原因：" + e.Message);
            }
        }
    }
}
