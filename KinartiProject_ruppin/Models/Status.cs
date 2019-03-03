using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class Status
    {

        public List<string> StatusList { get; set; }

        public Status(List<string> statusarr)
        {
            StatusList = statusarr;
        }

        public Status()
        {
        }

        public string[] StatusArr(string relateTo)
        {
            DBServices dbs = new DBServices();
            List<string> ls = new List<string>();
            ls = dbs.GetAllStatus(relateTo);
            string[] arrS = ls.ToArray();
            return arrS;
        }
    }
}