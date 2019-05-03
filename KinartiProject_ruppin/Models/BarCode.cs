using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class BarCode
    {
        public string BarCodeNumber { get; set; }

        public BarCode()
        {

        }

        public String ScanPart(string PartBarCode, string StationName, string CurrentDate)
        {
            //השדה של הסטיישן ניים שאני מקבל הוא לצורך ולידציה שנעשה בהמשך שהוא סרק את החלק במקום הנכון

            // במקום לעשות כל כך הרבה פונקציות.... 
            //  בהמשך אפשר אולי יהיה אפשר לרכז את זה בשאילתה 1 או 2 ולשים את זה לתוך משתנים של הקלאס הזה של סריקה
            DBServices dbs = new DBServices();
            string CurrentGroupPositionNo = null;
            int temp = 1;
            int CategoryTime = 0;

            string ScannedPartStatus = dbs.GetScannedPartStatus(PartBarCode);
            string ClickedStationNo = dbs.GetClickedStationNo(StationName);


            

            if (String.IsNullOrEmpty(dbs.GetGroupName(PartBarCode))) 
            {
                throw new UndifinedGroupForThisPart("שגיאה!!! בוצע נסיון סריקה לחלק שאינו משוייך לקבוצה קיימת. ");
            }

            string CurrentGroupStationNo = dbs.GetCurrentGroupStationNo(PartBarCode);
            if (!String.IsNullOrEmpty(CurrentGroupStationNo))
            {
                CurrentGroupPositionNo = dbs.GetCurrentGroupPositionNo(PartBarCode, CurrentGroupStationNo);
                temp = Int32.Parse(CurrentGroupPositionNo) + 1;
            }

                       
            int TotalStationCount = dbs.GetTotalStationCount(PartBarCode);
            int partCount = dbs.GetpartCount(PartBarCode);
            int ScannedPartCount = dbs.GetScannedPartCount(PartBarCode);

            //הפונקציה הזאת לא יכולה להיות פה - רק במקרה שהכן יש מסלול הבא... צריך לבדוק
            string NextGroupStationNo = dbs.GetNextGroupStationNo(temp.ToString(), PartBarCode);
            string CategoryType = dbs.GetCategoryType(StationName);

            //ולידציה האם החלק באמת נסרק בתחנה הנכונה או שזה החלק הראשון שנסרק בתחנה הבאה אחריו
            if (ClickedStationNo == CurrentGroupStationNo && partCount > ScannedPartCount && ScannedPartStatus != StationName)
            {
                // מביא את הזמן בדקות בין הסריקה האחרונה בקטגוריה לסריקה הנוכחית
                CategoryTime = GetNewCategoryTime(PartBarCode, StationName, CurrentDate, dbs.GetGroupName(PartBarCode), CategoryType);
                //סריקה של חלק רגיל באמצע תהליך במכונה מסויימת
                dbs.ScanPart(PartBarCode, StationName, ++ScannedPartCount, CurrentDate, CategoryTime, CategoryType);
            }
            else
            {
                ////////////////////////////////////////////////////////אני פה!!!!!!//////////////////////////////////////////////////////////////
                if (ClickedStationNo == NextGroupStationNo && partCount == ScannedPartCount)
                {
                    //צריך לבדוק שהחלק הנסרק עכשיו במכונה חדשה שייך לאותו קטגוריה אם כן ממשיכים לקדם את הזמן העכשוי...
                    // אחרת אנחנו צריכים להכניס 0 לקטגוריה החדשה כי בדיוק עכשיו התחילה עבודה, וכשיסרק החלק הבא זה יתעדכן
                    // לפה נכנס גם החישוב של הממוצע ומכניס אותולחלק ה-10 שנכנס כי אין סריה בסוף ה-10.

                    CategoryTime = GetNewCategoryTime(PartBarCode, StationName, CurrentDate, dbs.GetGroupName(PartBarCode), CategoryType);

                    dbs.PartScannedInNewStation(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);
                    // שים לב בבניה של הפונקציה הזאת לוודא שהוא מכניס נכון איפה שהעברית שם כי המילה סי-אן-סי בדיבי ושם נראים קצת שונה
                }
                else
                {
                    if (ClickedStationNo == NextGroupStationNo && String.IsNullOrEmpty(CurrentGroupStationNo))
                    {
                        //סריקה של חלק ראשון בקבוצה מסויימת שעדיין לא התחילה ייצור
                        dbs.FirstScanPartOfGroup(PartBarCode, StationName, CurrentDate, NextGroupStationNo, ++ScannedPartCount);
                    }
                    else
                    {
                        throw new PartScannedInWrongStation("שגיאה!!! בוצע נסיון סריקה של חלק במכונה לא נכונה.");
                    }
                }
            }
            
            return "Scanned Successfuly";
        }

        public int GetNewCategoryTime(string PartBarCode, string StationName, string CurrentDate, string GroupName, string CategoryType)
        {
            DBServices dbs = new DBServices();
            int PartScannedGap;
            string LastScanPartDate_str = dbs.GetLatScanPartDate(PartBarCode, StationName, CurrentDate, GroupName);
            

            if (!String.IsNullOrEmpty(LastScanPartDate_str))
            {
                DateTime LatScanPartDate = DateTime.Parse(LastScanPartDate_str);
                PartScannedGap = (int)Math.Round(DateTime.Parse(CurrentDate).Subtract(LatScanPartDate).TotalMinutes);
            }
            else
            {
                PartScannedGap = 0;
            }
            // שים לב יש מצב שהסריקה האחרונה של הקטגוריה פה מקבלת גם 0 או NULL
            PartScannedGap += dbs.GetLatScanCategoryDate(PartBarCode, StationName, CurrentDate, CategoryType);
            return PartScannedGap;
        }
    }
}