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
        public List<Part> ItemParts = new List<Part>();

        public Item(string itemnum, string itemname, string itemstatus, float projectnum, string projectname)
        {
            ItemNum = itemnum;
            ItemName = itemname;
            ItemStatus = itemstatus;
            ProjectNum = projectnum;
            ProjectName = projectname;
        }
        public Item(string itemnum, List<Part> itemparts, string itemstatus = "עוד לא התחיל")
        {
            //In this constractor we will have to add ==string itemname== that we dont have now in the excel file
            ItemNum = itemnum;
            ItemStatus = itemstatus;
            //ItemName = itemname;
            ItemParts = itemparts;
            //AddNewItemToDB();
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

        //public void AddNewItemToDB()
        //{
        //    //הגישה תיהיה דרך this.
        //    DBServices dbs = new DBServices();
        //    dbs.AddNewItemToDB(this);
        //}
    }
    
}