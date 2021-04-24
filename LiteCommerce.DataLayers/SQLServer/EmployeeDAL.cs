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
    public class EmployeeDAL:_BaseDAL ,IEmployeeDAL
    {
        private SqlConnection connection;

        public EmployeeDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Employee data)
        {
            int EmployeeID;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Employees (LastName,FirstName,BirthDate,Photo,Notes,Email,PassWord)" +
                "values(@LastName,@FirstName,@BirthDate,@Photo,@Notes,@Email,@PassWord)Select @@IDENTITY";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                cmd.Connection = connection;
                EmployeeID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return EmployeeID;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT COUNT(*) searchValue FROM Employees";
                count = Convert.ToInt32(cmd.ExecuteNonQuery());
                connection.Close();
            }
            return count;
        }

        public bool Delete(int EmployeeID)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from Employees Where EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                int rowAffect = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public Employee Get(int EmployeeID)
        {
            Employee data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Employees Where EmployeeID = @EmployeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    if (dataReader.Read())
                    {
                        if (EmployeeID >= 0)
                        {
                             EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]);
                            string LastName = Convert.ToString(dataReader["LastName"]);
                            string FirstName = Convert.ToString(dataReader["FirstName"]);
                            DateTime BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                            string Photo = Convert.ToString(dataReader["Photo"]);
                            string Notes = Convert.ToString(dataReader["Notes"]);
                            string Email = Convert.ToString(dataReader["Email"]);
                            string Password = Convert.ToString(dataReader["Password"]);
                            data = new Employee(EmployeeID, LastName, FirstName, BirthDate, Photo, Notes, Email, Password);
                        }

                    }
                }
                connection.Close();
            }


            return data;
        }

        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee data)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update Employees set LastName=@LastName,FirstName=@FirstName,BirthDate=@BirthDate,Photo=@Photo,Notes=@Notes,Email=@Email,PassWord=@PassWord Where EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return resuft;
        }
    }
}
