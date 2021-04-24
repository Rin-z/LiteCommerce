using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Supplier
    {
        public Supplier()
        {

        }

        public Supplier(int supplierId, string supplierName, string contactName, string address, string city, string postalCode, string country, string phone)
        {
            SupplierID = supplierId;
            SupplierName = supplierName;
            ContactName = contactName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        public static implicit operator Supplier(Shipper v)
        {
            throw new NotImplementedException();
        }
    }
}
