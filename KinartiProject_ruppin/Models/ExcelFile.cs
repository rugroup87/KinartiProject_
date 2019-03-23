using System;
using System.Collections.Generic;
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

        public void WorkOnExcelFile(string filename, string fileuploaddate)
        {
            List<Item> ItemList = new List<Item>();
            List<string> temp = new List<string>();
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelBook = excelApp.Workbooks.Open(@"C:\Users\alex.tochilovsky\source\repos\KinartiProject_ruppin\KinartiProject_ruppin\" + filename);
            Excel._Worksheet excelSheet = excelBook.Sheets[1];
            Excel.Range excelRange = excelSheet.UsedRange;
            

            // Calculating rows and cols.
            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;

            Project NewProject = new Project(excelRange.Cells[2, 1].Value2, excelRange.Cells[2, 2].Value2, DateTime.Parse(fileuploaddate));

            //Reading step by step cols and rows.
            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 4; j <= colCount; j++)
                {

                    //
                    if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                        temp.Add(excelRange.Cells[i, j].Value2.ToString());
                }
                Item item = new Item();
            }

            
            //after reading, relaase the excel project
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
    }
}