using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// cac chuc nang tac nghiep lien quan den quan ly du lieu
    /// </summary>
    public class DataService
    {
        private static ICountryDAL CountryDB;
        private static ICityDAL CityDB;
        private static ISupplierDAL SupplierDB;
        private static IEmployeeDAL EmployeeDB;
        private static ICustomerDAL CustomerDB;
        private static IShipperDAL ShipperDB;
        private static ICategoryDAL CategoryDB;

        /// <summary>
        /// Khoi Tao Lop tac nghiep 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionStrinf"></param>
        public static void Init(DatabaseTypes dbType,string connectionString)
        {
            switch(dbType)
            {
                case DatabaseTypes.SQLServer:
                    CountryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
                    CityDB = new DataLayers.SQLServer.CityDAL(connectionString);
                    SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
                    EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
                    CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
                    ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
                    CategoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
                    break;
                case DatabaseTypes.FakeDB:
                    break;
                default:
                    throw new Exception("Database Type is not supported");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListCountries()
        {
            return CountryDB.List();
        }


        /// <summary>
        /// Lay danh sach tat cac cac thanh pho
        /// </summary>
        /// <returns></returns>
        public static List<City> ListCities()
        {
            return CityDB.List();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static List<City> ListCities(string countryName)
        {
            return CityDB.List(countryName);
        } 



        /// <summary>
        /// hien thi tat nha cung acp
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListSupplier(int page,int pageSize,string searchValue,out int rowCount)
        {
            if (page <= 0)
                page = 1;
            if (pageSize <= 0)
                pageSize = 20;

            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }

        public static List<Supplier> ListOfSupplier()
        {
            return SupplierDB.List(1, 100, "");
        }
        /// <summary>
        /// hien thi 1 nha cung cap
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }

        public static IEnumerable<object> ListOfCustomerID()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// them nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }

        /// <summary>
        /// cap nhat nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }


        /// <summary>
        /// Xoa nha cung cap dua vao ma
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return SupplierDB.Delete(supplierID);
        }


        /// <summary>
        /// Hien thi tat ca nhan vien
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListEmployees(int page, int pageSize,string searchValue,out int rowCount)
        {
            if (page <= 0)
                page = 1;
            if (pageSize <= 0)
                pageSize = 5;
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        /// hien thi 1 nhan vien
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int EmployeeID)
        {
            return EmployeeDB.Get(EmployeeID);
        }

        /// <summary>
        /// them nhan vien
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }

        /// <summary>
        /// cap nhat nhan vien
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }


        /// <summary>
        /// Xoa 1 nhan vien dua vao ma
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int EmployeeID)
        {
            return EmployeeDB.Delete(EmployeeID);
        }


       

        /// <summary>
        /// Khach Hang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
                page = 1;
            if (pageSize <= 0)
                pageSize = 25;

            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue);
        }

        public static Customer GetCustomer(int customerID)
        {
            return CustomerDB.Get(customerID);
        }

        public static int  AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }

        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }
        
        public static bool DeleteCustomer(int customerID)
        {
            return CustomerDB.Delete(customerID);
        }



        /// <summary>
        /// Loai Hang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue);
        }


        public static List<Category> ListOfCategories()
        {
            return CategoryDB.List(1,100,"");
        }

        public static Category GetCategory(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }

        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }

        public static bool UpdateCategory(Category data)
        {
            return CategoryDB.Update(data);
        }

        public static bool DeleteCategory(int categoryID)
        {
            return CategoryDB.Delete(categoryID);
        }


        /// <summary>
        /// Shipper
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }

        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }

        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }

        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }

        public static bool DeleteShipper(int shipperID)
        {
            return ShipperDB.Delete(shipperID);
        }


    }
}
