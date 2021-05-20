using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderTime { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? AcceptTime { get; set; }
        public int ShipperID { get; set; }
        public DateTime? ShippedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public int Status { get; set; }
    }

    public class OrderEx : Order
    {
        public List<OrderDetail> OrderDetails { get; set; }
/*        public OrderStatus OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Shipper Shipper { get; set; }*/

    }

}
