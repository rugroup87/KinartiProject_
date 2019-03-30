﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class MachineController : ApiController
    {
        [HttpGet]
        [Route("api/Machine")]
        public IEnumerable<Machine> Get()
        {
            Machine m = new Machine();
            return m.Read();
        }
    }
}
