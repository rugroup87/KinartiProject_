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
        public object Get(string GroupName, string projectNum, string itemNum)
        {
            Group G = new Group();
            Route R = new Route();
            Part[] parts = G.GetGroupParts(GroupName, projectNum, itemNum);
            Group group = G.GetSpecificGroup(GroupName, projectNum, itemNum);
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

        [HttpPost]
        [Route("api/AddingPartToExistGroup")]
        public int AddingPartToExistGroup([FromBody] dynamic partsToGroup)
        {
            Group G = new Group();
            string groupName = partsToGroup.GroupName;
            string[] partNumToAddArr = partsToGroup.ArrPart.ToObject<string[]>();
            string projectNum = partsToGroup.ProjNum;
            string itemNum = partsToGroup.ItemNum;
            //int partCountToAdd = partsToGroup.PartCountAdding;
            return G.AddingPartToExistGroup(groupName, partNumToAddArr, projectNum, itemNum);

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
        public void DeleteGroup([FromBody] dynamic deletG)
        {
            Group G = new Group();
            string groupName = deletG.GroupName;
            string[] barcodes = deletG.PartsBarCodeNo.ToObject<string[]>();


            //System.Reflection.PropertyInfo g = deletG.GetType().GetProperty("GroupName");
            //string n1 = (string)g.GetValue(deletG, null);

            G.DeleteGroup(groupName, barcodes);
        }
    }
}
