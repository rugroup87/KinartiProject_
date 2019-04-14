using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;

namespace KinartiProject_ruppin.Models
{
    public class Project
    {
        public float ProjectNum { get; set; }
        public string ProjectName { get; set; }

        //private string prodstartdate;
        //public string ProdStartDate
        //{
        //    get
        //    {
        //        return prodstartdate;
        //    }
        //    set => prodstartdate = value;
        //}
        //Item[] ItemArr;

        private Nullable<DateTime> prodstartdate;
        public Nullable<DateTime> ProdStartDate
        {
            get
            {
                if (prodstartdate == null)
                {
                    DateTime? prodstartdate = null;
                    return prodstartdate;
                }
                else
                {
                    return prodstartdate;
                }
            }
            set
            {
                prodstartdate = value;
            }
        }
        public string ProdStartDateString { get; set; }

        private Nullable<DateTime> supplydate;
        public Nullable<DateTime> SupplyDate
        {
            get
            {
                if (supplydate == null)
                {
                    DateTime? supplydate = null;
                    return supplydate;
                }
                else
                {
                    return supplydate;
                }
            }
            set
            {
                supplydate = value;
            }
        }
        public string SupplyDateString { get; set; }

        public string ProjectStatus { get; set; }
        public string Comment { get; set; }
        private Nullable<DateTime> prodrentrancedate;
        public Nullable<DateTime> ProdEntranceDate
        {
            get
            {
                if (prodrentrancedate == null)
                {
                    DateTime? prodrentrancedate = null;
                    return prodrentrancedate;
                }
                else
                {
                    return prodrentrancedate;
                }
            }
            set
            {
                //string tempDate = value.Value.ToShortDateString();
                //prodstartdate = DateTime.ParseExact(tempDate, "dd/MM/yyyy", null);
                prodrentrancedate = value;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ExcelFile ProjFile = new ExcelFile();
        public Item Item = new Item();

        public Project(float _projectNum, string _projectName, string _ProdEntranceDate, Item item, string _projectStatus = "טרם התחיל")
        {
            //In this constractor we will have to add ==string itemname== that we dont have now in the excel file
            ProjectNum = _projectNum;
            ProjectName = _projectName;
            ProdEntranceDate = DateTime.Parse(_ProdEntranceDate);
            Item = item;
            ProjectStatus = _projectStatus;
            AddNewDataToDB();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Project(float _projectNum, string _projectName, DateTime _prodStartDate, DateTime _supplyDate, string _projectStatus, string _comment, DateTime _prodEntranceDate)
        {
            this.ProjectNum = _projectNum;
            this.ProjectName = _projectName;
            this.ProdStartDate = _prodStartDate;
            this.SupplyDate = _supplyDate;
            this.ProjectStatus = _projectStatus;
            this.Comment = _comment;
            this.ProdEntranceDate = _prodEntranceDate;
            //ItemArr = itemarr;
        }

        //public Project(float _projectNum, string _prodStartDate, string _supplyDate, string _comment)
        //{
        //    ProjectNum = _projectNum;
        //    ProdStartDate = DateTime.Parse(_prodStartDate);
        //    SupplyDate = DateTime.Parse(_supplyDate);
        //    Comment = _comment;
        //}

        public Project(float _projectNum, string _prodStartDateString, string _supplyDateString, string _comment)
        {
            ProjectNum = _projectNum;
            ProdStartDateString = _prodStartDateString;
            SupplyDateString = _supplyDateString;
            Comment = _comment;
        }

        public Project()
        {
        }

        public List<Project> GetAllProject()
        {
            DBServices dbs = new DBServices();
            List<Project> lp = new List<Project>();
            lp = dbs.GetAllProject();
            return lp;
        }

        public void StatusChange(Objectdata obj)
        {
            DBServices dbs = new DBServices();
            dbs.StatusChange(obj.newStatus, obj.projNumStatus);
        }

        public int UpdateProject()
        {
            //srting a = this.prodstartdate.ToString("yyyy-MM-dd HH:mm:ss");
            //srting abc = DateTime.ParseExact(this.prodstartdate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //abc.ToString("yyyy-MM-dd HH:mm:ss");
            //(this.prodstartdate).Value.ToString("yyyy-MM-dd");
            //(this.supplydate).Value.ToString("yyyy-MM-dd");
            DBServices dbs = new DBServices();
            return dbs.UpdateProject(this);
        }

        

        /// //////////////////////////////////////////////////////////////////////////////////
        public void AddNewDataToDB()
        {
            DBServices dbs = new DBServices();
            dbs.AddNewDataToDB(this);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


}