using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class ItemController : ApiController
    {
        [HttpGet]
        [Route("api/GetProjectItems")]
        public IEnumerable<Item> Get(string ProjectNum)
        {
            Item I = new Item();
            List<Item> ItemList = new List<Item>();
            if(ProjectNum != "all")
            {
                ItemList = I.GetProjectItems(float.Parse(ProjectNum));
            }
            else
            {
                ItemList = I.GetAllProjectItems();
            }
            return ItemList;
        }
    }
}