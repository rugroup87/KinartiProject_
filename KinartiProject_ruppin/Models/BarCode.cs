using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinartiProject_ruppin.Models
{
    public class BarCode
    {
        public string BarCodeNumber { get; set; }

        public BarCode()
        {

        }

        public string ScanPart(string PartBarCode, string StationName)
        {
            DBServices dbs = new DBServices();
            return dbs.ScanPart(PartBarCode, StationName);
        }
    }
}