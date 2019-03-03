using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class StatusController : ApiController
    {

        [HttpGet]
        [Route("api/status")]
        public string[] Get(string relateTo)
        {
            Status s = new Status();
            return s.StatusArr(relateTo);
            
            //Status s = new Status();
            //string[] arrStatus = new string[];
            //arrStatus = s.StatusArr(relateTo);
            //return arrStatus;
        }
    }
}
