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
    public class CustomerDAL : _BaseDAL, ICustomerDAL
    {
        private SqlConnection connection;

        public CustomerDAL(string connectionString) : base(connectionString)
        {
            
        }


        public int Add(Customer data)
        {
            int result = 0;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Customers
                                    (
	                                    CustomerName,
	                                    ContactName,
	                                    Address,
	                                    City,
	                                    PostalCode,
	                                    Country,
	                                    Email
                                    ) 
                                    VALUES
                                    (
	                                    @CustomerName,
	                                    @ContactName,
	                                    @Address,
	                                    @City,
	                                    @PostalCode,
	                                    @Country,
	                                    @Email
                                    )

                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.CommandType = System.Data.CommandType.Text;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return result;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using(SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) from Customers where @searchValue = N'' or ContactName like @searchValue or CustomerName like @searchValue";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;

        }

        public bool Delete(int customerID)
        {
            bool result = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Customers
                                    WHERE CustomerID = @customerId";
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                int rowsEffected = cmd.ExecuteNonQuery();
                result = rowsEffected > 0;

                connection.Close();
            }

            return result;
        }

        public Customer Get(int CustomerID)
        {
            Customer data = null;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = CustomerID,
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Email = Convert.ToString(dbReader["Email"])
                        };
                    }
                }

                connection.Close();
            }

            return data;
        }

        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using(SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from
                                          ( select	row_number() over (order by CustomerName) as RowNumber,Customers.* from	Customers
	                                            where(@searchValue = N'') or (ContactName like @searchValue) or(CustomerName like @searchValue)
                                          ) as t
                                          where t.RowNumber between (@page-1)*@pageSize+1 and @page*@pageSize order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                using(SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        data.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(dataReader["CustomerID"]),
                            CustomerName = Convert.ToString(dataReader["CustomerName"]),
                            ContactName = Convert.ToString(dataReader["ContactName"]),
                            Address = Convert.ToString(dataReader["Address"]),
                            City = Convert.ToString(dataReader["City"]),
                            Country = Convert.ToString(dataReader["Country"]),
                            PostalCode = Convert.ToString(dataReader["PostalCode"]),
                            Email = Convert.ToString(dataReader["Email"]),
                            Password = Convert.ToString(dataReader["Password"]),

                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Customer data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Customers 
                                    SET CustomerName = @CustomerName,
	                                    ContactName = @ContactName,
	                                    Address = @Address,
	                                    City = @City,
	                                    PostalCode = @PostalCode,
	                                    Country = @Country,
	                                    Email = @Email
                                    WHERE CustomerID = @CustomerID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);


                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }
    }
}
