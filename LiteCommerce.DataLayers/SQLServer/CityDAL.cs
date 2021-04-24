using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CityDAL :_BaseDAL,ICityDAL
    {
        public CityDAL(string connectionString) : base(connectionString)
        {

        }
   


        public List<City> List()
        {
            return null;
        }


        public List<City> List(string CountryName)
        {
            List<City> data = new List<City>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Cities where CountryName = @CountryName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CountryName", CountryName);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        data.Add(new City()
                        {
                            CityName = Convert.ToString(dataReader["CityName"]),
                            CountryName = Convert.ToString(dataReader["CountryName"])

                        });


                    }
                }
                connection.Close();

            }
            return data;
        }

    }
}
