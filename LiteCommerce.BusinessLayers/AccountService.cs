using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cap cac chuc nang lien quan den  tai khoan nguoi dung
    /// </summary>
    public static class AccountService
    {
        private static IAccountDAL AccountDB;
        public static void Init(DatabaseTypes dbType, string connectionString,AccountTypes accountType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    if (accountType == AccountTypes.Employee)
                        AccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
                    else
                        AccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database Type is supported");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Account Authorize(string userName, string password)
        {
            return AccountDB.Authorize(userName, password);
        }



       public static bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            return AccountDB.ChangePassword(accountId, oldPassword, newPassword);
        }

    }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum AccountTypes
    {
        /// <summary>
        /// Nhan vien (dung ung dung LiteCommerce.Admin)
        /// </summary>
        Employee,
        /// <summary>
        /// Khach hang (dung ung dung LiteCommerce.Customer)
        /// </summary>
        Customer
    }









