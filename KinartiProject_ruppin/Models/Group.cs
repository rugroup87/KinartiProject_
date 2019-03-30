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
        public string GroupPartCount { get; set; }
        public int EstPrepTime { get; set; }
        public int EstCarpTime { get; set; }
        public int EstColorTime { get; set; }

        public Group()
        {

        }

        public Part[] GetGroupParts(string GroupName)
        {
            DBServices dbs = new DBServices();
            return dbs.GetGroupParts(GroupName);
        }
    }
}