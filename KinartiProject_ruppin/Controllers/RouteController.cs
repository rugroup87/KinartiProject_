using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class RouteController : ApiController
    {
        [HttpGet]
        [Route("api/Route")]
        public IEnumerable<Route> Get()
        {
            Route r = new Route();
            List<Route> RouteList = new List<Route>();
            RouteList = r.GetAllRoutes();
            return RouteList;
        }

        [HttpGet]
        [Route("api/Route")]
        public IEnumerable<Route> Get(string routeName)
        {
            Route ri = new Route();
            return ri.ReadRouteName(routeName);
        }

        [HttpPost]
        [Route("api/Route")]
        public void Post([FromBody]Route r)
        {
            
            r.InsertStation();
        }

        [HttpPost]
        [Route("api/Route/UpdateRoute")]
        public void UpdateRoute([FromBody]Route r)
        {
            r.UpdateRoute();
        }

    }
}
