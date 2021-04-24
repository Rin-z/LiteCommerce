using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien qua den nha cung cap
    /// <param name = "page">Trang can lay du lieu</param>
    /// <param name="pageSize">so dong tren moi trang</param>
    /// <param name=" SearchValue">Gia tri can tim(neu rong neu lay toan bo)</param>
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Lay Danh Sach Cua Nha cung cap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Supplier> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Dem so luong nha cung cap
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// lay thong tin 1 nha cung cap
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        Supplier Get(int supplierId);

        /// <summary>
        /// bo sung 1 nha cung cap,ham tra ve ma cua nha cung cap duoc bo sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);
        /// <summary>
        /// cap nhat thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// xoa thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        bool Delete(int supplierId);

    }

}
