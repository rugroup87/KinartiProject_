using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class FileUploudController : ApiController
    {
        [HttpPost]
        [Route("api/FileUpload")]
        public HttpResponseMessage Post()
        {
            ExcelFile NewFile = new ExcelFile();
            //List<string> FilesLinks = new List<string>();
            string FilePath, UploadDate;
            FilePath = UploadDate = string.Empty;
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    // this is an example of how you can extract addional values from the Ajax call
                    UploadDate = httpContext.Request.Form["UploadDate"];

                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                        string fname = httpPostedFile.FileName.Split('\\').Last();
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), fname);
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);
                        //FilesLinks.Add("uploadedFiles/" + fname);
                        FilePath = "uploadedFiles/" + fname;
                    }
                }
                NewFile.WorkOnExcelFile(FilePath, UploadDate);
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, FilePath);
        }
    }
}
