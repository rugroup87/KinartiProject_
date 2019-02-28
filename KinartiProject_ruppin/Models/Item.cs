using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Item
    {
        public string ItemNum { get; set; }
        public string ItemName { get; set; }
        public string ItemStatus { get; set; }
        public double ItemCompletedPercentage { get; set; }
        public int ItemGroupCount { get; set; }

        public Item(string itemnum, string itemname, string itemstatus, double itemcompletedpercentage, int itemgroupcount)
        {
            ItemNum = itemnum;
            ItemName = itemname;
            ItemStatus = itemstatus;
            ItemCompletedPercentage = itemcompletedpercentage;
            ItemGroupCount = itemgroupcount;
        }
        public Item()
        {

        }

        public List<Item> GetProjectItems(float projNum)
        {
            DBServices dbs = new DBServices();
            List<Item> Pi = new List<Item>();
            Pi = dbs.GetProjectItems(projNum);
            return Pi;
        }
    }
}