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

        public string UserVadilation(string department, string password)
        {
            DBservices dbs = new DBservices();
            string user = dbs.UserVadilation(department, password);
            return user;
        }
    }
}