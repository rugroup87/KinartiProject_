using KinartiProject_ruppin.Models;
using System.Collections.Generic;
using System.Web.Http;

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
        public IEnumerable<Route> GetRouteWithoutOld_()
        {
            Route r = new Route();
            List<Route> RouteList = new List<Route>();
            RouteList = r.GetRouteWithoutOld_();
            return RouteList;
        }        

        [HttpGet]
        [Route("api/Route")]
        public IEnumerable<Route> Get(string routeName)
        {
            Route ri = new Route();
            return ri.ReadRouteName(routeName);
        }


        [HttpGet]
        //[Route("api/RouteNameCheck")]
        public string RouteNameCheck(string routeName)
        {
            Route r = new Route();
            return r.RouterValidation(routeName);
        }

        // לדשבורד - מחזיר מיקום וכמה תחנות במסלול
        [HttpGet]
        [Route("api/GetGroupInRouteInformaion")]
        public object GetGroupInRouteInformaion(string projectNum, string itemNum, string routeName, string groupName)
        {
            Route R = new Route();
            object o = new object();
            o = R.GroupInRouteInformaion(projectNum, itemNum, routeName, groupName);
            return o;
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
