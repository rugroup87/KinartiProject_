using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

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
        //Item [] ItemArr;
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

        public Project(float _projectNum, string _projectName, string _prodStartDate, Item item, string _projectStatus = "תרם התחיל")
        {
            //In this constractor we will have to add ==string itemname== that we dont have now in the excel file
            ProjectNum = _projectNum;
            ProjectName = _projectName;
            ProdStartDate = DateTime.Parse(_prodStartDate);
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

        //public void StatusChange(string projectStatus, float projectNum)
        //{
        //    DBServices dbs = new DBServices();
        //    dbs.StatusChange(projectStatus, projectNum);
        //}

        public void StatusChange(Objectdata obj)
        {
            DBServices dbs = new DBServices();
            dbs.StatusChange(obj.projectStatus, obj.projNumStatus);
        }

        public void StatusChangeSpace(string projectStatus, float projectNum, int indexSpace)
        {
            DBServices dbs = new DBServices();
            dbs.StatusChange(projectStatus.Insert(indexSpace," "), projectNum);
        }

        public int UpdateProject()
        {
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