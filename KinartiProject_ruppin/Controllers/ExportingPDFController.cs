using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class ExportingPDFController : ApiController
    {
        [HttpPost]
        [Route("api/ExportingPDF")]
        public void Post()
        {
            ItextToPDF doc = new ItextToPDF();
        }
    }
}
