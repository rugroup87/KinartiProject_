using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Group
    {
        public float ProjectNum { get; set; }
        public string ProjectName { get; set; }
        public string ItemNum { get; set; }
        public string ItemName { get; set; }
        public string GroupName { get; set; }
        public string GroupStatus { get; set; }
        public string GroupRouteName { get; set; }
        public string CurrentGroupStation { get; set; } // ברקוד של מספר!! מכונה
        public int GroupPartCount { get; set; }
        public int ScannedPartsCount { get; set; }
        public int EstPrepTime { get; set; }
        public int EstCarpTime { get; set; }
        public int EstColorTime { get; set; }
        public int CurrentPrepTime { get; set; }
        public int CurrentCarpTime { get; set; }
        public int CurrentColorTime { get; set; }
        public string[] ArrPart { get; set; }

        //הג'ייסון לא נכנס לי לבנאי הזה ולכן אין סטטוס כי הוא הולך לבנאי הריק - לבדוק למה
        public Group(float _projectNum,string _projectName, string _itemNum, string _itemName, string _groupName, string _groupRouteName,
           int _groupPartCount,int _scannedPartsCount, int _estPrepTime, int _estCarpTime, int _estColorTime,
           string[] _arrPart, string _CurrentGroupStation, string _groupStatus = "קבוצה מוכנה לעבודה")
        {
            ProjectNum = _projectNum;
            ProjectName = _projectName;
            ItemNum = _itemNum;
            ItemName = _itemName;
            GroupName = _groupName;
            GroupRouteName = _groupRouteName;
            GroupPartCount = _groupPartCount;
            ScannedPartsCount = _scannedPartsCount;
            EstPrepTime = _estPrepTime;
            EstCarpTime = _estCarpTime;
            EstColorTime = _estColorTime;
            GroupStatus = _groupStatus;
            ArrPart = _arrPart;
            CurrentGroupStation = _CurrentGroupStation;
        }

        //בנאי ריק
        public Group()
        {
        }

        //מחזיר לנו את החלקים של הקבוצה שנבחרה כולל את המידע של הקבוצה 
        public Part[] GetGroupParts(string GroupName, string projectNum, string itemNum)
        {
            DBServices dbs = new DBServices();
            return dbs.GetGroupParts(GroupName, projectNum, itemNum);
        }
        //מחזיר את הנתונים של הקבוצה הספציפית
        public Group GetSpecificGroup(string GroupName, string projectNum, string itemNum)
        {
            DBServices dbs = new DBServices();
            return dbs.GetSpecificGroup(GroupName, projectNum, itemNum);
        }

        public void UpdateGroupEstTime(string prepTime, string carpTime, string paintTime, string projectNum, string itemNum, string groupName)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateGroupEstTime(prepTime, carpTime, paintTime,projectNum, itemNum, groupName);
        }

        //מחזיר את כל הקבוצות אשר שייכוח לפרויקט ופריט מסויים
        public Group[] GetGroups(string projectNum, string itemNum)
        {
            DBServices dbs = new DBServices();
            return dbs.GetGroups(projectNum, itemNum);
        }

        //יצירת קבוצה חדשה
        public int InsertNewGroup()
        {
            DBServices dbs = new DBServices();
            int numAffected = dbs.InsertNewGroup(this);
            return numAffected;
        }

        //הוספת חלקים לקבוצה קיימת
        public int AddingPartToExistGroup(string groupName, string[] partNumToAddArr, string projectNum, string itemNum)
        {
            DBServices dbs = new DBServices();
            Group groupInfo = dbs.GetSpecificGroup(groupName, projectNum, itemNum);
            string groupPosition = dbs.CheckGroupPosition(groupInfo.GroupRouteName);
            int numAffected;
            //אם הקבוצה נמצאת בתחנה מתקדמת במסלול
            if (groupPosition == "inProgress")
            {
                numAffected = dbs.AccomplishGroup(groupInfo, partNumToAddArr);
                //במידה ויצרנו קבוצת השלמה נחזיר אמת
                return 1;
            }
            else
            {
                numAffected = dbs.AddingPartToExistGroup(groupInfo, partNumToAddArr);
                //במידה והוספנו לקבוצה קיימת נחזיר שקר
                return 0;
            }
        }

        public string DeletePartFromGroup(string partBarcode, int PartCount, string projectNum, string itemNum, string GroupName)
        {
            --PartCount;
            DBServices dbs = new DBServices();
            return dbs.DeletePartFromGroup(partBarcode, PartCount, projectNum, itemNum, GroupName);
        }
        //מחזירה את כל הקבוצות הקיימות בבסיס הנתונים
        public List<Group> GetAllGroupsFromAllProjects()
        {
            DBServices dbs = new DBServices();
            List<Group> gl = new List<Group>();
            gl = dbs.GetAllGroupsFromAllProjects();
            return gl;
        }

        public void DeleteGroup(string projNum, string itemNum, string groupName, string[] barcodes)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteGroup(projNum, itemNum, groupName, barcodes);
        }
    }

  
}