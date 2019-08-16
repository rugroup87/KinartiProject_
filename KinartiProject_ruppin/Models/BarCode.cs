using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text;

namespace KinartiProject_ruppin.Models
{
    public class BarCode
    {
        //public string BarCodeNumber { get; set; }
        //bool threadIsSleeping = false;
        Thread cnc = new Thread(ExecuteInForeground);
        Thread color1 = new Thread(ExecuteInForeground);
        Thread color2 = new Thread(ExecuteInForeground);
        Thread Adbaka = new Thread(ExecuteInForeground);
        Thread Hituh = new Thread(ExecuteInForeground);
        Thread shiuf = new Thread(ExecuteInForeground);
        Thread Nisur = new Thread(ExecuteInForeground);
        private static bool cnc_ThreadIsSleeping = false;
        private static bool color1_ThreadIsSleeping = false;
        private static bool color2_ThreadIsSleeping = false;
        private static bool Adbaka_ThreadIsSleeping = false;
        private static bool Hituh_ThreadIsSleeping = false;
        private static bool shiuf_ThreadIsSleeping = false;
        private static bool Nisur_ThreadIsSleeping = false;
        private static bool LastPartFinish = false;

        public BarCode()
        {

        }



        //int PartTimeAvg, string PartBarCode, string NextGroupStationName
        private static void ExecuteInForeground(object o)
        {
            //TempObj data = (TempObj)o;
            //Thread.Sleep(data.PartTimeAvg * 60000);
            //DBServices dbs = new DBServices();
            //return "Thread Activated";
            
            if (!LastPartFinish)
            {
            }
            //    dbs.UpdateStatusWaitingForMachine(data.PartBarCode, data.NextGroupStationName, data.GroupName, data.CategoryType, data.CategoryTime, data.PartTimeAvg);
            //    switch (data.StationName)
            //    {
            //        case "CNC":
            //            cnc_ThreadIsSleeping = false;
            //            break;
            //        case "צבע 1":
            //            color1_ThreadIsSleeping = false;
            //            break;
            //        case "צבע 2":
            //            color2_ThreadIsSleeping = false;
            //            break;
            //        case "הדבקה":
            //            Adbaka_ThreadIsSleeping = false;
            //            break;
            //        case "חיתוך":
            //            Hituh_ThreadIsSleeping = false;
            //            break;
            //        case "שיוף":
            //            shiuf_ThreadIsSleeping = false;
            //            break;
            //        case "ניסור":
            //            Nisur_ThreadIsSleeping = false;
            //            break;
            //        default:
            //            break;
            //    }
            
        }



        public object ScanPart(string PartBarCode, string StationName, string CurrentDate)
        {
            //השדה של הסטיישן ניים שאני מקבל הוא לצורך ולידציה שנעשה בהמשך שהוא סרק את החלק במקום הנכון

            // במקום לעשות כל כך הרבה פונקציות.... 
            //  בהמשך אפשר אולי יהיה אפשר לרכז את זה בשאילתה 1 או 2 ולשים את זה לתוך משתנים של הקלאס הזה של סריקה
            DBServices dbs = new DBServices();
            string CurrentGroupPositionNo = null;
            string NextGroupStationNo = null;
            string NextGroupStationName = null;
            string PrevGroupStationName = null;
            int Nexttemp = 1;
            int Prevtemp = 0;
            int CategoryTime = 0;
            int PartTimeAvg = 0;


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
                Nexttemp = Int32.Parse(CurrentGroupPositionNo) + 1;

                if (Convert.ToInt32(CurrentGroupPositionNo) > 1)
                {
                    Prevtemp = Int32.Parse(CurrentGroupPositionNo) - 1;
                    PrevGroupStationName = dbs.GetNextGroupStationName(CurrentGroupPositionNo, PartBarCode);
                }
            }
            else
            {
                //צריך לראות מה עושים אם הוא המיקום הנוכחי הוא ריק או לא קיים
            }

            int TotalStationCount = dbs.GetTotalStationCount(PartBarCode);
            int partCount = dbs.GetpartCount(PartBarCode);
            int ScannedPartCount = dbs.GetScannedPartCount(PartBarCode);

            //הפונקציה הזאת לא יכולה להיות פה - רק במקרה שהכן יש מסלול הבא... צריך לבדוק
            NextGroupStationNo = dbs.GetNextGroupStationNo(Nexttemp.ToString(), PartBarCode);
            NextGroupStationName = dbs.GetNextGroupStationName(Nexttemp.ToString(), PartBarCode);



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

                    TempObj tempObj = new TempObj(PartTimeAvg, PartBarCode, NextGroupStationName, GroupName, CategoryType, CategoryTime, StationName);

                    //switch (StationName)
                    //{
                    //    case "CNC":
                    //        cnc_ThreadIsSleeping = true;
                    //        cnc.Start(tempObj);
                    //        break;
                    //    case "צבע 1":
                    //        color1_ThreadIsSleeping = true;
                    //        color1.Start(tempObj);
                    //        break;
                    //    case "צבע 2":
                    //        color2_ThreadIsSleeping = true;
                    //        color2.Start(tempObj);
                    //        break;
                    //    case "הדבקה":
                    //        Adbaka_ThreadIsSleeping = true;
                    //        Adbaka.Start(tempObj);
                    //        break;
                    //    case "חיתוך":
                    //        Hituh_ThreadIsSleeping = true;
                    //        Hituh.Start(tempObj);
                    //        break;
                    //    case "שיוף":
                    //        shiuf_ThreadIsSleeping = true;
                    //        shiuf.Start(tempObj);
                    //        break;
                    //    case "ניסור":
                    //        Nisur_ThreadIsSleeping = true;
                    //        Nisur.Start(tempObj);
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
            else
            {
                ///////////////////// את החלק הזה יש אפשרות להוריד...משום שכבר יש 2 סריקות לחלק אחרון!!!!!!//////////////////////////////////////////////////////////////
                // חלק חדש שנסרק בתחנה חדשה
                string temp;
                //temp.("ממתין ל- {0} בעגלה",NextGroupStationName);
                if (ClickedStationNo == NextGroupStationNo && partCount == ScannedPartCount /*&& ScannedPartStatus == temp*/)
                {
                    //bool FinishThread = false;
                    CategoryTime = GetNewCategoryTime(PartBarCode, StationName, CurrentDate, GroupName, CategoryType);

                    //switch (PrevGroupStationName)
                    //{
                    //    case "CNC":
                    //        if (cnc_ThreadIsSleeping)
                    //        {
                    //            cnc.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    case "צבע 1":
                    //        if (color1_ThreadIsSleeping)
                    //        {
                    //            color1.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    case "צבע 2":
                    //        if (color2_ThreadIsSleeping)
                    //        {
                    //            color2.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    case "הדבקה":
                    //        if (Adbaka_ThreadIsSleeping)
                    //        {
                    //            Adbaka.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    case "חיתוך":
                    //        if (Hituh_ThreadIsSleeping)
                    //        {
                    //            Hituh.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    case "שיוף":
                    //        if (shiuf_ThreadIsSleeping)
                    //        {
                    //            shiuf.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    case "ניסור":
                    //        if (Nisur_ThreadIsSleeping)
                    //        {
                    //            Nisur.Abort();
                    //        }
                    //        else
                    //        {
                    //            FinishThread = true;
                    //        }
                    //        break;
                    //    default:
                    //        break;
                    //}

                    //if (!FinishThread)
                    //{
                        dbs.PartScannedInNewStation(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);
                        // שים לב בבניה של הפונקציה הזאת לוודא שהוא מכניס נכון איפה שהעברית שם כי המילה סי-אן-סי בדיבי ושם נראים קצת שונה
                    //}
                    //else
                    //{
                        //dbs.PartScannedInNewStationAfterThread(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);
                        //לאפס את הזמני סריקות של כל החלקים
                    //}
                }
                else
                {
                    if (ClickedStationNo == NextGroupStationNo && String.IsNullOrEmpty(CurrentGroupStationNo))
                    {
                        //סריקה של חלק ראשון בקבוצה מסויימת שעדיין לא התחילה ייצור
                        dbs.FirstScanPartOfGroup(PartBarCode, StationName, CurrentDate, NextGroupStationNo, ++ScannedPartCount);
                    }
                    if (ClickedStationNo == CurrentGroupStationNo && partCount == ScannedPartCount)
                    {
                        LastPartFinish = true;
                        switch (StationName)
                        {
                            case "CNC":
                                cnc.Abort();
                                break;
                            case "צבע 1":
                                color1.Abort();
                                break;
                            case "צבע 2":
                                color2.Abort();
                                break;
                            case "הדבקה":
                                Adbaka.Abort();
                                break;
                            case "חיתוך":
                                Hituh.Abort();
                                break;
                            case "שיוף":
                                shiuf.Abort();
                                break;
                            case "ניסור":
                                Nisur.Abort();
                                break;
                            default:
                                break;
                        }
                        dbs.UpdateStatusWaitingForMachine(PartBarCode, NextGroupStationName, GroupName, CategoryType, CategoryTime, PartTimeAvg);
                    }
                    else
                    {
                        throw new PartScannedInWrongStation("שגיאה!!! בוצע נסיון סריקה של חלק במכונה לא נכונה.");
                    }

                }
            }

            return new { LastPartFinish, PartTimeAvg };
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
                DateTime LatScanPartDate = DateTime.Parse(LastScanPartDate_str, new CultureInfo("en-US", true));
                DateTime curent = DateTime.Parse(CurrentDate, new CultureInfo("en-US", true));
                //DateTime LatScanPartDate = LastScanPartDate_str
                PartScannedGap = (int)Math.Round(DateTime.Parse(CurrentDate, new CultureInfo("en-US", true)).Subtract(LatScanPartDate).TotalMinutes);
            }
            else
            {
                PartScannedGap = 0;
            }
            // שים לב יש מצב שהסריקה האחרונה של הקטגוריה פה מקבלת גם 0 או NULL
            //מחזיר את הזמן העדכני של הקטגוריה
            CategoryTotalTime = PartScannedGap + dbs.GetLatScanCategoryDate(PartBarCode, StationName, CurrentDate, CategoryType, GroupName);
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
        public string StationName { get; set; }

        public TempObj(int PartTimeAvg, string PartBarCode, string NextGroupStationName, string GroupName, string CategoryType, int CategoryTime, string StationName)
        {
            this.PartTimeAvg = PartTimeAvg;
            this.PartBarCode = PartBarCode;
            this.NextGroupStationName = NextGroupStationName;
            this.GroupName = GroupName;
            this.CategoryType = CategoryType;
            this.CategoryTime = CategoryTime;
            this.StationName = StationName;
        }
    }
}