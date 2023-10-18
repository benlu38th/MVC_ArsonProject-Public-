using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ganss.Xss;
using MVC_ArsonProject.Models;
using MvcPaging;

namespace MVC_ArsonProject.Controllers
{
    public class DownloadController : Controller
    {
        private Context db = new Context();

        //設定一頁幾筆
        private const int DefaultPageSize = 3;

        // GET: Download
        public ActionResult Index(int? page)
        {
            if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
            {
                return RedirectToAction("Login", "Members");
            }

            //var posts = db.Posts.Include(p => p.MyMember);

            var newMessages = db.Messages
                .GroupBy(m => m.MyPostId)
                .Select(group => group.OrderByDescending(m => m.InitDate).FirstOrDefault())
                .ToList();

            var combinedList = db.Posts.Include(p => p.MyMember)
                                .GroupJoin(
                                    db.Messages,
                                    post => post.Id,
                                    message => message.MyPostId,
                                    (post, messages) => new ViewDownload
                                    {
                                        Post = post,
                                        LatestMessage = messages.OrderByDescending(m => m.InitDate).FirstOrDefault(),
                                        MessageNums = messages.Count()
                                    }
                                )
                                .ToList();

            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            //總資料筆數
            //ViewBag.Count = db.News.Count();

            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(combinedList.OrderByDescending(p => p.Post.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Download/Details/5
        public ActionResult Details(int? id, int? page)
        {
            if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
            {
                return RedirectToAction("Login", "Members");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.Messages.Where(p => p.MyPostId == id).Count();

            ViewBag.CurrentPageIndex = currentPageIndex+1;
            ViewBag.Post = post;


            var Message = db.Messages.Where(p => p.MyPostId == id).OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize);

            return View(Message);
        }

        // GET: Download/Create
        public ActionResult Create()
        {
            if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
            {
                return RedirectToAction("Login", "Members");
            }


            return View();
        }

        // POST: Download/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewDownloadCreate viewDownloadCreate)
        {
            if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
            {
                return RedirectToAction("Login", "Members");
            }

            if (ModelState.IsValid)
            {
                // 使用者識別
                //string userId = User.Identity.Name;

                int userId = Convert.ToInt32(Session["UserId"]);

                Post newPost = new Post();
                newPost.PosterId = userId;
                newPost.InitDate = DateTime.Now;

                // 創建一個HtmlSanitizer實例
                var sanitizer = new HtmlSanitizer();
                // 清理HTML內容
                string inputHtml = viewDownloadCreate.Description;
                newPost.Description = inputHtml;

                newPost.Title = viewDownloadCreate.Title;



                db.Posts.Add(newPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewDownloadCreate);
        }

        public ActionResult CreateRe(int? id)
        {
            if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
            {
                return RedirectToAction("Login", "Members");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.PostId = post.Id;
            ViewBag.PostTitle = post.Title;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRe(ViewDownloadCreateRe viewDownloadCreateRe)
        {
            if (ModelState.IsValid)
            {
                if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
                {
                    return RedirectToAction("Login", "Members");
                }

                // 使用者識別
                //int userId = Convert.ToInt32(User.Identity.Name);

                int userId = Convert.ToInt32(Session["UserId"]);

                //清洗description
                var sanitizer = new HtmlSanitizer();
                string input = viewDownloadCreateRe.Description;
                string sanitizedDescription = sanitizer.Sanitize(input);

                var result = new Message
                {
                    MessagerId = userId,
                    MyPostId = viewDownloadCreateRe.PostId,
                    Description = sanitizedDescription,
                    InitDate = DateTime.Now
                };

                db.Messages.Add(result);
                db.SaveChanges();

                return RedirectToAction("Details", "Download", new { id = viewDownloadCreateRe.PostId});
            }
            return View(viewDownloadCreateRe);
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
