using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Functions;
using MVC_ArsonProject.Models;
using Ganss.Xss;
using System.Drawing;
using System.Drawing.Drawing2D;
using MVC_ArsonProject.Areas.BackEnd.Filter;

namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class NewsMgmtController : Controller
    {
        private Context db = new Context();

        // GET: BackEnd/NewsMgmt
        public ActionResult Index()
        {
            return View(db.News.OrderByDescending(n => n.InitDate).ThenByDescending(n => n.Id).ToList());
        }

        // GET: BackEnd/NewsMgmt/Details/5
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

        // GET: BackEnd/NewsMgmt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackEnd/NewsMgmt/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Sumary,Description,CoverUrl,InitDate")] News news, HttpPostedFileBase CoverFile)
        {
            if (ModelState.IsValid)
            {
                if (CoverFile != null)//如果有上傳成功
                {
                    if (General.IsPicture(CoverFile.FileName))//如果是檔案尾標符合條件
                    {
                        if (General.IsImage(CoverFile) != null)//如果是圖片檔案
                        {
                            if (CoverFile.ContentLength > 0)//如果是檔案大小>0
                            {
                                // 從上傳的圖片擷取檔名（不含副檔名）
                                string fileName = Path.GetFileNameWithoutExtension(CoverFile.FileName);

                                // 取得圖片的副檔名
                                string extension = Path.GetExtension(CoverFile.FileName);

                                // 組合新的檔名：原始檔名 + 現在時間（年月日時分秒毫秒） + 副檔名
                                fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                                // 組合完整的檔案路徑
                                string savsAsPath = Path.Combine(Server.MapPath("~/Areas/BackEnd/UploadImages/NewsCover"), fileName);

                                // 儲存圖片到伺服器上的指定路徑
                                CoverFile.SaveAs(savsAsPath);

                                // 設定圖片的取得路徑（相對路徑）
                                news.CoverUrl = "/Areas/BackEnd/UploadImages/NewsCover/" + fileName;

                                //清理CKEditor內容
                                // 創建一個HtmlSanitizer實例
                                var sanitizer = new HtmlSanitizer();

                                // 配置允許的標簽、屬性等（可選）
                                //sanitizer.AllowedTags.Add("strong");

                                // 清理HTML內容
                                string sanitizedHtml = sanitizer.Sanitize(news.Description);
                                news.Description = sanitizedHtml;

                                //塞時間
                                if (news.InitDate == null)
                                {
                                    news.InitDate = DateTime.Now;
                                }

                                // 將圖片資訊存入資料庫
                                db.News.Add(news);

                                // 執行資料庫變更，將圖片資訊寫入資料庫
                                db.SaveChanges();







                                // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "檔案大小未大於0";
                                return View(news);
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "不符合圖片格式";
                            return View(news);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "檔案尾標不符合";
                        return View(news);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "上傳失敗";
                    return View(news);
                }
            }

            return View(news);
        }

        // GET: BackEnd/NewsMgmt/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: BackEnd/NewsMgmt/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Sumary,Description,CoverUrl,InitDate")] News news, HttpPostedFileBase CoverFile)
        {
            if (ModelState.IsValid)
            {
                if (CoverFile != null)//如果有上傳檔案
                {
                    if (General.IsPicture(CoverFile.FileName))//如果是檔案尾標符合條件
                    {
                        if (General.IsImage(CoverFile) != null)//如果是圖片檔案
                        {
                            if (CoverFile.ContentLength > 0)//如果是檔案大小>0
                            {
                                // 從上傳的圖片擷取檔名（不含副檔名）
                                string fileName = Path.GetFileNameWithoutExtension(CoverFile.FileName);

                                // 取得圖片的副檔名
                                string extension = Path.GetExtension(CoverFile.FileName);

                                // 組合新的檔名：原始檔名 + 現在時間（年月日時分秒毫秒） + 副檔名
                                fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                                // 組合完整的檔案路徑
                                string savsAsPath = Path.Combine(Server.MapPath("~/Areas/BackEnd/UploadImages/NewsCover"), fileName);

                                // 儲存圖片到伺服器上的指定路徑
                                CoverFile.SaveAs(savsAsPath);

                                // 設定圖片的取得路徑（相對路徑）
                                news.CoverUrl = "/Areas/BackEnd/UploadImages/NewsCover/" + fileName;

                                //清理CKEditor內容
                                // 創建一個HtmlSanitizer實例
                                var sanitizer = new HtmlSanitizer();

                                // 配置允許的標簽、屬性等（可選）
                                //sanitizer.AllowedTags.Add("strong");

                                // 清理HTML內容
                                string sanitizedHtml = sanitizer.Sanitize(news.Description);
                                news.Description = sanitizedHtml;

                                // 將編輯的資訊更新至資料庫
                                db.Entry(news).State = EntityState.Modified;

                                // 執行資料庫變更，將圖片資訊寫入資料庫
                                db.SaveChanges();

                                // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "檔案大小未大於0";
                                return View(news);
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "不符合圖片格式";
                            return View(news);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "檔案尾標不符合";
                        return View(news);
                    }
                }
                else //沒上傳檔案
                {
                    //清理CKEditor內容
                    // 創建一個HtmlSanitizer實例
                    var sanitizer = new HtmlSanitizer();

                    // 配置允許的標簽、屬性等（可選）
                    //sanitizer.AllowedTags.Add("strong");

                    // 清理HTML內容
                    string sanitizedHtml = sanitizer.Sanitize(news.Description);
                    news.Description = sanitizedHtml;

                    // 將編輯的資訊更新至資料庫
                    db.Entry(news).State = EntityState.Modified;

                    // 執行資料庫變更，將圖片資訊寫入資料庫
                    db.SaveChanges();

                    // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                    return RedirectToAction("Index");
                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: BackEnd/NewsMgmt/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: BackEnd/NewsMgmt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
