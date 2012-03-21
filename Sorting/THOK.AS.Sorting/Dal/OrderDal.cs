using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.AS.Sorting.Dao;
using THOK.Util;

namespace THOK.AS.Sorting.Dal
{
    public class OrderDal
    {
        public DataTable GetOrderMaster()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindMaster();
            }
        }

        public DataTable GetOrderDetail(string sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDetail(sortNo);
            }
        }

        public DataTable GetCigarettes()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindCigarettes();
            }
        }

        public DataTable GetOrderWithCigarette(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindOrderWithCigarette(cigaretteCode);
            }
        }

        public void ClearPackage(string orderID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.ClearPackQuantity(orderID);
            }
        }

        public void SetPackage(string orderID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.UpdatePackQuantity(orderID);
            }
        }

        public DataTable GetPackMaster()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackMaster();
            }
        }

        public DataTable GetPackMaster(string [] filter)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackMaster(filter);
            }
        }

        public DataTable GetPackDetail(string orderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackDetail(orderId);
            }
        }

        public void UpdateQuantity(string sortNo, string orderId, string channelName, string cigaretteCode,int quantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                pm.BeginTransaction();
                try
                {
                    OrderDao orderDao = new OrderDao();
                    orderDao.UpdateQuantity(sortNo, orderId, channelName, cigaretteCode, quantity);
                    pm.Commit();
                }
                catch (Exception)
                {
                    pm.Rollback();
                }
            }
        }
         
        /// <summary>
        ///  多沟带数据显示DAL方法       多沟带数据显示DAL
        /// </summary>
        /// <param name="channelGroup"></param>
        /// <param name="sortNoStart"></param>
        /// <returns></returns>
        public DataTable GetAllOrderDetailForCacheOrderQuery(int channelGroup, int sortNoStart)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDetailForCacheOrderQuery(channelGroup, sortNoStart);
            }
        }

        public DataTable GetOrderDetailForCacheOrderQuery(int channelGroup, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable table = orderDao.FindOrderIDAndOrderNoForCacheOrderQuery(channelGroup, sortNo);

                if (table.Rows.Count != 0)
                {
                    string orderId = table.Rows[0]["ORDERID"].ToString();
                    int orderNo = Convert.ToInt32(table.Rows[0]["ORDERNO"]);
                    return orderDao.FindDetailForCacheOrderQuery(orderId, orderNo, channelGroup);
                }
                return (new DataTable());
            }
        }
        
        public DataTable GetOrderDetailForCacheOrderQuery(string packMode, int exportNo, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDetailForCacheOrderQuery(exportNo, sortNo, packMode);
            }
        }

        public DataTable GetPackDataOrder(int exportNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackDataOrder(exportNo);
            }
        }
    }
}
