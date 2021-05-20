using LiteCommerce.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class CookieHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AccountToCookieString(Account value)
        {
            return JsonConvert.SerializeObject(value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Account CookieStringToAccount(string value)
        {
            return JsonConvert.DeserializeObject<Account>(value);
        }
    }
}