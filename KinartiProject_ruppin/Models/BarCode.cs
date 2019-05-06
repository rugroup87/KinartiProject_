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
            dbs.UpdateStatusWaitingForMachine(data.PartBarCode, data.NextGroupStationName, data.GroupName, data.CategoryType, data.CategoryTime);
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

                if (partCount == ScannedPartCount)
                {
                    // השאילתה מביאה לנו את החציון של חלקים במכונה
                    PartTimeAvg = dbs.GetMedianTimeForPart(PartBarCode, GroupName, CurrentDate);

                    // פותחים פרוסס חדש ומחכים את הזמן החציוני שלוקח לחלק לעבוד במכונה,
                    // ושם בסוף הזמן נעדכן את הסטטוסים והזמנים וכל מה שצריך
                    TempObj tempObj = new TempObj(PartTimeAvg, PartBarCode, NextGroupStationName, GroupName, CategoryType, CategoryTime);
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
                    bool FinishThread = false;
                    CategoryTime = GetNewCategoryTime(PartBarCode, StationName, CurrentDate, GroupName, CategoryType);

                    switch (StationName)
                    {
                        case "CNC":
                            if (cnc.IsAlive)
                            {
                                cnc.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        case "צבע 1":
                            if (color1.IsAlive)
                            {
                                color1.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        case "צבע 2":
                            if (color2.IsAlive)
                            {
                                color2.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        case "הדבקה":
                            if (Adbaka.IsAlive)
                            {
                                Adbaka.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        case "חיתוך":
                            if (Hituh.IsAlive)
                            {
                                Hituh.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        case "שיוף":
                            if (shiuf.IsAlive)
                            {
                                shiuf.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        case "ניסור":
                            if (Nisur.IsAlive)
                            {
                                Nisur.Abort();
                            }
                            else
                            {
                                FinishThread = true;
                            }
                            break;
                        default:
                            break;
                    }

                    if (!FinishThread)
                    {
                        dbs.PartScannedInNewStation(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);
                        // שים לב בבניה של הפונקציה הזאת לוודא שהוא מכניס נכון איפה שהעברית שם כי המילה סי-אן-סי בדיבי ושם נראים קצת שונה
                    }
                    else
                    {
                        dbs.PartScannedInNewStationAfterThread(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);
                        //לאפס את הזמני סריקות של כל החלקים
                    }
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
            int CategoryTotalTime = 0;
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
            //מחזיר את הזמן העדכני של הקטגוריה
            CategoryTotalTime = PartScannedGap + dbs.GetLatScanCategoryDate(PartBarCode, StationName, CurrentDate, CategoryType);
            return CategoryTotalTime;
        }
    }

    class TempObj
    {
        public int PartTimeAvg { get; set; }
        public string PartBarCode { get; set; }
        public string NextGroupStationName { get; set; }
        public string GroupName { get; set; }
        public string CategoryType { get; set; }
        public int CategoryTime { get; set; }

        public TempObj(int PartTimeAvg, string PartBarCode, string NextGroupStationName, string GroupName, string CategoryType, int CategoryTime)
        {
            this.PartTimeAvg = PartTimeAvg;
            this.PartBarCode = PartBarCode;
            this.NextGroupStationName = NextGroupStationName;
            this.GroupName = GroupName;
            this.CategoryType = CategoryType;
            this.CategoryTime = CategoryTime;
        }
    }
}