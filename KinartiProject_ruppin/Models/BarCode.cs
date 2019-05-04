using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class BarCode
    {
        //public string BarCodeNumber { get; set; }

        public BarCode()
        {

        }



        //int PartTimeAvg, string PartBarCode, string NextGroupStationName
        private void ExecuteInForeground(object o)
        {
            TempObj data = (TempObj)o;
            Thread.Sleep(data.PartTimeAvg * 60000);
            DBServices dbs = new DBServices();
            dbs.UpdateStatusWaitingForMachine(data.PartBarCode, data.NextGroupStationName);
        }



        public String ScanPart(string PartBarCode, string StationName, string CurrentDate)
        {
            //השדה של הסטיישן ניים שאני מקבל הוא לצורך ולידציה שנעשה בהמשך שהוא סרק את החלק במקום הנכון

            // במקום לעשות כל כך הרבה פונקציות.... 
            //  בהמשך אפשר אולי יהיה אפשר לרכז את זה בשאילתה 1 או 2 ולשים את זה לתוך משתנים של הקלאס הזה של סריקה
            DBServices dbs = new DBServices();
            string CurrentGroupPositionNo = null;
            string NextGroupStationName = null;
            int temp = 1;
            int CategoryTime = 0;
            int PartTimeAvg = 0;
            

            Thread cnc = new Thread(ExecuteInForeground);
            Thread color1 = new Thread(ExecuteInForeground);
            Thread color2 = new Thread(ExecuteInForeground);
            Thread Adbaka = new Thread(ExecuteInForeground);
            Thread Hituh = new Thread(ExecuteInForeground);
            Thread shiuf = new Thread(ExecuteInForeground);
            Thread Nisur = new Thread(ExecuteInForeground);

            string ScannedPartStatus = dbs.GetScannedPartStatus(PartBarCode);
            string ClickedStationNo = dbs.GetClickedStationNo(StationName);
            string GroupName = dbs.GetGroupName(PartBarCode);





            if (String.IsNullOrEmpty(GroupName)) 
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
            NextGroupStationName = dbs.GetNextGroupStationName(temp.ToString(), PartBarCode);

            string CategoryType = dbs.GetCategoryType(StationName);

            //ולידציה האם החלק באמת נסרק בתחנה הנכונה או שזה החלק הראשון שנסרק בתחנה הבאה אחריו
            if (ClickedStationNo == CurrentGroupStationNo && partCount > ScannedPartCount && ScannedPartStatus != StationName)
            {
                // מביא את הזמן בדקות בין הסריקה האחרונה בקטגוריה לסריקה הנוכחית
                CategoryTime = GetNewCategoryTime(PartBarCode, StationName, CurrentDate, GroupName, CategoryType);
                //סריקה של חלק רגיל באמצע תהליך במכונה מסויימת
                dbs.ScanPart(PartBarCode, StationName, ++ScannedPartCount, CurrentDate, CategoryTime, CategoryType);

                if (partCount == (ScannedPartCount + 1))
                {
                    // השאילתה מביאה לנו את החציון של חלקים במכונה
                    PartTimeAvg = dbs.GetMedianTimeForPart(PartBarCode, GroupName, CurrentDate);

                    // פותחים פרוסס חדש ומחכים את הזמן החציוני שלוקח לחלק לעבוד במכונה,
                    // ושם בסוף הזמן נעדכן את הסטטוסים והזמנים וכל מה שצריך
                    TempObj tempObj = new TempObj(PartTimeAvg, PartBarCode, NextGroupStationName);
                    switch (StationName)
                    {
                        case "CNC":
                            cnc.Start(tempObj);
                            break;
                        case "צבע 1":
                            color1.Start(tempObj);
                            break;
                        case "צבע 2":
                            color2.Start(tempObj);
                            break;
                        case "הדבקה":
                            Adbaka.Start(tempObj);
                            break;
                        case "חיתוך":
                            Hituh.Start(tempObj);
                            break;
                        case "שיוף":
                            shiuf.Start(tempObj);
                            break;
                        case "ניסור":
                            Nisur.Start(tempObj);
                            break;
                        default:
                            break;
                    } 
                }
            }
            else
            {
                ////////////////////////////////////////////////////////אני פה!!!!!!//////////////////////////////////////////////////////////////
                if (ClickedStationNo == NextGroupStationNo && partCount == ScannedPartCount)
                {
                    //צריך לבדוק שהחלק הנסרק עכשיו במכונה חדשה שייך לאותו קטגוריה אם כן ממשיכים לקדם את הזמן העכשוי...
                    // אחרת אנחנו צריכים להכניס 0 לקטגוריה החדשה כי בדיוק עכשיו התחילה עבודה, וכשיסרק החלק הבא זה יתעדכן
                    // לפה נכנס גם החישוב של הממוצע ומכניס אותולחלק ה-10 שנכנס כי אין סריה בסוף ה-10.

                    // כאשר החלק האחרון נסרק בקבוצה יש זמן ממוצע של כל החלקים ומבקלים זמן עבודה ממוצע לחלק
                    // אז כאשר החלק האחרון נסרק, נפעיל שעון שיחכה את הזמן הדרוש לחלק ובתום הזמן הזה - נצטרך לעדכן את
                    // את הסטטוס של כל החלקים והקבוצה לממינים למכונה הבאה
                    //חוץ מיזה נעדכן את הזמן של הקטגוריה לזמן הזה
                    CategoryTime = GetNewCategoryTime(PartBarCode, StationName, CurrentDate, GroupName, CategoryType);


                    switch (StationName)
                    {
                        case "CNC":
                            if (cnc.IsAlive)
                            {
                                cnc.Abort();
                            }
                            break;
                        case "צבע 1":
                            if (color1.IsAlive)
                            {
                                color1.Abort();
                            }
                            break;
                        case "צבע 2":
                            if (color2.IsAlive)
                            {
                                color2.Abort();
                            }
                            break;
                        case "הדבקה":
                            if (Adbaka.IsAlive)
                            {
                                Adbaka.Abort();
                            }
                            break;
                        case "חיתוך":
                            if (Hituh.IsAlive)
                            {
                                Hituh.Abort();
                            }
                            break;
                        case "שיוף":
                            if (shiuf.IsAlive)
                            {
                                shiuf.Abort();
                            }
                            break;
                        case "ניסור":
                            if (Nisur.IsAlive)
                            {
                                Nisur.Abort();
                            }
                            break;
                        default:
                            break;
                    }
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
            // מביא את הזמן של החלק שנסרק אחרון בקבוצה הזאת שהוא נמצא בא
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

    class TempObj
    {
        public int PartTimeAvg { get; set; }
        public string PartBarCode { get; set; }
        public string NextGroupStationName { get; set; }

        public TempObj(int PartTimeAvg, string PartBarCode, string NextGroupStationName)
        {
            this.PartTimeAvg = PartTimeAvg;
            this.PartBarCode = PartBarCode;
            this.NextGroupStationName = NextGroupStationName;
        }
    }
}