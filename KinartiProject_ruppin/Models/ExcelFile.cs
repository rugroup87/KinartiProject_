using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.CSharp.RuntimeBinder;

namespace KinartiProject_ruppin.Models
{
    public class ExcelFile
    {
        public string FileName { get; set; }
        public int MyProperty { get; set; }

        public ExcelFile()
        {

        }

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        //פוקנציה שמחזירה את האיידי של הפרוסס של הקובץ אקסל שאני עובד עליו
        Process GetExcelProcess(Excel.Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            return Process.GetProcessById(id);
        }

        public void WorkOnExcelFile(string filename, string fileuploaddate)
        {
            string path = @"C:\Users\alex.tochilovsky\source\repos\KinartiProject_ruppin\KinartiProject_ruppin\" + filename;
            List<Part> PartList = new List<Part>();
            string temp1 = "";
            List<string> temp = new List<string>();
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelBook = excelApp.Workbooks.Open(path);
            Excel._Worksheet excelSheet = excelBook.Sheets[1];
            Excel.Range excelRange = excelSheet.UsedRange;


            // Calculating rows and cols.
            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;

            //get the excel process ID - for future delete
            var ExcelIdProcess = GetExcelProcess(excelApp);
            try
            {
                //Reading step by step cols and rows.
                for (int i = 2; i <= rowCount; i++)
                {
                    Part part = new Part();

                    for (int j = 4; j <= colCount; j++)
                    {
                        //רץ על גודל הקובץ אקסל שיש נתונים
                        if (excelRange.Cells[i, j] != null)
                        {
                            //רץ על הערכים עצמם
                            if (excelRange.Cells[i, j].Value2 == null)
                            {
                                temp1 = "";
                            }
                            else
                            {
                                temp1 = excelRange.Cells[i, j].Value2.ToString();
                            }
                        }
                        //בודק באיזה עמודה הוא נמצא
                        switch (excelRange.Cells[1, j].Value2.ToString())
                        {
                            case "מספר_ארגז":
                                part.PartSetNumber = Convert.ToInt32(temp1);
                                break;
                            case "מספר_חלק":
                                part.PartNum = temp1;
                                break;
                            case "שם_חלק":
                                part.PartName = temp1;
                                break;
                            case "קנטים":
                                part.PartKantim = temp1;
                                break;
                            case "פעולת_מכונה_ראשונה":
                                part.PartFirstMachine = temp1;
                                break;
                            case "פעולת_מכונת_שניה":
                                part.PartSecondMachine = temp1;
                                break;
                            case "חומר":
                                part.PartMaterial = temp1;
                                break;
                            case "צבע_":
                                part.PartColor = temp1;
                                break;
                            case "אורך_חלק":
                                part.PartLength = Convert.ToInt32(temp1);
                                break;
                            case "רוחב_חלק":
                                part.PartWidth = Convert.ToInt32(temp1);
                                break;
                            case "עובי":
                                part.PartThickness = Convert.ToInt32(temp1);
                                break;
                            case "תוספת_לאורך":
                                part.AdditionToLength = Convert.ToInt32(temp1);
                                break;
                            case "תוספת_לרוחב":
                                part.AdditionToWidth = Convert.ToInt32(temp1);
                                break;
                            case "תוספת_לעובי":
                                part.AdditionToThickness = Convert.ToInt32(temp1);
                                break;
                            //case "תז_של_חלק":
                            //    part.PartID = temp1;
                            //    break;
                            case "בר_קוד_תז":
                                part.PartBarCode = temp1;
                                break;
                            case "מכונת_חיתוך":
                                part.PartCropType = temp1;
                                break;
                            case "קטגוריית_חלק":
                                part.PartCategory = temp1;
                                break;
                            case "הערות":
                                part.PartComment = temp1;
                                break;
                            case "כמות":
                                part.PartQuantity = Convert.ToInt32(temp1);
                                break;

                            default:
                                throw new MissingHeaderException();
                                //break;
                                // code block
                        }



                        part.PartStatus = "חלק טרם נסרק";
                        part.GroupName = "";
                    }
                    PartList.Add(part);
                }
            }
            catch (COMException e)
            {
                throw new COMException("adhtv", e.InnerException);
            }
            //כאשר חסרה עמודה(כותרת) בקובץ
            catch (MissingHeaderException e)
            {
                throw new MissingHeaderException("ייתכן כי חסרה עמודה בקובץ - אנא נסה שוב", e.InnerException);
            }
            //
            catch (RuntimeBinderException e)
            {
                throw new RuntimeBinderException("המערכת תומכת בקבצי אקסל עם הסיומת xlxs אנא נסה שנית", e.InnerException);
            }
            //אקספשן כאשר יש שדה עם טייפ לא נכון שמנסים להכניס
            catch (FormatException e)
            {
                throw new FormatException(" אחד הערכים בקובץ מוגדר בפורמט שאינו מתאים", e.InnerException);
            }
            catch (Exception e)
            {
                throw new Exception("שגיאת מערכת, צור קשר עם צוות התמיכה", e.InnerException);
            }

            finally
            {
                excelBook.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                KillSpecificExcelFileProcess(ExcelIdProcess.Id);
                File.Delete(path);
            }

            try
            {
                Item Item = new Item(excelRange.Cells[2, 3].Value2.ToString(), PartList);
                Project NewData = new Project(Convert.ToSingle(excelRange.Cells[2, 1].Value2), excelRange.Cells[2, 2].Value2, fileuploaddate, Item);
            }
            //e.TargetSite.MetadataToken == 100667808
            catch (COMException e)
            {
                throw new COMException("בעיית RPC - צור קשר עם צוות התמיכה.", e.InnerException);
            }
            //כאשר נכנס קובץ אקסל עם ערכים לא נכונים זה קופץ
            catch (System.Data.SqlClient.SqlException e)
            {
                throw (e);
            }

            //שגיאת מפתחות - הכנסה של נתונים קיימים
            catch (DuplicatePrimaryKeyException e)
            {

                throw new DuplicatePrimaryKeyException("פרוייקט או פריט זה כבר קיימים במערכת אנא בדוק שוב את הקובץ", e.InnerException);
            }

            //כאשר מנסים לעלות קובץ שהוא לא קובץ אקסל
            catch (RuntimeBinderException e)
            {

                throw new RuntimeBinderException("המערכת תומכת בקבצי אקסל עם הסיומת xlxs אנא נסה שנית", e.InnerException);
            }
            // כל שגיאה כללית אחרת
            catch (Exception e)
            {
                throw new Exception("שגיאת מערכת, צור קשר עם צוות התמיכה", e.InnerException);
            }

            finally
            {
                excelBook.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                KillSpecificExcelFileProcess(ExcelIdProcess.Id);
                File.Delete(path);
            }

            //after reading, relaase the excel project
            //KillSpecificExcelFileProcess(ExcelIdProcess.Id);
            //excelApp.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        //הורג\מסיים את הפרוסס של האקסל עליו עבדתי
        private void KillSpecificExcelFileProcess(int id)
        {
            var process = Process.GetProcessById(id);
            process.Kill();
        }
    }
}