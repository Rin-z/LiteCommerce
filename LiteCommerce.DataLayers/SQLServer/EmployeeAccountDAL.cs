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
    /// <summary>
    /// cac tinh nang lien quan den tai khoan nhan vien
    /// </summary>
    /// <param name="connectionString"></param>
    public class EmployeeAccountDAL : _BaseDAL, IAccountDAL
    {

        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {
           

        }

        public Account Authorize(string userName, string passWord)
        {
            Account data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT EmployeeID, FirstName,Photo,LastName,Email From Employees Where Email =@email And Password =@password";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@email", userName);
                cmd.Parameters.AddWithValue("@password", passWord);

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dataReader.Read()) 
                    {
                        data = new Account()
                        {
                            AccountID = dataReader["EmployeeID"].ToString(),
                            FullName = $"{dataReader["FirstName"]} {dataReader["LastName"]}",
                            Photo = dataReader["Photo"].ToString(),
                            Email = dataReader["Email"].ToString()
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            int rowAffected;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Employees set Password = @NewPassword 
                                        where EmployeeID =@EmployeeId and Password = @OldPassword";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                cmd.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32(accountId));

                rowAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowAffected >0;
        }
    }
}
