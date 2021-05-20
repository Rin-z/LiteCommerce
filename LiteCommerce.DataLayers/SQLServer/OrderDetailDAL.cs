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
    public class OrderDetailDAL : _BaseDAL, IOrderDetailDAL
    {
        public OrderDetailDAL(string conn) : base(conn) { }

        public int Add(OrderDetail data)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.CommandText = @"INSERT INTO OrderDetails
                                        (OrderID,ProductID,Quantity,SalePrice)
                                        VALUES (OrderID = @OrderID,ProductID = @ProductID,Quantity = @Quantity,SalePrice = @SalePrice)
                                        SELECT @@IDENTITY";

                command.Parameters.AddWithValue("@OrderID", data.OrderID);
                command.Parameters.AddWithValue("@ProductID", data.ProductID);
                command.Parameters.AddWithValue("@Quantity", data.Quantity);
                command.Parameters.AddWithValue("@SalePrice", data.SalePrice);

                result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public bool Delete(int orderDetailID)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
                command.CommandText = @"DELETE FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";

                result = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }

        public OrderDetail Get(int orderDetailID)
        {
            OrderDetail data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dataReader.Read())
                    {
                        data = new OrderDetail()
                        {
                            OrderDetailID = Convert.ToInt32(dataReader["OrderDetailID"]),
                            ProductID = Convert.ToInt32(dataReader["ProductID"]),
                            OrderID = Convert.ToInt32(dataReader["OrderID"]),
                            Quantity = Convert.ToInt32(dataReader["Quantity"]),
                            SalePrice = Convert.ToDecimal(dataReader["SalePrice"]),
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<OrderDetail> List(int orderId)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT * FROM OrderDetails WHERE OrderID = @OrderID";
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dataReader.Read())
                    {
                        data.Add(new OrderDetail()
                        {
                            OrderDetailID = Convert.ToInt32(dataReader["OrderDetailID"]),
                            ProductID = Convert.ToInt32(dataReader["ProductID"]),
                            OrderID = Convert.ToInt32(dataReader["OrderID"]),
                            Quantity = Convert.ToInt32(dataReader["Quantity"]),
                            SalePrice = Convert.ToDecimal(dataReader["SalePrice"]),
                        });
                    }
                }

                connection.Close();
            }
            return data;
        }

        public bool Update(OrderDetail data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.CommandText = @"UPDATE OrderDetails SET OrderID = @OrderID, 
                                                            ProductID = @ProductID,
                                                            Quantity = @Quantity,
                                                            SalePrice = @SalePrice
                                        WHERE OrderDetailID = @OrderDetailID";

                command.Parameters.AddWithValue("@OrderDetailID", data.OrderDetailID);
                command.Parameters.AddWithValue("@OrderID", data.OrderID);
                command.Parameters.AddWithValue("@ProductID", data.ProductID);
                command.Parameters.AddWithValue("@Quantity", data.Quantity);
                command.Parameters.AddWithValue("@SalePrice", data.SalePrice);

                result = command.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
    }
}
