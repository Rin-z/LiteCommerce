using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
   public interface IOrderDAL
    {

        List<Order> List(int page, int pageSize, string searchValue, int status, DateTime startDate, DateTime endDate);

        int Count(string searchValue, int status, DateTime startDate, DateTime endDate);

        Order Get(int orderId);

        int Add(Order data);

        bool Update(Order data);

        bool Delete(int orderId);



    }
}
