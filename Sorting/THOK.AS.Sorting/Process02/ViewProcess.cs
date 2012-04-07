using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.AS.Sorting.View;
using System.Windows.Forms;

namespace THOK.AS.Sorting.Process
{
    public class ViewProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            THOK.MCP.View.ViewClickArgs e = (THOK.MCP.View.ViewClickArgs)stateItem.State;

            Logger.Info(string.Format("查询 {0} {1} 订单信息！", e.DeviceClass, e.DeviceNo));

            int sortNo = 0;
            int sortNoStart = 0;
            int frontQuantity = 0;
            int laterQuantity = 0;
            int channelGroup = 0;
            int exportNo = 0;
            int deviceNo = 0;
            //WriteToProcess("CacheOrderProcess", "CacheOrderSortNoesA", stateCacheA);
            //WriteToProcess("CacheOrderProcess", "CacheOrderSortNoesB", stateCacheB);
            //WriteToProcess("CacheOrderProcess", "CacheOrderSortNoesBarCode1", stateBarCode1);
            //WriteToProcess("CacheOrderProcess", "CacheOrderSortNoesBarCode2", stateBarCode2);
            //WriteToProcess("CacheOrderProcess", "CacheOrderSortNoesPacker1", statePacker1);
            //WriteToProcess("CacheOrderProcess", "CacheOrderSortNoesPacker2", statePacker2);
            switch (e.DeviceClass)
            {
                //缓存段去除挡板，改用多沟带。
                case "多沟带缓存段":
                    switch (e.DeviceNo)
                    {
                        case 1:
                        case 2:
                            int[] sortNoesB = new int[3];
                            object stateCacheB = Context.Services["SortPLC"].Read("CacheOrderSortNoesB");
                            if (stateCacheB is Array)
                            {
                                Array arrayCacheB = (Array)stateCacheB;
                                if (arrayCacheB.Length == 3)
                                {
                                    arrayCacheB.CopyTo(sortNoesB, 0);
                                    sortNoStart = sortNoesB[0];
                                    frontQuantity = sortNoesB[2];
                                    laterQuantity = sortNoesB[1];
                                }
                            }
                            channelGroup = 2;
                            deviceNo = e.DeviceNo;
                            break;
                        case 3:
                        case 4:
                            int[] sortNoesA = new int[3];
                            object stateCacheA = Context.Services["SortPLC"].Read("CacheOrderSortNoesA");
                            if (stateCacheA is Array)
                            {
                                Array arrayCacheA = (Array)stateCacheA;
                                if (arrayCacheA.Length == 3)
                                {
                                    arrayCacheA.CopyTo(sortNoesA, 0);
                                    sortNoStart = sortNoesA[0];
                                    frontQuantity = sortNoesA[2];
                                    laterQuantity = sortNoesA[1];
                                }
                            }
                            channelGroup = 1;
                            deviceNo = e.DeviceNo;
                            break;
                        default:
                            break;

                    }
                    CacheOrderQueryForm cacheOrderQueryForm1 = new CacheOrderQueryForm(deviceNo, channelGroup, sortNoStart, frontQuantity, laterQuantity);
                    cacheOrderQueryForm1.Paint += new PaintEventHandler(cacheOrderQueryForm1.CacheOrderQueryFormPaint);//窗体重绘加载颜色
                    cacheOrderQueryForm1.ShowDialog(Application.OpenForms["MainForm"]);
                    break;
                case "打码缓存段":
                    if (e.DeviceNo == 5)
                    {
                        deviceNo = 5;
                        int[] sortNoesBarCode1 = new int[2];
                        object stateBarCode1 = Context.Services["SortPLC"].Read("CacheOrderSortNoesBarCode1");
                        if (stateBarCode1 is Array)
                        {
                            Array arrayBarCode1 = (Array)stateBarCode1;
                            if (arrayBarCode1.Length == 2)
                            {
                                arrayBarCode1.CopyTo(sortNoesBarCode1, 0);
                                sortNo = sortNoesBarCode1[0];
                                channelGroup = sortNoesBarCode1[1];
                            }
                        }
                    }
                    else if (e.DeviceNo == 6)
                    {
                        deviceNo = 6;
                        int[] sortNoesBarCode2 = new int[2];
                        object stateBarCode2 = Context.Services["SortPLC"].Read("CacheOrderSortNoesBarCode2");
                        if (stateBarCode2 is Array)
                        {
                            Array arrayBarCode2 = (Array)stateBarCode2;
                            if (arrayBarCode2.Length == 2)
                            {
                                arrayBarCode2.CopyTo(sortNoesBarCode2, 0);
                                sortNo = sortNoesBarCode2[0];
                                channelGroup = sortNoesBarCode2[1];
                            }
                        }
                    }
                    CacheOrderQueryForm cacheOrderQueryForm2 = new CacheOrderQueryForm(deviceNo, channelGroup, sortNo);
                    cacheOrderQueryForm2.Text = "打码缓存段:";
                    cacheOrderQueryForm2.ShowDialog(Application.OpenForms["MainForm"]);
                    break;
                case "包装缓存段":
                    if (e.DeviceNo == 7)
                    {
                        int[] sortNoesPacker1 = new int[2];
                        object statePacker1 = Context.Services["SortPLC"].Read("CacheOrderSortNoesPacker1");
                        if (statePacker1 is Array)
                        {
                            Array arrayPacker1 = (Array)statePacker1;
                            if (arrayPacker1.Length == 2)
                            {
                                arrayPacker1.CopyTo(sortNoesPacker1, 0);
                                sortNo = sortNoesPacker1[0];
                                channelGroup = sortNoesPacker1[1];
                            }
                            deviceNo = e.DeviceNo;
                            exportNo = 1;
                        }
                    }
                    else if (e.DeviceNo == 8)
                    {
                        int[] sortNoesPacker2 = new int[2];
                        object statePacker2 = Context.Services["SortPLC"].Read("CacheOrderSortNoesPacker2");
                        if (statePacker2 is Array)
                        {
                            Array arrayPacker2 = (Array)statePacker2;
                            if (arrayPacker2.Length == 2)
                            {
                                arrayPacker2.CopyTo(sortNoesPacker2, 0);
                                sortNo = sortNoesPacker2[0];
                                channelGroup = sortNoesPacker2[1];
                            }
                            deviceNo = e.DeviceNo;
                            exportNo = 2;
                        }
                    }
                    CacheOrderQueryForm cacheOrderQueryForm3 = new CacheOrderQueryForm(deviceNo, channelGroup, sortNo);
                    //cacheOrderQueryForm3.Paint += new PaintEventHandler(cacheOrderQueryForm3.CacheOrderQueryForm_Paint);
                    cacheOrderQueryForm3.Text = "包装缓存段:";
                    cacheOrderQueryForm3.ShowDialog(Application.OpenForms["MainForm"]);
                    break;
                default:
                    break;
            }
        }

    }
}
