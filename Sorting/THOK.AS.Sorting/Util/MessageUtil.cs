using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.AS.Sorting.Util
{
    public class MessageUtil
    {
        private string exportIP = "";
        private int exportPort = 0;
        private string supplyIP = "";
        private int supplyPort = 0;
        private string sortLedIP = "";
        private int sortLedPort = 0;

        private string lineCode = "";

        public MessageUtil(THOK.MCP.Collection.AttributeCollection parameters)
        {
            exportIP = parameters["ExportIP"].ToString();
            exportPort = Convert.ToInt32(parameters["ExportPort"]);
            supplyIP = parameters["SupplyIP"].ToString();
            supplyPort = Convert.ToInt32(parameters["SupplyPort"]);
            sortLedIP = parameters["SortLedIP"].ToString();
            sortLedPort = Convert.ToInt32(parameters["SortLedPort"]);

            lineCode = parameters["LineCode"].ToString();
        }
        //给出口终端发送信息！
        public void SendToExport(string sortNo, string channelGroup)
        {
            THOK.UDP.Client export = new THOK.UDP.Client(exportIP, exportPort);
            THOK.UDP.Util.MessageGenerator generator = new THOK.UDP.Util.MessageGenerator("SORTNO", "Sorting");
            generator.AddParameter("SORTNO", sortNo);
            generator.AddParameter("ChannelGroup", channelGroup == "A" ? "1" : "2");
            export.Send(generator.GetMessage());
        }
        //给补货系统发送信息
        public void SendToSupply(string orderDate,string batchNo,string sortNo, string channelGroup)
        {
            THOK.UDP.Client client = new THOK.UDP.Client(supplyIP, supplyPort);
            THOK.UDP.Util.MessageGenerator mg = new THOK.UDP.Util.MessageGenerator("SupplyRequest", "Sorting");
            mg.AddParameter("OrderDate", orderDate);
            mg.AddParameter("BatchNo", batchNo);
            mg.AddParameter("LineCode", lineCode);
            mg.AddParameter("SortNo", sortNo);
            mg.AddParameter("ChannelGroup", channelGroup == "A"?"1":"2");
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

        internal void SendToExport(Dictionary<string, int> parameter)
        {
            THOK.UDP.Client export = new THOK.UDP.Client(exportIP, exportPort);
            THOK.UDP.Util.MessageGenerator generator = new THOK.UDP.Util.MessageGenerator("SORTNO", "Sorting");

            foreach (string key in parameter.Keys)
            {
                generator.AddParameter(key, parameter[key].ToString());
            }
            export.Send(generator.GetMessage());
        }
    }
}
