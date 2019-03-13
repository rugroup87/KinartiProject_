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
        public float ProjectNum { get; set; }
        public string ProjectName { get; set; }
        List<Part> ItemParts = new List<Part>();

        public Item(string itemnum, string itemname, string itemstatus, double itemcompletedpercentage, int itemgroupcount, float projectnum, string projectname)
        {
            ItemNum = itemnum;
            ItemName = itemname;
            ItemStatus = itemstatus;
            ItemCompletedPercentage = itemcompletedpercentage;
            ItemGroupCount = itemgroupcount;
            ProjectNum = projectnum;
            ProjectName = projectname;
        }
        public Item(string itemnum, string itemname,List<Part> itemparts, string itemstatus = "עוד לא התחיל")
        {
            ItemNum = itemnum;
            ItemName = itemname;
            ItemStatus = itemstatus;
            itemparts = ItemParts;
        }
        public Item()
        {

        }

        public Item[] GetProjectItems(float projNum)
        {
            DBServices dbs = new DBServices();
            //List<Item> Pi = new List<Item>();
            //Pi = dbs.GetProjectItems(projNum);
            return dbs.GetProjectItems(projNum);
        }

        public Item[] GetAllProjectItems()
        {
            DBServices dbs = new DBServices();
            //List<Item> Pi = new List<Item>();
            //Pi = dbs.GetAllProjectItems();
            return dbs.GetAllProjectItems();
        }
    }
    
}