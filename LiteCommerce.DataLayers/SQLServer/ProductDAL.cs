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
    public class ProductDAL:_BaseDAL , IProductDAL
    {
        private SqlConnection connection;

        public ProductDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Product data)
        {
            int ProductID;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Products (ProductName,CategoryID,SupplierID,Unit,Price,Photo) values (@ProductName,@CategoryID,@SupplierID,@Unit,@Price,@Photo)Select @@IDENTITY";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Connection = connection;
                ProductID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return ProductID;
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ProductID)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from Products Where ProductID = @ProductID";
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                int rowAffect = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public Product Get(int ProductID)
        {
            Product data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Products Where ProductID = @ProductID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    if (dataReader.Read())
                    {
                        if (ProductID >= 0)
                        {
                            ProductID = Convert.ToInt32(dataReader["ProductID"]);
                            string ProductName = Convert.ToString(dataReader["ProductName"]);
                            int SupplierID = Convert.ToInt32(dataReader["SupplierID"]);
                            int CategoryID = Convert.ToInt32(dataReader["CategoryID"]);
                            string Photo = Convert.ToString(dataReader["Unit"]);
                            string Unit = Convert.ToString(dataReader["Price"]);
                            string Price = Convert.ToString(dataReader["Photo"]);
                            data = new Product(ProductID, ProductName, SupplierID, CategoryID, Unit, Price, Photo);
                        }
                    }
                }
                connection.Close();
            }


            return data;
        }

        public List<Product> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product data)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update Products set ProductName=@ProductName,CategoryID=@CategoryID,SupplierID=@SupplierID,Unit=@Unit,Price=@Price,Photo=@Photo  Where ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return resuft;
        }
    }
}
