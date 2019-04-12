using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//ספרייה לשימוש של ג'ייסון
using Newtonsoft.Json.Linq;
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
            Route R = new Route();
            Part[] parts = G.GetGroupParts(GroupName);
            Group group = G.GetSpecificGroup(GroupName);
            List<Route> routes = R.ReadRouteName(group.GroupRouteName);
            return new {parts, group, routes};  
        }

        [HttpGet]
        [Route("api/GetGroups")]
        public object Get(string projectNum, string itemNum)
        {
            Machine M = new Machine();
            List<Machine> mlist;
            Group G = new Group();
            Group[] Glist;
            Glist = G.GetGroups(projectNum, itemNum);
            mlist = M.GetAllMachines();
            return new {Glist, mlist };
        }

        public int Post([FromBody]Group group)
        {
            return group.InsertNewGroup();
        }

        [HttpPut]
        [Route("api/UpdateGroupEstTime")]
        public void UpdateGroupEstTime(string prepTime, string carpTime, string paintTime, string groupName)
        {
            Group G = new Group();
            G.UpdateGroupEstTime(prepTime, carpTime, paintTime, groupName);
        }

        [HttpPut]
        [Route("api/DeletePartFromGroup")]
        public string DeletePartFromGroup(string partBarcode, string PartCount, string GroupName)
        {
            Group G = new Group();
            return G.DeletePartFromGroup(partBarcode, Convert.ToInt32(PartCount), GroupName);
        }

        [HttpPut]
        [Route("api/DeleteGroup")]
        public void DeleteGroup([FromBody] String deletG)
        {

            //JToken token = JObject.Parse(deletG);
            //string s = (string)token.SelectToken("GroupName");

            //System.Reflection.PropertyInfo g = deletG.GetType().GetProperty("GroupName");
            //System.Reflection.PropertyInfo[] a = deletG.GetType().GetProperties();


            //System.Reflection.PropertyInfo b = deletG.GetType().GetProperty("PartsBarCodeNo");
            //string[] n1 = (string[])b.GetValue(deletG, null);
            //Group G = new Group();

            //G.DeleteGroup(deletG);
        }
    }
}
