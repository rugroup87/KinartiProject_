﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KinartiProject_ruppin.Models;

namespace KinartiProject_ruppin.Controllers
{
    public class PartController : ApiController
    {
        //[HttpGet]
        //[Route("api/part")]
        //public IEnumerable<Part> Get()
        //{
        //    Part p = new Part();
        //    List<Part> partList = new List<Part>();
        //    partList = p.GetAllPart();
        //    return partList;
        //}

        //[HttpGet]
        [RequireHttps]
        [Route("api/GetPartFromItem")]
        public IEnumerable<Part> Get(float projectNum, string itemNum)
        {
            Part p = new Part();
            Part[] partList;
            partList = p.GetPartFromItem(projectNum, itemNum);
            return partList;
        }


        //[HttpPut]
        [RequireHttps]
        [Route("api/part")]
        public void Put(Objectdata oData)
        {
            Part p = new Part();
            p.StatusChange(oData);
        }

    }
}
