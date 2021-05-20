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
    public class OrderDAL:_BaseDAL,IOrderDAL
    {
        private SqlConnection connection;

        public OrderDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Order data)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.CommandText = @"INSERT INTO Orders (CustomerID, OrderTime,  Status)
                                        VALUES (@CustomerID, @OrderTime, @EmployeeID, @Status)
                                        SELECT @@IDENTITY";

                command.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                command.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                command.Parameters.AddWithValue("@Status", 1);

                result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public int Count(string searchValue, int status, DateTime startDate, DateTime endDate)
        {
            int result = 0;
            if (searchValue != "") searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT COUNT(Orders.OrderID)
                                    FROM    Orders
                                    JOIN Customers ON Customers.CustomerID = Orders.CustomerID
                                    WHERE (@Status = 0 OR Status = @Status)
                                            AND OrderTime BETWEEN @startDate AND @endDate
                                            AND (@searchValue = '' OR Customers.CustomerName LIKE @searchValue OR Customers.ContactName LIKE @searchValue)";
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return result;
        }

        public bool Delete(int orderId)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.Parameters.AddWithValue("@OrderID", orderId);
                command.CommandText = @"DELETE FROM Orders WHERE OrderID = @OrderID
                                                                 AND OrderID NOT IN (SELECT OrderID FROM OrderDetails)";

                result = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result; throw new NotImplementedException();
        }

        public Order Get(int orderId)
        {
            Order data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT * FROM Orders WHERE OrderID = @OrderID";
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dataReader.Read())
                    {
                        Order obj= new Order()
                        {
                            OrderID = Convert.ToInt32(dataReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dataReader["CustomerID"]),
                            OrderTime = Convert.ToDateTime(dataReader["OrderTime"]),
                            Status = Convert.ToInt32(dataReader["Status"]),
                            EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]),
                            ShipperID = Convert.ToInt32(dataReader["ShipperID"]),
                            AcceptTime = Convert.ToDateTime(dataReader["AcceptTime"]),
                            ShippedTime = Convert.ToDateTime(dataReader["ShippedTime"]),
                            FinishedTime = Convert.ToDateTime(dataReader["FinishedTime"]),
                        };
                        if (dataReader["AcceptTime"] == DBNull.Value)
                            obj.AcceptTime = null;
                        else
                            obj.AcceptTime = Convert.ToDateTime(dataReader["AcceptTime"]);

                        if (dataReader["ShippedTime"] == DBNull.Value)
                            obj.AcceptTime = null;
                        else
                            obj.AcceptTime = Convert.ToDateTime(dataReader["ShippedTime"]);

                        if (dataReader["FinishedTime"] == DBNull.Value)
                            obj.AcceptTime = null;
                        else
                            obj.AcceptTime = Convert.ToDateTime(dataReader["FinishedTime"]);

                        data = obj;

                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<Order> List(int page, int pageSize, string searchValue, int status, DateTime startDate, DateTime endDate)
        {
            if (searchValue != "") searchValue = "%" + searchValue + "%";
            List<Order> data = new List<Order>();

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT  * FROM (
                                        SELECT Orders.*, ROW_NUMBER() OVER(ORDER BY OrderID) AS RowNumber
                                        FROM    Orders
                                        JOIN Customers ON Customers.CustomerID = Orders.CustomerID
                                        WHERE (@Status = 0 OR Status = @Status)
                                            AND OrderTime BETWEEN @startDate AND @endDate
                                            AND (@searchValue = '' OR Customers.CustomerName LIKE @searchValue OR Customers.ContactName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN(@page -1)*@pageSize + 1 AND @page*@pageSize";

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        Order obj = new Order()
                        {
                            OrderID = Convert.ToInt32(dataReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dataReader["CustomerID"]),
                            Status = Convert.ToInt32(dataReader["Status"]),
                            OrderTime = Convert.ToDateTime(dataReader["OrderTime"]),
                            ShipperID = Convert.ToInt32(dataReader["ShipperID"]),
                            EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]),
                            AcceptTime = Convert.ToDateTime(dataReader["AcceptTime"]),
                            ShippedTime = Convert.ToDateTime(dataReader["ShippedTime"]),
                            FinishedTime = Convert.ToDateTime(dataReader["FinishedTime"]),
                        };
                        if(dataReader["AcceptTime"] == DBNull.Value)
                                obj.AcceptTime = null;
                        else
                            obj.AcceptTime = Convert.ToDateTime(dataReader["AcceptTime"]);

                        if (dataReader["ShippedTime"] == DBNull.Value)
                            obj.AcceptTime = null;
                        else
                            obj.AcceptTime = Convert.ToDateTime(dataReader["ShippedTime"]);

                        if (dataReader["FinishedTime"] == DBNull.Value)
                            obj.AcceptTime = null;
                        else
                            obj.AcceptTime = Convert.ToDateTime(dataReader["FinishedTime"]);


                        data.Add(obj);
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Order data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.CommandText = @"UPDATE Orders SET CustomerID = @CustomerID, 
                                                            OrderTime = @OrderTime,
                                                            EmployeeID = @EmployeeID,
                                                            AcceptTime = @AcceptTime,
                                                            ShipperID = @ShipperID,
                                                            ShippedTime = @ShippedTime,
                                                            FinishedTime = @FinishedTime,
                                                            Status = @Status,
=                                        WHERE OrderID = @OrderID";

                command.Parameters.AddWithValue("@OrderID", data.OrderID);
                command.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                command.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                command.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                command.Parameters.AddWithValue("@AcceptTime", data.AcceptTime);
                command.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                command.Parameters.AddWithValue("@ShippedTime", data.ShippedTime);
                command.Parameters.AddWithValue("@FinishedTime", data.FinishedTime);
                command.Parameters.AddWithValue("@Status", data.Status);

                result = command.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
    }
}
