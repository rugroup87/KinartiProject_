using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class BarCodeScanController : ApiController
    {
        //[RequireHttps]
        //[HttpPut]
        //[Route("api/ScanPart")]
        //public object ScanPart(string PartBarCode, string StationName)
        //{
        //    string CurrentDate = Convert.ToString(DateTime.Now);
        //    BarCode Bcode = new BarCode();
        //    return Bcode.ScanPart(PartBarCode, StationName, CurrentDate);
        //}

        //[RequireHttps]
        //[HttpPost]
        [RequireHttps]
        [Route("api/ScanPart")]
        public void ScanPart(string PartBarCode, string StationName)
        {
            string CurrentDate = Convert.ToString(DateTime.Now);
            BarCode Bcode = new BarCode();
            Bcode.ScanPart(PartBarCode, StationName, CurrentDate);
        }
    }

    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required for this call"
                };
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}
