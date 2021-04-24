using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Product
    {
        public Product()
        {
        }

        public Product(int productID, string productName, int supplierID, int categoryID, string unit, string price, string photo)
        {
            ProductID = productID;
            ProductName = productName;
            SupplierID = supplierID;
            CategoryID = categoryID;
            Unit = unit;
            Price = price;
            Photo = photo;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Unit { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public string Price { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; set; }

    }
}
