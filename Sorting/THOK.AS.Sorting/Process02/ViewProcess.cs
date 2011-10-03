using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.AS.Sorting.View;
using System.Windows.Forms;

namespace THOK.AS.Sorting.Process
{
    public class ViewProcess: AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            THOK.MCP.View.ViewClickArgs e = (THOK.MCP.View.ViewClickArgs)stateItem.State;

            Logger.Info(string.Format("查询 {0} {1} 订单信息！", e.DeviceClass, e.DeviceNo));

            int sortNo = 0;
            int sortNoStart = 0;
            int sumQuantity = 0;//缓存段卷烟数量
            int channelGroup = 0;
            int exportNo = 0;
            int deviceNo = 0;
            string packMode = "";
            int[] sortNoes = new int[17];

            object state = Context.Services["SortPLC"].Read("CacheOrderSortNoes");
            WriteToProcess("CacheOrderProcess", "CacheOrderSortNoes", state);
            if (state is Array && e.DeviceNo > 0)
            {
                Array array = (Array)state;
                if (array.Length == 17)
                {
                    array.CopyTo(sortNoes, 0);


                    switch (e.DeviceClass)
                    {
                        //缓存段去除挡板，改用多沟带。
                        case "多沟带缓存段":
                            switch (e.DeviceNo)
                            {
                                case 1:
                                    sortNoStart = sortNoes[0];
                                    sumQuantity = sortNoes[1];
                                    channelGroup = 1;
                                    deviceNo = 1;
                                    break;
                                case 2:
                                    sortNoStart = sortNoes[0];
                                    sumQuantity = sortNoes[2];
                                    channelGroup = 1;
                                    deviceNo = 2;
                                    break;
                                case 3:
                                    sortNoStart = sortNoes[3];
                                    sumQuantity = sortNoes[4];
                                    channelGroup = 2;
                                    deviceNo = 3;
                                    break; ;
                                case 4:
                                    sortNoStart = sortNoes[3];
                                    sumQuantity = sortNoes[5];
                                    channelGroup = 2;
                                    deviceNo = 4;
                                    break;
                                default:
                                    break;

                            }
                            CacheOrderQueryForm cacheOrderQueryForm1 = (new CacheOrderQueryForm(deviceNo, channelGroup, sortNoStart, sumQuantity));
                            cacheOrderQueryForm1.Paint += new PaintEventHandler(cacheOrderQueryForm1.CacheOrderQueryFormPaint);//窗体重绘加载颜色
                            cacheOrderQueryForm1.Text = "多沟带缓存段:";
                            cacheOrderQueryForm1.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        case "打码缓存段":
                            if (e.DeviceNo == 5)
                            {
                                sortNo = sortNoes[8];
                                channelGroup = sortNoes[9];
                                deviceNo = 1;
                            }
                            else if (e.DeviceNo == 6)
                            {
                                sortNo = sortNoes[10];
                                channelGroup = sortNoes[11];
                                deviceNo = 2;
                            }
                            CacheOrderQueryForm cacheOrderQueryForm2 = new CacheOrderQueryForm(deviceNo, channelGroup, sortNo);
                            cacheOrderQueryForm2.Text = "打码缓存段:";
                            cacheOrderQueryForm2.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        case "包装缓存段":
                            if (e.DeviceNo == 7)
                            {
                                sortNo = sortNoes[12];
                                channelGroup = sortNoes[13];
                                exportNo = 1;
                            }
                            else if (e.DeviceNo == 8)
                            {
                                sortNo = sortNoes[14];
                                channelGroup = sortNoes[15];
                                exportNo = 2;
                            }
                            packMode = sortNoes[16].ToString();
                            CacheOrderQueryForm cacheOrderQueryForm3 = new CacheOrderQueryForm(packMode, exportNo, sortNo, channelGroup);
                            cacheOrderQueryForm3.Paint += new PaintEventHandler(cacheOrderQueryForm3.CacheOrderQueryForm_Paint);
                            cacheOrderQueryForm3.Text = "包装缓存段:";
                            cacheOrderQueryForm3.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
