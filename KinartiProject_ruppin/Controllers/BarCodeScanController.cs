using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class BarCodeScanController : ApiController
    {

        [HttpPut]
        [Route("api/ScanPart")]
        public String ScanPart(string PartBarCode, string StationName, string CurrentDate)
        {
            BarCode Bcode = new BarCode();
            return Bcode.ScanPart(PartBarCode, StationName, CurrentDate);
        }
    }
}
