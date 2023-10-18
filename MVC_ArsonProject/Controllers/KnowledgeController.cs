using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Models;
using MvcPaging;

namespace MVC_ArsonProject.Controllers
{
    public class KnowledgeController : Controller
    {
        private Context db = new Context();

        //設定一頁幾筆
        private const int DefaultPageSize = 3;

        // GET: Knowledge
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.Knowledges.Count();

            return View(db.Knowledges.OrderByDescending(x=>x.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
