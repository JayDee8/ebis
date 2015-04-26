using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ebis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Pro používání systému se přihlašte.";

            return View();
        }

        [Authorize]
        public ActionResult Contacts()
        {
            return View();
        }

        [Authorize]
        public ActionResult Artists()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProdLists()
        {
            return View();
        }

        [Authorize]
        public ActionResult Locations()
        {
            return View();
        }

        [Authorize]
        public ActionResult Finances()
        {
            return View();
        }

        [Authorize]
        public ActionResult Instruments()
        {
            return View();
        }

       
    }
}
