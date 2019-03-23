﻿using System;
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
        public string ProdStartDate1 { get; set; }

        //private string supplydate;
        //public string SupplyDate
        //{
        //    get
        //    {
        //        return supplydate;
        //    }
        //    set
        //    {
        //        supplydate = value.Substring(0, 10);
        //    }
        //}
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
        public string SupplyDate1 { get; set; }

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
        Item NewItem = new Item();


        public Project(float _projectNum, string _projectName, DateTime _prodStartDate, string _projectStatus = "תרם התחיל")
        {
            ProjectNum = _projectNum;
            ProjectName = _projectName;
            ProdStartDate = _prodStartDate;
            ProjectStatus = _projectStatus;
            AddNewProjectToDB();
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

        //public Project(float _projectNum, string _prodStartDate1, string _comment)
        //{
        //    this.ProjectNum = _projectNum;
        //    this.ProdStartDate1 = _prodStartDate1;
        //    this.Comment = _comment;
        //}


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
            dbs.StatusChange(obj.projectStatus, obj.projNumStatus);
        }

        public int UpdateProject()
        {
            //srting a = this.prodstartdate.ToString("yyyy-MM-dd HH:mm:ss");
            DBServices dbs = new DBServices();
            return dbs.UpdateProject(this);
        }

        /// //////////////////////////////////////////////////////////////////////////////////
        public void AddNewProjectToDB()
        {
            DBServices dbs = new DBServices();
            dbs.AddNewProjectToDB(this);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


}