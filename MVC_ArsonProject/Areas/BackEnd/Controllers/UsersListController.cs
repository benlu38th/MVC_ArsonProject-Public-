using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Areas.BackEnd.Filter;
using MVC_ArsonProject.Models;

namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class UsersListController : Controller
    {
        private Context db = new Context();

        // GET: BackEnd/UsersList
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
    }
}