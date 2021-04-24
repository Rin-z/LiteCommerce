using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien qua den thanh dat nuoc
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// lay danh sach tat ca cac quoc gia
        /// </summary>
        /// <returns></returns>
        List<Country> List();
    }
}
