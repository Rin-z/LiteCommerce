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
    public class SupplierDAL : _BaseDAL, ISupplierDAL
    {
        private SqlConnection connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Supplier data)
        {
            int supplierId;
            using (SqlConnection connection = GetConnection()) 
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Suppliers (SupplierName,ContactName,Address,City,PostalCode,Country,Phone)" +
                "values(@SupplierName,@ContactName,@Address,@City,@PostalCode,@Country,@Phone)Select @@IDENTITY";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Connection = connection;
                supplierId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return supplierId;
        }


        public int Count(string searchValue)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (SqlConnection connection = GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) from Suppliers where @searchValue = N'' or ContactName like @searchValue or Phone like @searchValue";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return count;
        }

        public bool Delete(int supplierId)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from Suppliers Where SupplierId = @SupplierId";
                cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                int rowAffect  = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public Supplier Get(int supplierId)
        {
            Supplier data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Suppliers Where SupplierID = @SupplierID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    
                    if (dataReader.Read())
                     {
                        if(supplierId >= 0)
                            { 
                            int SupplierID = Convert.ToInt32(dataReader["SupplierID"]);
                            string SupplierName = Convert.ToString(dataReader["SupplierName"]);
                            string ContactName = Convert.ToString(dataReader["ContactName"]);
                            string Address = Convert.ToString(dataReader["Address"]);
                            string City = Convert.ToString(dataReader["City"]);
                            string PostalCode = Convert.ToString(dataReader["PostalCode"]);
                            string Country = Convert.ToString(dataReader["Country"]);
                            string Phone = Convert.ToString(dataReader["Phone"]);
                            data = new Supplier(SupplierID, SupplierName, ContactName, Address, City, PostalCode, Country, Phone);
                        }

                    }
                }
            connection.Close();
            }
            

            return data;
            
            
        }

        public List<Supplier> List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (SqlConnection connection = GetConnection())
            {
                //Tạo lệnh thực thi truy vấn dữ liệu
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from
                                          ( select	row_number() over (order by SupplierName) as RowNumber,Suppliers.* from	Suppliers
	                                            where(@searchValue = N'') or (ContactName like @searchValue) or(Phone like @searchValue)
                                          ) as t
                                          where t.RowNumber between (@page-1)*@pageSize+1 and @page*@pageSize order by t.RowNumber";
                //@pageSize =-1 => phaan chia truong hop phan trang
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),

                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Supplier data)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update Suppliers set SupplierName=@SupplierName,ContactName=@ContactName,Address=@Address" +
                    ",City=@City,PostalCode=@PostalCode,Country=@Country,Phone=@Phone Where SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();

            }
            return resuft;
        }
    }
}
