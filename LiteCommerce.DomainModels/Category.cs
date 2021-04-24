using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Category
    {
        public Category()
        {
        }

        public Category(int categoryID, string categoryName, string description)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
            Description = description;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public string CategoryName { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public string Description { get; set; }
    }
}
