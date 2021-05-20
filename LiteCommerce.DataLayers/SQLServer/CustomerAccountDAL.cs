using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CustomerAccountDAL :_BaseDAL,IAccountDAL
    {
        /// <summary>
        /// cac tinh lien quan den tai khoan khach hang
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {

        }

        public Account Authorize(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
