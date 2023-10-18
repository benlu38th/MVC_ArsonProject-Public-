using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Models;

namespace MVC_ArsonProject.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();

        public ActionResult Index()
        {
            var result = db.News.OrderByDescending(n => n.InitDate).ThenByDescending(n=>n.Id).ToList();

            return View(result);
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