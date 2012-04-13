using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.AS.Sorting.Util
{
    public class MessageUtil
    {
        private string exportIP1 = "";
        private int exportPort1 = 0;
        private string exportIP2 = "";
        private int exportPort2 = 0;
        private string supplyIP = "";
        private int supplyPort = 0;
        private string sortLedIP = "";
        private int sortLedPort = 0;

        private string lineCode = "";

        public MessageUtil(THOK.MCP.Collection.AttributeCollection parameters)
        {
            exportIP1 = parameters["ExportIP1"].ToString();
            exportPort1 = Convert.ToInt32(parameters["ExportPort1"]);
            exportIP2 = parameters["ExportIP2"].ToString();
            exportPort2 = Convert.ToInt32(parameters["ExportPort2"]);
            supplyIP = parameters["SupplyIP"].ToString();
            supplyPort = Convert.ToInt32(parameters["SupplyPort"]);
            sortLedIP = parameters["SortLedIP"].ToString();
            sortLedPort = Convert.ToInt32(parameters["SortLedPort"]);

            lineCode = parameters["LineCode"].ToString();
        }

        //给1号出口终端发送包号
        public void SendToExport1(string packNo)
        {
            THOK.UDP.Client export = new THOK.UDP.Client(exportIP1, exportPort1);
            THOK.UDP.Util.MessageGenerator generator = new THOK.UDP.Util.MessageGenerator("PACKNO", "Sorting");
            generator.AddParameter("PACKNO", packNo);
            export.Send(generator.GetMessage());
        }

        //给2号出口终端发送包号
        public void SendToExport2(string packNo)
        {
            THOK.UDP.Client export = new THOK.UDP.Client(exportIP2, exportPort2);
            THOK.UDP.Util.MessageGenerator generator = new THOK.UDP.Util.MessageGenerator("PACKNO", "Sorting");
            generator.AddParameter("PACKNO", packNo);
            export.Send(generator.GetMessage());
        }

        //给补货系统发送信息
        public void SendToSupply(string orderDate, string batchNo, string sortNo, string channelGroup)
        {
            THOK.UDP.Client client = new THOK.UDP.Client(supplyIP, supplyPort);
            THOK.UDP.Util.MessageGenerator mg = new THOK.UDP.Util.MessageGenerator("SupplyRequest", "Sorting");
            mg.AddParameter("OrderDate", orderDate);
            mg.AddParameter("BatchNo", batchNo);
            mg.AddParameter("LineCode", lineCode);
            mg.AddParameter("SortNo", sortNo);
            mg.AddParameter("ChannelGroup", channelGroup == "A" ? "1" : "2");
            client.Send(mg.GetMessage());
        }
        public void SendToSortLed(string sortNo, RefreshData refreshData)
        {
            THOK.UDP.Client client = new THOK.UDP.Client(sortLedIP, sortLedPort);
            THOK.UDP.Util.MessageGenerator mg = new THOK.UDP.Util.MessageGenerator("RefreshData", "Sorting");
            mg.AddParameter("LineCode", lineCode);
            mg.AddParameter("SortNo", sortNo);

            mg.AddParameter("TotalCustomer", refreshData.TotalCustomer.ToString());
            mg.AddParameter("TotalQuantity", refreshData.TotalQuantity.ToString());
            mg.AddParameter("TotalRoute", refreshData.TotalRoute.ToString());

            mg.AddParameter("CompleteCustomer", refreshData.CompleteCustomer.ToString());
            mg.AddParameter("CompleteQuantity", refreshData.CompleteQuantity.ToString());
            mg.AddParameter("CompleteRoute", refreshData.CompleteRoute.ToString());

            mg.AddParameter("Average", refreshData.Average.ToString());

            client.Send(mg.GetMessage());
        }

        internal void SendToExport1(Dictionary<string, int> parameter)
        {
            THOK.UDP.Client export = new THOK.UDP.Client(exportIP1, exportPort1);
            THOK.UDP.Util.MessageGenerator generator = new THOK.UDP.Util.MessageGenerator("PACKNO", "Sorting");

            foreach (string key in parameter.Keys)
            {
                generator.AddParameter(key, parameter[key].ToString());
            }
            export.Send(generator.GetMessage());
        }
        internal void SendToExport2(Dictionary<string, int> parameter)
        {
            THOK.UDP.Client export = new THOK.UDP.Client(exportIP2, exportPort2);
            THOK.UDP.Util.MessageGenerator generator = new THOK.UDP.Util.MessageGenerator("PACKNO", "Sorting");

            foreach (string key in parameter.Keys)
            {
                generator.AddParameter(key, parameter[key].ToString());
            }
            export.Send(generator.GetMessage());
        }
    }
}
