using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class ProjectController : ApiController
    {

        [HttpGet]
        [Route("api/project")]
        public IEnumerable<Project> Get()
        {
            Project p = new Project();
            List<Project> projectList = new List<Project>();
            projectList = p.GetAllProject();
            return projectList;
        }

        [HttpPut]
        [Route("api/project")]
        public void Put(string projectStatus, float projectNum)
        {
            Project p = new Project();
            p.StatusChange(projectStatus, projectNum);
        }
        [HttpPut]
        [Route("api/project")]
        public void Put(string projectStatus, float projectNum, int indexSpace)
        {
            Project p = new Project();
            p.StatusChangeSpace(projectStatus, projectNum, indexSpace);
        }
        

    }

}
