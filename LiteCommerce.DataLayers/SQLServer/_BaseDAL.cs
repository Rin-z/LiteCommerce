using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// chuoi tham so ket noi
        /// </summary>
        protected string _connectionString;

        public _BaseDAL(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Tao mo va ket noi den CSDL
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection() 
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this._connectionString;
            connection.Open();
            return connection;
        }
    }
}
