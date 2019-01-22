using PST.Reader.BL;
using Redemption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PST.Reader.ASP.NET.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var pstPath = @"C:\Users\aidas\OneDrive\Documents\Outlook Files\asdf@asdf.com.pst";

            var service = new PstReadingService();
            service.ReadFile(pstPath);

            return View();
        }

        //[HttpPost]
        //public ActionResult Index(IFormFile file)
        //{

        //    RDOSession session = new RDOSession();


        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}