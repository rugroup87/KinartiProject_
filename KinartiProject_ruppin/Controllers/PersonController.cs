using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class PersonController : ApiController
    {
        [HttpGet]
        [Route("api/persons")]
        public string Get(string department, string password)
        {
            Person p = new Person();
            return p.UserValidation(department, password);
        }
    }
}