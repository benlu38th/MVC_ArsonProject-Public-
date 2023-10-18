using MVC_ArsonProject.Areas.BackEnd.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class CalendarMgmtController : Controller
    {
        

        // GET: BackEnd/Calendar
        public ActionResult Index()
        {
            return View();
        }
    }
}