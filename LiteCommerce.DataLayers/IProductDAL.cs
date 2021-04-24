using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL
    {
         /// <summary>
        /// Lay Danh Sach Cua Product
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Dem so luong Product
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// lay thong tin 1 Product
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Product Get(int ProductID);

        /// <summary>
        /// bo sung 1 Product,ham tra ve ma cua Product duoc bo sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);

        /// <summary>
        /// cap nhat thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);

        /// <summary>
        /// xoa thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        bool Delete(int ProductID);
    }
}
