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
    public class NewsController : Controller
    {
        private Context db = new Context();

        //設定一頁幾筆
        private const int DefaultPageSize = 2;

        // GET: News
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            //總資料筆數
            //ViewBag.Count = db.News.Count();

            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.News.OrderByDescending(p => p.InitDate).ThenByDescending(p=>p.Id).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
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
