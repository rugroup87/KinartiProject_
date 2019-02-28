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
        public IEnumerable<Item> Get(float projNum)
        {
            Item I = new Item();
            List<Item> ItemList = new List<Item>();
            ItemList = I.GetProjectItems(projNum);
            return ItemList;
        }
    }
}