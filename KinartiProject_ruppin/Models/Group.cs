using System;
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


        public Group(float _projectNum, string _itemNum, string _groupName,string _groupRouteName,
           int _groupPartCount, int _estPrepTime, int _estColorTime,string[] _arrPart, string _groupStatus="קבוצה מוכנה לעבודה")
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

        public Part[] GetGroupParts(string GroupName)
        {
            DBServices dbs = new DBServices();
            return dbs.GetGroupParts(GroupName);
        }

        public int InsertNewGroup()
        {
            DBServices dbs = new DBServices();
            int numAffected = dbs.InsertNewGroup(this);
            return numAffected;
        }
    }
}