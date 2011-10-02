using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace THOK.AS.Sorting.Util
{
    class SetChannelSortNoUtil
    {
        public static void SetChannelSortNo(DataTable ChannelTable, DataTable ordersTable)
        {
            foreach (DataRow channelRow in ChannelTable.Rows)
            {
                int channelCount = 0;

                DataRow[] rows = null;

                rows = ordersTable.Select(string.Format("CHANNELCODE='{0}'", channelRow["CHANNELCODE"]), "SORTNO DESC");

                channelCount = 0;
                foreach (DataRow scOrderRow in rows)
                {
                    //记录分拣倒数第7条的订单号
                    if (channelCount <= 7 && channelCount + Convert.ToInt32(scOrderRow["QUANTITY"]) >= 7)
                    {
                        channelRow["SORTNO"] = Convert.ToInt32(scOrderRow["SORTNO"]);
                        return;
                    }
                    channelCount += Convert.ToInt32(scOrderRow["QUANTITY"]);
                }
            }
        }
    }
}
