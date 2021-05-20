using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Dinh Nghia cac phep xu ly lien quan den tai khoan dang nhap he thong
    /// </summary>
    public interface IAccountDAL
    {
        /// <summary>
        /// kiem tra thong tin dang nhap. neu dang nhap dung  thi tra ve 1 doi tuong kieu account 
        /// thong tin cua tai khoan dang nhap . nguoc lai tra ve null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Account Authorize(string userName, string passWord);

        /// <summary>
        /// Thay doi mk cua tai khoan
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string accountId, string oldPassword, string newPassword);

    }
}
