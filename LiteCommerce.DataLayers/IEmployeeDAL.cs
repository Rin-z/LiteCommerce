using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Lay Danh Sach Cua Nha cung cap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Dem so luong nha cung cap
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// lay thong tin 1 nha cung cap
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        Employee Get(int EmployeeID);

        /// <summary>
        /// bo sung 1 nha cung cap,ham tra ve ma cua nha cung cap duoc bo sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// cap nhat thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);

        /// <summary>
        /// xoa thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool Delete(int EmployeeID);
    }
}
