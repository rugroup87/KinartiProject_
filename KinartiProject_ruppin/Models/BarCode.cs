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

        public string ScanPart(string PartBarCode, string StationName, string CurrentDate)
        {
            //השדה של הסטיישן ניים שאני מקבל הוא לצורך ולידציה שנעשה בהמשך שהוא סרק את החלק במקום הנכון

            // במקום לעשות כל כך הרבה פונקציות.... 
            //  בהמשך אפשר אולי יהיה אפשר לרכז את זה בשאילתה 1 או 2 ולשים את זה לתוך משתנים של הקלאס הזה של סריקה
            DBServices dbs = new DBServices();

            string ClickedStationNo = dbs.GetClickedStationNo(StationName);
            string CurrentGroupStationNo = dbs.GetCurrentGroupStationNo(PartBarCode);
            //string CurrentGroupStationName = dbs.GetCurrentGroupStationName(PartBarCode);
            string CurrentGroupPositionNo = dbs.GetCurrentGroupPositionNo(PartBarCode, CurrentGroupStationNo);

            int temp = Int32.Parse(CurrentGroupPositionNo) + 1;           
            int TotalStationCount = dbs.GetTotalStationCount(PartBarCode);
            int partCount = dbs.GetpartCount(PartBarCode);
            int ScannedPartCount = dbs.GetScannedPartCount(PartBarCode);

            //הפונקציה הזאת לא יכולה להיות פה - רק במקרה שהכן יש מסלול הבא... צריך לבדוק
            string NextGroupStationNo = dbs.GetNextGroupStationNo(temp.ToString(), PartBarCode);

            //הולידציה האם החלק באמת נסרק בתחנה הנכונה או שזה החלק הראשון שנסרק בתחנה הבאה אחריו
            if (ClickedStationNo == CurrentGroupStationNo && partCount > ScannedPartCount)
            {
                if (CurrentGroupStationNo == null)
                {
                    //סריקה של חלק ראשון בקבוצה מסויימת שעדיין לא התחילה ייצור
                    dbs.FirstScanPartOfGroup(PartBarCode, StationName, CurrentDate, CurrentGroupStationNo, ++ScannedPartCount);
                }
                else
                {
                    //סריקה של חלק רגיל באמצע תהליך במכונה מסויימת
                    dbs.ScanPart(PartBarCode, StationName, ++ScannedPartCount);
                }

            }
            else
            {
                ////////////////////////////////////////////////////////אני פה!!!!!!//////////////////////////////////////////////////////////////
                if (ClickedStationNo == NextGroupStationNo && partCount == ScannedPartCount)
                {
                    // כשנסרק חלק ראשון בתחנה חדשה לאחר ששאר החלקים כבר סיימו בתחנה הקודמת
                    //צריך להעביר סטטוס קבוצה לתחנה החדשה, סטטוס חלק, שנסרק ושאר החלקים בנתיים בסטטוס ממתין לתחנה זו החדשה
                }
                else
                {

                }
            }

                ////בודק האם אני נמצא בתחנה הסופית של המסלול וגם כל החלקים סרוקים
                //if (Int32.Parse(CurrentGroupPositionNo) == TotalStationCount && partCount == ScannedPartCount)
                //{
                //    // במקרה כזה זה אומר שחלק כלשהו נוסף מנסים לסרוק פעם נוספת צריך לזרוק שגיאה
                //    //אם אני בתחנה הסופית גם פה צריך לבדוק כמה חלקים נסרקו כדי לדעת מה לעשות
                //}
                //else
                //{

                //}

            
            return dbs.ScanPart(PartBarCode, StationName, CurrentDate);
        }
    }
}