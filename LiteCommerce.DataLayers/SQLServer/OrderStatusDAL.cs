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
    public class OrderStatusDAL : _BaseDAL, IOrderStatusDAL
    {
        public OrderStatusDAL(string conn) : base(conn) { }

        public OrderStatus Get(int statusId)
        {
            OrderStatus data = null;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM OrderStatus WHERE Status = @Status";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Status", statusId);

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dataReader.Read())
                    {
                        data = new OrderStatus()
                        {
                            Status = Convert.ToInt32(dataReader["Status"]),
                            Description = Convert.ToString(dataReader["Description"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<OrderStatus> List()
        {
            List<OrderStatus> data = new List<OrderStatus>();

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM OrderStatus";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        data.Add(new OrderStatus()
                        {
                            Status = Convert.ToInt32(dataReader["Status"]),
                            Description = Convert.ToString(dataReader["Description"])
                        });
                    }
                }

                connection.Close();
            }
            return data;
        }


    }
}
