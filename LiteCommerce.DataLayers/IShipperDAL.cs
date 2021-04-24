using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IShipperDAL
    {
        /// <summary>
        /// Lay Danh Sach Cua Shipper
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Dem so luong Shipper
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// lay thong tin 1 Shipper
        /// </summary>
        /// <param name="ShipperId"></param>
        /// <returns></returns>
        Shipper Get(int ShipperID);

        /// <summary>
        /// bo sung 1 Shipper,ham tra ve ma cua shipper duoc bo sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);

        /// <summary>
        /// cap nhat thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);

        /// <summary>
        /// xoa thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="ShipperId"></param>
        /// <returns></returns>
        bool Delete(int ShipperId);
    }
}
