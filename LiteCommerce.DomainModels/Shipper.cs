using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Shipper
    {
        public Shipper()
        {
        }

        public Shipper(int shipperId, string shipperName, string phone)
        {
            ShipperId = shipperId;
            ShipperName = shipperName;
            Phone = phone;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ShipperId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipperName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

    }
}
