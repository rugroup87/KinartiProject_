﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Group
    {
        public float ProjectNum { get; set; }
        public string ItemNum { get; set; }
        public string GroupName { get; set; }
        public string GroupStatus { get; set; }
        public string GroupRouteName { get; set; }
        public int GroupPartCount { get; set; }
        public int EstPrepTime { get; set; }
        public int EstCarpTime { get; set; }
        public int EstColorTime { get; set; }
        public string[] ArrPart { get; set; }


        public Group(float _projectNum, string _itemNum, string _groupName, string _groupRouteName,
           int _groupPartCount, int _estPrepTime, int _estColorTime, string[] _arrPart, string _groupStatus = "קבוצה מוכנה לעבודה")
        {
            ProjectNum = _projectNum;
            ItemNum = _itemNum;
            GroupName = _groupName;
            GroupRouteName = _groupRouteName;
            GroupPartCount = _groupPartCount;
            EstPrepTime = _estPrepTime;
            EstColorTime = _estColorTime;
            GroupStatus = _groupStatus;
            ArrPart = _arrPart;
        }
        //בנאי ריק
        public Group()
        {
        }

        //מחזיר לנו את החלקים של הקבוצה שנבחרה כולל את המידע של הקבוצה 
        public Part[] GetGroupParts(string GroupName)
        {
            DBServices dbs = new DBServices();
            return dbs.GetGroupParts(GroupName);
        }
        //מחזיר את הנתונים של הקבוצה הספציפית
        public Group GetSpecificGroup(string GroupName)
        {
            DBServices dbs = new DBServices();
            return dbs.GetSpecificGroup(GroupName);
        }



        ////מחזיר לנו את החלקים של הקבוצה שנבחרה כולל את המידע של הקבוצה 
        //public Part[] GetGroupParts(string GroupName)
        //{
        //    DBServices dbs = new DBServices();
        //    Part[] parts = dbs.GetGroupParts(GroupName);
        //    Group g = dbs.GetSpecificGroup(GroupName);
        //    var result = Add_Multiply(g, parts);
        //    return result;
        //}
        ////השימוש ב טאפל.. מאפשר להחזיר 2 פרמטרים
        //private static Tuple<Group, Part[]> Add_Multiply(Group g, Part[] parts)
        //{
        //    var tuple = new Tuple<Group, Part[]>(g, parts);
        //    return tuple;
        //}

        //מחזיר את כל הקבוצות אשר שייכוח לפרויקט ופריט מסויים
        public Group[] GetGroups(string projectNum, string itemNum)
        {
            DBServices dbs = new DBServices();
            return dbs.GetGroups(projectNum, itemNum);
        }

        public int InsertNewGroup()
        {
            DBServices dbs = new DBServices();
            int numAffected = dbs.InsertNewGroup(this);
            return numAffected;
        }
    }
}