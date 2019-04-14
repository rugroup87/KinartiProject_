using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KinartiProject_ruppin.Models
{
    public class Route
    {       
        public int MachineNum { get; set; }
        public string MachineName { get; set; }
        public int Position { get; set; }
        public int[] StationArr { get; set; }
        public string RouteName { get; set; }

        public Route()
        {

        }

        public Route(int machineNum, string machineName, int position, int[] stationArr, string routeName)
        {
            MachineNum = machineNum;
            MachineName = machineName;
            Position = position;
            RouteName = routeName;
            StationArr = stationArr;
        }

        public List<Route> GetAllRoutes()
        {
            DBServices dbs = new DBServices();
            List<Route> rl = new List<Route>();
            rl = dbs.GetAllRoutes();
            return rl;      
        }

        public int InsertStation()
        {
            DBServices dbs = new DBServices();
            int numAffected = dbs.InsertStation(this);
            return numAffected;
        }

        //public int UpdateRoute()
        //{
        //    DBServices dbs = new DBServices();
        //    return dbs.UpdateRoute(this);
        //}

        public int UpdateOldRoute(string route_name)
        {         
            DBServices dbs = new DBServices();
            return dbs.UpdateOldRoute(route_name);
        }

        //--------------------------------add
        public List<Route> ReadRouteName(string routeName)
        {
            DBServices dbs = new DBServices();
            List<Route> rli = new List<Route>();
            rli = dbs.ReadRouteInfo("KinartiConnectionString", routeName);
            return rli;
        }

            public string RouterValidation(string routeName)
            {
                DBServices dbs = new DBServices();
                return dbs.RouteValidation(routeName);
            }
        }

    }
