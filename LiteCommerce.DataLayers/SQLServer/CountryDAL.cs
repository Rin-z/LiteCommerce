using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        public CountryDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        data.Add(new Country()
                        {
                            CountryName = Convert.ToString(dataReader["CountryName"])
                        }) ;


                    }
                }
                connection.Close();

            }
            return data;
        }
    }
}
