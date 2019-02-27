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
        public DateTime ProdStartDate { get; set; }
        public DateTime SupplyDate { get; set; }
        public string ProjectStatus { get; set; }
        public string Comment { get; set; }
        public DateTime ProdEntranceDate { get; set; }

        public Project(float _projectNum, string _projectName, DateTime _prodStartDate, DateTime _supplyDate, string _projectStatus, string _comment, DateTime _prodEntranceDate)
        {
            this.ProjectNum = _projectNum;
            this.ProjectName = _projectName;
            this.ProdStartDate = _prodStartDate;
            this.SupplyDate = _supplyDate;
            this.ProjectStatus = _projectStatus;
            this.Comment = _comment;
            this.ProdEntranceDate = _prodEntranceDate;
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



    }


}