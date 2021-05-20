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
    public class CategoryDAL : _BaseDAL, ICategoryDAL
    {
        private SqlConnection Connection;

        public CategoryDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Category data)
        {
            int CategoryID;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Categories (CategoryName,Description)" +
                "values(@CategoryName,@Description)Select @@IDENTITY";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Connection = connection;
                CategoryID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return CategoryID;
        }

        public int Count(string searchValue)
        {
            int result = 0;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT COUNT(*)
                                    FROM Categories
                                    WHERE CategoryName LIKE @searchValue";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");
                cmd.Connection = connection;
                result = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return result;
        }

        public bool Delete(int CategoryID)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from Categories Where CategoryID = @CategoryID";
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                int rowAffect = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public Category Get(int CategoryID)
        {
            Category data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Categories Where CategoryID = @CategoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    if (dataReader.Read())
                    {
                        if (CategoryID >= 0)
                        {
                            CategoryID = Convert.ToInt32(dataReader["CategoryID"]);
                            string CategoryName = Convert.ToString(dataReader["CategoryName"]);
                            string Description = Convert.ToString(dataReader["Description"]);
                            data = new Category(CategoryID, CategoryName, Description);
                        }

                    }
                }
                connection.Close();
            }


            return data;
        }

        public List<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                    (
	                                    SELECT *, ROW_NUMBER() OVER (ORDER BY CategoryID) AS 'STT' FROM Categories
	                                    WHERE (CategoryName LIKE @searchValue)
                                    ) AS B
                                    WHERE B.STT BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Category data)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update Categories set CategoryName=@CategoryName,Description=@Description Where CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return resuft;
        }
    }
}
