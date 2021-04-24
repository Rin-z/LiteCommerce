using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategoryDAL
    {
        /// <summary>
        /// Lay Danh Sach Cua Loai Hang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Category> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Dem so luong Loai hang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// lay thong tin 1 Loai Hang
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        Category Get(int CategoryID);

        /// <summary>
        /// bo sung 1 Loai Hang,ham tra ve ma cua nha cung cap duoc bo sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// cap nhat thong tin cua 1 Loai Hang
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);

        /// <summary>
        /// xoa thong tin cua 1 Loai Hang
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        bool Delete(int CategoryID);
    }
}
