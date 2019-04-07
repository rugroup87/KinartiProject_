using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

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

        Process GetExcelProcess(Excel.Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            return Process.GetProcessById(id);
        }

        public void WorkOnExcelFile(string filename, string fileuploaddate)
        {
            List<Part> PartList = new List<Part>();
            string temp1 = "";
            List<string> temp = new List<string>();
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelBook = excelApp.Workbooks.Open(@"C:\Users\alex.tochilovsky\source\repos\KinartiProject_ruppin\KinartiProject_ruppin\" + filename);
            Excel._Worksheet excelSheet = excelBook.Sheets[1];
            Excel.Range excelRange = excelSheet.UsedRange;
            

            // Calculating rows and cols.
            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;

            //Reading step by step cols and rows.
            for (int i = 2; i <= rowCount; i++)
            {
                Part part = new Part();

                for (int j = 4; j <= colCount; j++)
                {
                    if (excelRange.Cells[i, j] != null)
                    {
                        if (excelRange.Cells[i, j].Value2 == null)
                        {
                            temp1 = "";
                        }
                        else
                        {
                            temp1 = excelRange.Cells[i, j].Value2.ToString();
                        }
                    }
                    // Inside the case we can change the Convert.ToInt32(excelRange.Cells[i, j].Value2); values to temp1.....
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
                            break;
                            // code block
                    }
                }
                part.PartStatus = "חלק טרם נסרק";
                part.GroupName = "";
                PartList.Add(part);
            }
            var ExcelIdProcess = GetExcelProcess(excelApp);
            Item Item = new Item(excelRange.Cells[2, 3].Value2.ToString(), PartList);
            try
            {
                Project NewData = new Project(Convert.ToSingle(excelRange.Cells[2, 1].Value2), excelRange.Cells[2, 2].Value2, fileuploaddate, Item);
            }
            catch (Exception e)
            {
                KillSpecificExcelFileProcess(ExcelIdProcess.Id);
                string excelFileName = filename.Replace("uploadedFiles/", "");
                File.Delete(excelBook.Path + excelFileName);
                throw (e);
            }
            
            //after reading, relaase the excel project
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private void KillSpecificExcelFileProcess(int id)
        {
            //excelFileName = excelFileName.Replace("uploadedFiles/", "");
            var process = Process.GetProcessById(id);
            process.Kill();
        }
    }
}