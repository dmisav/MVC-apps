﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontPagePackages.Controllers
{
    public class PackagesController : Controller
    {
        //
        // GET: /Packages/

        public ActionResult Index()
        {
            return View();
        }

    }
}
