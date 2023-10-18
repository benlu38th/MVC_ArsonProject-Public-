using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Models;

namespace MVC_ArsonProject.Controllers
{
    public class ArticleController : Controller
    {
        private Context db = new Context();

        public ActionResult Declare()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "財產權宣告");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Calendar()
        {
            return View();
        }

        // GET: Article/Survey
        public ActionResult Survey()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "縱火調查");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Refer
        public ActionResult Refer()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "諮詢、顧問");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Licenses
        public ActionResult Licenses()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "訓練、認證、發照");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Job
        public ActionResult Job()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "協會業務");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/About
        public ActionResult About()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "關於我們");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Organization
        public ActionResult Organization()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "組織架構");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/History
        public ActionResult History()
        {
            Article article = db.Articles.FirstOrDefault(a => a.Name == "沿革");
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Member
        public ActionResult Member()
        {
            var certifiedMembers = db.CertifiedMembers.ToList();
            if (certifiedMembers == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMembers);
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
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
