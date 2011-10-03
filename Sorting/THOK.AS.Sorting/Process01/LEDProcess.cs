using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Sorting.Dao;
using THOK.AS.Sorting.Util;

namespace THOK.AS.Sorting.Process
{
    public class LEDProcess : AbstractProcess
    {
        [Serializable]
        public class RestartState
        {
            public bool IsRestart = false;
        }

        private RestartState restartState = new RestartState();

        private LEDUtil ledUtil = null;
        private Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);

                Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
                string[] ledConfig = context.Attributes["IsActiveLeds"].ToString().Split(';');

                foreach (string led in ledConfig)
                {
                    if (network.Ping(led.Split(',')[1]))
                    {
                        isActiveLeds.Add(Convert.ToInt32(led.Split(',')[0]), led.Split(',')[1]);
                    }
                    else
                    {
                        Logger.Error(Convert.ToInt32(led.Split(',')[0]) + "号LED屏故障，请检查！IP:[" + led.Split(',')[1] + "]");
                    }
                }

                restartState = Util.SerializableUtil.Deserialize<RestartState>(true, @".\RestartState.sl");
                ledUtil = new LEDUtil();
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LEDProcess 初始化失败！原因：{0}！ {1}", e.Message, "LEDProcess.cs 行号：51！"));
            }
        }

        public override void Release()
        {
            try
            {
                ledUtil.Release();
                base.Release();
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LEDProcess 释放资源失败！原因：{0}！ {1}", e.Message, "LEDProcess.cs 行号：64！"));
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                switch (stateItem.ItemName)
                {
                    case "NewData"://由下载按钮事件发出  
                        // 写空仓操作流水号
                        WriteChannelDataToPLC();
                        //写重新开始分拣标志
                        WriteRestartDataToPLC();

                        //处理新数据标志。
                        restartState.IsRestart = true;
                        Util.SerializableUtil.Serialize(true, @".\RestartState.sl", restartState);

                        //LED显示盘点数据和卷烟品牌
                        Show("A", false);
                        Show("B", false);

                        break;
                    case "Check"://由盘点按钮事件发出
                        if (!restartState.IsRestart)
                        {
                            object statea = Context.Services["SortPLC"].Read("CheckA");
                            if (statea is Array)
                            {
                                Array array = (Array)statea;
                                if (array.Length == 40)
                                {
                                    //LED显示盘点数据和卷烟品牌
                                    int[] quantity = new int[40];
                                    array.CopyTo(quantity, 0);
                                    Show("A", true, quantity);
                                }
                            }
                            object stateb = Context.Services["SortPLC"].Read("CheckB");
                            if (stateb is Array)
                            {
                                Array array = (Array)stateb;
                                if (array.Length == 40)
                                {
                                    //LED显示盘点数据和卷烟品牌
                                    int[] quantity = new int[40];
                                    array.CopyTo(quantity, 0);
                                    Show("B", true, quantity);
                                }
                            }
                        }
                        else
                        {
                            Show("A", true);
                            Show("B", true);
                        }

                        break;
                    case "UnCheck"://由开始按钮事件发出                        
                        if (restartState.IsRestart)
                        {
                            restartState.IsRestart = false;
                            Util.SerializableUtil.Serialize(true, @".\RestartState.sl", restartState);
                        }

                        //延时处理，再启动分拣订单处理线程，及包装订单处理线程。                        
                        if (Context.Processes["OrderRequestProcess"] != null)
                        {
                            Context.Processes["OrderRequestProcess"].Resume();
                        }

                        if (Context.Processes["PackRequestProcess"] != null)
                        {
                            Context.Processes["PackRequestProcess"].Resume();
                        }

                        //LED不显示盘点数据，只显示卷烟品牌
                        Show("A", false);
                        Show("B", false);

                        break;
                    case "EmptyErrA":
                        //缺烟报警
                        object oa = ObjectUtil.GetObject(stateItem.State);
                        int channelAddressA = Convert.ToInt32(oa);
                        if (channelAddressA == 0)
                        {
                            ledUtil.errChannelAddress.Clear();
                            Show("A", false);
                        }
                        else
                        {
                            ledUtil.errChannelAddress.Clear();
                            if (!ledUtil.errChannelAddress.ContainsKey(channelAddressA))
                                ledUtil.errChannelAddress.Add(channelAddressA, channelAddressA);

                            if (!restartState.IsRestart)
                            {
                                object statea = Context.Services["SortPLC"].Read("CheckA");
                                if (statea is Array)
                                {
                                    Array array = (Array)statea;
                                    if (array.Length == 40)
                                    {
                                        //LED显示盘点数据和卷烟品牌
                                        int[] quantity = new int[40];
                                        array.CopyTo(quantity, 0);
                                        Show("A", true, quantity);
                                    }
                                }
                            }
                            else
                                Show("A", true);

                            ledUtil.errChannelAddress.Clear();
                        }

                        break;
                    case "EmptyErrB":
                        //缺烟报警
                        object ob = ObjectUtil.GetObject(stateItem.State);
                        int channelAddressB = Convert.ToInt32(ob);
                        if (channelAddressB == 0)
                        {
                            ledUtil.errChannelAddress.Clear();
                            Show("B", false);
                        }
                        else
                        {
                            ledUtil.errChannelAddress.Clear();
                            if (!ledUtil.errChannelAddress.ContainsKey(channelAddressB))
                                ledUtil.errChannelAddress.Add(channelAddressB, channelAddressB);
                            if (!restartState.IsRestart)
                            {
                                object stateb = Context.Services["SortPLC"].Read("CheckB");
                                if (stateb is Array)
                                {
                                    Array array = (Array)stateb;
                                    if (array.Length == 40)
                                    {
                                        //LED显示盘点数据和卷烟品牌
                                        int[] quantity = new int[40];
                                        array.CopyTo(quantity, 0);
                                        Show("B", true, quantity);
                                    }
                                }

                            }
                            else
                                Show("B", true);
                            ledUtil.errChannelAddress.Clear();
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED 更新操作失败！原因：{0}！ {1}", e.Message, "LEDProcess.cs 行号：224！"));
            }
        }

        private void Show(string channelGroup, bool checkMode, params int[] quantity)
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    ChannelDao channelDao = new ChannelDao();

                    string sortNo = "";
                    DataTable channelTable = null;

                    sortNo = orderDao.FindMaxSortedMaster(channelGroup);
                    channelTable = channelDao.FindChannelQuantity(sortNo, channelGroup);

                    DataRow[] channelRows = channelTable.Select("CHANNELTYPE='立式机'", "CHANNELNAME");

                    if (!restartState.IsRestart && checkMode && quantity.Length > 0)
                    {
                        foreach (DataRow row in channelRows)
                        {
                            row["REMAINQUANTITY"] = Convert.ToInt32(row["REMAINQUANTITY"]) + quantity[Convert.ToInt32(row["CHANNELADDRESS"]) - 1];
                        }
                    }

                    ledUtil.Show(isActiveLeds, channelRows, checkMode);
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("LED SHOW 操作失败！原因：{0}！ {1}", e.Message, "LEDProcess.cs 行号：258！"));
            }
        }

        private void WriteChannelDataToPLC()
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    ChannelDao channelDao = new ChannelDao();
                    DataTable channelTableA = channelDao.FindLastSortNo(1);//获取A线烟道
                    DataTable channeltableB = channelDao.FindLastSortNo(2);//获取B线烟道

                    int[] channelDataA = new int[40];
                    int[] channelDataB = new int[40];

                    for (int i = 0; i < channelTableA.Rows.Count; i++)
                    {
                        channelDataA[Convert.ToInt32(channelTableA.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(channelTableA.Rows[i]["SORTNO"]);
                    }

                    for (int i = 0; i < channeltableB.Rows.Count; i++)
                    {
                        channelDataB[Convert.ToInt32(channeltableB.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(channeltableB.Rows[i]["SORTNO"]);
                    }

                    WriteToService("SortPLC", "ChannelDataA", channelDataA);
                    WriteToService("SortPLC", "ChannelDataB", channelDataB);
                }
            }
            catch (Exception e)
            {
                Logger.Error("写空仓操作失败！原因：" + e.Message);
            }
        }

        private void WriteRestartDataToPLC()
        {
            try
            {
                WriteToService("SortPLC", "RestartData", 1);
            }
            catch (Exception e)
            {
                Logger.Error("写重新分拣标志操作失败！原因：" + e.Message);
            }
        }
    }
}
