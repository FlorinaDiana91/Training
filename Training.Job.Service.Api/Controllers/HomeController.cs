﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Training.Job.BusinessService;

namespace Training.Job.Service.Api.Controllers
{
    public class HomeController : ApiController
    {
        public ActionResult Index()
        {
            JobService v = new JobService();
           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}