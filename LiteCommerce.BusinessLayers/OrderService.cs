using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public static class OrderService
    {
        private static IOrderDAL OrderDB;
        private static IOrderDetailDAL OrderDetailDB;
        private static IOrderStatusDAL OrderStatusDB;

        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    OrderDB = new DataLayers.SQLServer.OrderDAL(connectionString);
                    OrderDetailDB = new DataLayers.SQLServer.OrderDetailDAL(connectionString);
                    OrderStatusDB = new DataLayers.SQLServer.OrderStatusDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }

        public static List<Order> ListOrders(int page, int pageSize, string searchValue, int status, DateTime startDate, DateTime endDate, out int rowCount)
        {
            rowCount = OrderDB.Count(searchValue, status, startDate, endDate);
            return OrderDB.List(page, pageSize, searchValue, status, startDate, endDate);
        }

        public static int Count(DateTime startDate, DateTime endDate, string searchValue = "", int status = 0)
        {
            return OrderDB.Count(searchValue, status, startDate, endDate);
        }

        public static List<OrderStatus> ListOrderStatus()
        {
            return OrderStatusDB.List();
        }
    }
}
