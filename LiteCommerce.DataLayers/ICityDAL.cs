using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien qua den cac thanh pho
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// lay danh sach tat cac thanh pho
        /// </summary>
        /// <returns></returns>
        List<City> List();
        /// <summary>
        /// lay danh quoc gia thuoc thanh pho
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        List<City> List(string countryName);
    }
}
