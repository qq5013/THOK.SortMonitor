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

            Logger.Info(string.Format("��ѯ {0} {1} ������Ϣ��", e.DeviceClass, e.DeviceNo));

            int sortNo = 0;
            int sortNoStart = 0;
            int sumQuantity = 0;//����ξ�������
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
                        //�����ȥ�����壬���ö๵����
                        case "�๵�������":
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
                            cacheOrderQueryForm1.Paint += new PaintEventHandler(cacheOrderQueryForm1.CacheOrderQueryFormPaint);//�����ػ������ɫ
                            cacheOrderQueryForm1.Text = "�๵�������:";
                            cacheOrderQueryForm1.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        case "���뻺���":
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
                            cacheOrderQueryForm2.Text = "���뻺���:";
                            cacheOrderQueryForm2.ShowDialog(Application.OpenForms["MainForm"]);
                            break;
                        case "��װ�����":
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
                            cacheOrderQueryForm3.Text = "��װ�����:";
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
