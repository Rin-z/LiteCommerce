using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(int employeeID, string lastName, string firstName, DateTime birthDate, string photo, string notes, string email, string password)
        {
            EmployeeID = employeeID;
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            Photo = photo;
            Notes = notes;
            Email = email;
            Password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID {get;set;}
   /// <summary>
   /// 
   /// </summary>
        public string LastName {get;set;}
    /// <summary>
    /// 
    /// </summary>
        public string FirstName {get;set;}
    /// <summary>
    /// 
    /// </summary>
        public DateTime BirthDate {get;set;}
    /// <summary>
    /// 
    /// </summary>
        public string Photo {get;set;}
    /// <summary>
    /// 
    /// </summary>
        public string Notes {get;set;}
    /// <summary>
    /// 
    /// </summary>
        public string Email {get;set;}
    /// <summary>
    /// 
    /// </summary>
        public string Password {get;set;}

    }
}
