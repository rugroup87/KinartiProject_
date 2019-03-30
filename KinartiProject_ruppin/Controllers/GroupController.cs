﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class GroupController : ApiController
    {
        [HttpGet]
        [Route("api/GetGroupParts")]
        public IEnumerable<Part> Get(string GroupName)
        {
            Group G = new Group();
            Part[] Plist;
            Plist = G.GetGroupParts(GroupName);
            return Plist;
        }
    }
}