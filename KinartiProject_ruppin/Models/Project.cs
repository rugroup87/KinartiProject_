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
        //private Nullable<DateTime> prodstartdate;
        //public Nullable<DateTime> ProdStartDate
        //{
        //    get
        //    {
        //        if (prodstartdate == null)
        //        {
        //            DateTime? prodstartdate = null;
        //            return prodstartdate;
        //        }
        //        else
        //        {
        //            return prodstartdate;
        //        }
        //    }
        //    set
        //    {
        //        string tempDate = value.Value.ToShortDateString();
        //        prodstartdate = DateTime.ParseExact(tempDate, "dd/MM/yyyy", null);
        //    }
        //}
        private string prodstartdate;
        public string ProdStartDate
        {
            get
            {
                return prodstartdate;
            }
            set => prodstartdate = value;
        }
        //Item [] ItemArr;
        private string supplydate;
        public string SupplyDate
        {
            get
            {
                return supplydate;
            }
            set
            {
                supplydate = value.Substring(0, 10);
            }
        }
        public string ProjectStatus { get; set; }
        public string Comment { get; set; }
        //public DateTime ProdEntranceDate { get; set; }
        private string prodrntrancedate;
        public string ProdEntranceDate
        {
            get
            {
                return prodrntrancedate;
            }
            set => prodrntrancedate = value.Substring(0, 10);
        }

        public Project(float _projectNum, string _projectName, string _prodStartDate, string _supplyDate, string _projectStatus, string _comment, string _prodEntranceDate, Item[] itemarr)
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

        public void StatusChange(string projectStatus, float projectNum)
        {
            DBServices dbs = new DBServices();
            dbs.StatusChange(projectStatus, projectNum);
        }

        public void StatusChangeSpace(string projectStatus, float projectNum, int indexSpace)
        {
            DBServices dbs = new DBServices();
            dbs.StatusChange(projectStatus.Insert(indexSpace," "), projectNum);
        }



    }


}