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
            CacheOrderQueryForm cacheOrderQueryForm = null;

            Logger.Info(string.Format("²éÑ¯ {0} {1} ¶©µ¥ÐÅÏ¢£¡", e.DeviceClass, e.DeviceNo));
            
            int sortNo = 0;
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
                        case "¶©µ¥»º´æ¶Î":
                            switch (e.DeviceNo)
                            {
                                case 1:
                                    sortNo = sortNoes[0];
                                    channelGroup = 1;
                                    deviceNo = 1;
                                    break;
                                case 2:
                                    sortNo = sortNoes[1];
                                    channelGroup = 1;
                                    deviceNo = 2;
                                    break;
                                case 3:
                                    sortNo = sortNoes[2];
                                    channelGroup = 1;
                                    deviceNo = 3;
                                    break;
                                case 4:
                                    sortNo = sortNoes[3];
                                    channelGroup = 2;
                                    deviceNo = 1;
                                    break;
                                case 5:
                                    sortNo = sortNoes[4];
                                    channelGroup = 2;
                                    deviceNo = 2;
                                    break;
                                case 6:
                                    sortNo = sortNoes[5];
                                    channelGroup = 2;
                                    deviceNo = 3;
                                    break;
                                default:
                                    break;
                            }
                            cacheOrderQueryForm = new CacheOrderQueryForm(deviceNo, channelGroup, sortNo);
                            cacheOrderQueryForm.Text = "¶©µ¥»º´æ¶Î:";
                            cacheOrderQueryForm.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        case "°Ú¶¯»º´æ¶Î":
                            if (e.DeviceNo == 7)
                            {
                                sortNo = sortNoes[6];
                                channelGroup = sortNoes[7];
                                deviceNo = 1;
                            }
                            else if (e.DeviceNo == 8)
                            {
                                sortNo = sortNoes[8];
                                channelGroup = sortNoes[9];
                                deviceNo = 2;
                            }
                            else if (e.DeviceNo == 9)
                            {
                                sortNo = sortNoes[10];
                                channelGroup = sortNoes[11];
                                deviceNo = 3;
                            }
                            cacheOrderQueryForm = new CacheOrderQueryForm(deviceNo, channelGroup, sortNo);
                            cacheOrderQueryForm.Text = "°Ú¶¯»º´æ¶Î:";
                            cacheOrderQueryForm.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        case "°ü×°»º´æ¶Î":
                            if (e.DeviceNo == 10)
                            {
                                sortNo = sortNoes[12];
                                channelGroup = sortNoes[13];
                                exportNo = 1;
                            }
                            else if (e.DeviceNo == 11)
                            {
                                sortNo = sortNoes[14];
                                channelGroup = sortNoes[15];
                                exportNo = 2;
                            }
                            packMode = sortNoes[16].ToString();
                            cacheOrderQueryForm = new CacheOrderQueryForm(packMode, exportNo, sortNo,channelGroup);
                            cacheOrderQueryForm.Text = "°ü×°»º´æ¶Î:";
                            cacheOrderQueryForm.Paint += new PaintEventHandler(cacheOrderQueryForm.CacheOrderQueryForm_Paint);
                            cacheOrderQueryForm.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
