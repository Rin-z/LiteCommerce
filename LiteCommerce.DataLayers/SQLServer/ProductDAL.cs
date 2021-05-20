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

        public long AddGallery(ProductGallery data)
        {
            int galleryId = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductGallery (ProductID, Photo, Description, DisplayOrder, IsHidden)
                                            VALUES (@ProductID, @Photo, @Description, @DisplayOrder, @IsHidden)
                                            SELECT @@IDENTiTY";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                galleryId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return galleryId;
        }

        public long Addtrribute(ProductAttribute data)
        {
            int AttributeID;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into ProductAttributes (ProductID,AttributeName,AttributeValue,DisplayOrder) values " +
                    "(@ProductID,@AttributeName,@AttributeValue,@DisplayOrder) Select @@IDENTITY";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Connection = connection;
                AttributeID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return AttributeID;
        }


        public int Count(int categoryId, int supplierId, string searchValue)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (SqlConnection connection = GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"
                       select COUNT(*) from Categories INNER JOIN
                             Products ON Categories.CategoryID = Products.CategoryID INNER JOIN
                             Suppliers ON Products.SupplierID = Suppliers.SupplierID
				       where  ((@searchValue =N'') or (ProductName like @searchValue) )
                                and (@categoryID=N'' or @categoryID = Products.CategoryID) 
                                and (@suppID =N'' or @suppID = Products.SupplierID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryID", categoryId);
                cmd.Parameters.AddWithValue("@suppID", supplierId);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return count;
        }


        public bool DeleAddtrribute(long attributeID)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from ProductAttributes Where AttributeID = @AttributeID";
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);
                int rowAffect = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public bool Delete(int ProductID)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"
                                    DELETE From ProductAttributes where ProductID = @ProductID 
                                                                  AND NOT EXISTS(SELECT * FROM OrderDetails where ProductID = @ProductID)
                                    DELETE From ProductGallery where ProductID = @ProductID 
                                                                  AND NOT EXISTS(SELECT * FROM OrderDetails where ProductID = @ProductID)
                                    DELETE FROM Products
                                            WHERE(ProductID = @ProductID)
                                            AND NOT EXISTS(SELECT * FROM OrderDetails where ProductID = @ProductID)
                                   ";
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                int rowAffect = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public bool DeleteGallery(long galleryID)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from ProductGallery Where GalleryID = @GalleryID";
                cmd.Parameters.AddWithValue("@GalleryID", galleryID);
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
                cmd.CommandText = "Select * from Products Where ProductID = @ProductID " ;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dataReader.Read())
                    {
                            data = new Product()
                            {
                                ProductID = Convert.ToInt32(dataReader["ProductID"]),
                                ProductName = Convert.ToString(dataReader["ProductName"]),
                                SupplierID = Convert.ToInt32(dataReader["SupplierID"]),
                                CategoryID = Convert.ToInt32(dataReader["CategoryID"]),
                                Photo = Convert.ToString(dataReader["Photo"]),
                                Unit = Convert.ToString(dataReader["Unit"]),
                                Price = Convert.ToDecimal(dataReader["Price"]),

                            };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute data = null;
        
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from ProductAttributes Where AttributeID = @AttributeID order by DisplayOrder ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);
             
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        data = new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt64(dataReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dataReader["ProductID"]),
                            AttributeName = Convert.ToString(dataReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dataReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dataReader["DisplayOrder"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public ProductEX GetEX(int productID)
        {
            Product product = Get(productID);
            if (product == null)
            {
                return null;
            }

            List<ProductAttribute> listAttributes = this.ListAttributes(productID);
            List<ProductGallery> listGallery = this.ListGalleries(productID);
            ProductEX data = new ProductEX()
            {
                ProductID = product.ProductID,
                CategoryID = product.CategoryID,
                Photo = product.Photo,
                Price = product.Price,
                ProductName = product.ProductName,
                SupplierID = product.SupplierID,
                Unit = product.Unit,
                Attributes = listAttributes,
                Galleries = listGallery
            };
            return data;
        }

        public ProductGallery GetGallery(long galleryID)
        {
          
            ProductGallery data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from ProductGallery Where GalleryID = @GalleryID order by DisplayOrder";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@GalleryID", galleryID);
              
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dataReader.Read())
                    {
                        data = new ProductGallery()
                        {
                            GalleryID = Convert.ToInt64(dataReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dataReader["ProductID"]),
                            Photo = Convert.ToString(dataReader["Photo"]),
                            Description = Convert.ToString(dataReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dataReader["DisplayOrder"]),
                            IsHidden = Convert.ToBoolean(dataReader["IsHidden"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Product> List(int page, int pageSize, string searchValue,int categoryID,int supplierID)
        {
            List<Product> data = new List<Product>();

            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";



            using (SqlConnection connection =GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM    Products 
                                        WHERE   (@categoryId = 0 OR CategoryId = @categoryId)
                AND  (@supplierId = 0 OR SupplierId = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                //@pageSize =-1 => phaan chia truong hop phan trang
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@categoryId", categoryID);
                cmd.Parameters.AddWithValue("@supplierId", supplierID);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"]),

                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<ProductAttribute> ListAttributes(int productId)
        {
            {

                List<ProductAttribute> data = new List<ProductAttribute>();
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select * from ProductAttributes where ProductID = @ProductID order by DisplayOrder";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    cmd.Connection = connection;

                    using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new ProductAttribute()
                            {
                                AttributeID = Convert.ToInt64(dataReader["AttributeID"]),
                                ProductID = Convert.ToInt32(dataReader["ProductID"]),
                                AttributeName = Convert.ToString(dataReader["AttributeName"]),
                                AttributeValue = Convert.ToString(dataReader["AttributeValue"]),
                                DisplayOrder = Convert.ToInt32(dataReader["DisplayOrder"]),
                            });
                        }
                    }
                    connection.Close();
                }
                return data;
            }
        }

        public List<ProductGallery> ListGalleries(int productId)
        {
            {
                List<ProductGallery> data = new List<ProductGallery>();
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select * from ProductGallery  where ProductID = @ProductID order by DisplayOrder";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    cmd.Connection = connection;

                    using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new ProductGallery()
                            {
                                GalleryID = Convert.ToInt64(dataReader["GalleryID"]),
                                ProductID = Convert.ToInt32(dataReader["ProductID"]),
                                Photo = Convert.ToString(dataReader["Photo"]),
                                Description = Convert.ToString(dataReader["Description"]),
                                DisplayOrder = Convert.ToInt32(dataReader["DisplayOrder"]),
                                IsHidden = Convert.ToBoolean(dataReader["IsHidden"]),
                            });
                        }
                    }
                    connection.Close();
                }
                return data;
            }
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

        public bool UpdateAddtrribute(ProductAttribute data)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update ProductAttributes set ProductID = @ProductID, AttributeName=@AttributeName,AttributeValue=@AttributeValue,DisplayOrder=@DisplayOrder  Where AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return resuft;
        }

        public bool UpdateGallery(ProductGallery data)
        {
            bool resuft = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update ProductGallery set ProductID = @ProductID, Photo=@Photo,Description=@Description,DisplayOrder=@DisplayOrder,IsHidden =@IsHidden  Where GalleryID = @GalleryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@GalleryID", data.GalleryID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return resuft;
        }
    }
}
