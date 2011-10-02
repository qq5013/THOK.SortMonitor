using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.AS.Sorting.Dao;
using THOK.Util;

namespace THOK.AS.Sorting.Dal
{
    public class ChannelDal
    {
        public DataTable GetChannel()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannel();
            }
        }

        public DataTable GetChannel(string sortNo, string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelQuantity(sortNo,status);
            }
        }

        public DataTable GetEmptyChannel(string channelCode,string channelType, int channelGroup)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindEmptyChannel(channelCode,channelType, channelGroup);
            }
        }
        
        public DataTable GetChannelCode(string channelcode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelCode(channelcode);
            }
        }

        public DataTable GetChannelQuantity(string status)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                string sortNo = orderDao.FindLastSortNo(status);

                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelQuantity(sortNo,status);
            }
        }

        public bool ExechangeChannel(string sourceChannel, string targetChannel, out int sourceChannelAddress, out int targetChannelAddress)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                OrderDao orderDao = new OrderDao();
                try
                {
                    pm.BeginTransaction();
                    DataTable sourceChannelTable = channelDao.FindChannel(sourceChannel);//获取欲交换的烟道
                    DataTable targetChannelTable = channelDao.FindChannel(targetChannel);//获取要交换的目的烟道

                    sourceChannelAddress = Convert.ToInt32(sourceChannelTable.Rows[0]["CHANNELADDRESS"]);
                    targetChannelAddress = Convert.ToInt32(targetChannelTable.Rows[0]["CHANNELADDRESS"]);

                    channelDao.UpdateChannel(targetChannel,
                        sourceChannelTable.Rows[0]["CIGARETTECODE"].ToString(),
                        sourceChannelTable.Rows[0]["CIGARETTENAME"].ToString(), 
                        Convert.ToInt32(sourceChannelTable.Rows[0]["QUANTITY"]),
                        sourceChannelTable.Rows[0]["SORTNO"].ToString());

                    channelDao.UpdateChannel(sourceChannel,
                        targetChannelTable.Rows[0]["CIGARETTECODE"].ToString(),
                        targetChannelTable.Rows[0]["CIGARETTENAME"].ToString(),
                        Convert.ToInt32(targetChannelTable.Rows[0]["QUANTITY"]),
                        targetChannelTable.Rows[0]["SORTNO"].ToString());

                    orderDao.UpdateChannel(sourceChannel, "0000");
                    orderDao.UpdateChannel(targetChannel, sourceChannel);
                    orderDao.UpdateChannel("0000", targetChannel);

                    pm.Commit();
                    return true;
                }
                catch
                {                    
                    pm.Rollback();
                    sourceChannelAddress = 0;
                    targetChannelAddress = 0;
                    return false;
                }
            }
        }
    }
}
