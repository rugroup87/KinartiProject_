using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Person
    {

        public string Department { get; set; }
        public string Password { get; set; }

        public Person(string department, string password)
        {
            Department = department;
            Password = password;               
        }

        public Person()
        {
                
        }
        public string UserValidation(string department, string password)
        {
            DBServices dbs = new DBServices();
            return dbs.UserValidation(department, password);
        }
    }
}