using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Tai Khoan Dang Nhap He Thong
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Ma Tai khoan (ma Employee, ma customer)
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// ten goi
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// photo
        /// </summary>
        public string Photo { get; set; }
    }
}
