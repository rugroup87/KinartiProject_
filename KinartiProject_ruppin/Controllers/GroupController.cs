using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Web.Mvc:
using System.Web.Http;

using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class GroupController : ApiController
    {
        [HttpGet]
        [Route("api/GetGroupParts")]
        public object Get(string GroupName)
        {
            Group G = new Group();
            Part[] parts = G.GetGroupParts(GroupName);
            Group group = G.GetSpecificGroup(GroupName);
            //Route route = 
            return new {parts, group };
        }

        [HttpGet]
        [Route("api/GetGroups")]
        public IEnumerable<Group> Get(string projectNum, string itemNum)
        {
            Group G = new Group();
            Group[] Glist;
            Glist = G.GetGroups(projectNum, itemNum);
            return Glist;
        }

        public int Post([FromBody]Group group)
        {
            return group.InsertNewGroup();
        }
    }
}
