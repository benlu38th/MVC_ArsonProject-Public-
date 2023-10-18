using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Areas.BackEnd.Filter;
using MVC_ArsonProject.Models;

namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class KnowledgeMgmtController : Controller
    {
        private Context db = new Context();

        // GET: BackEnd/KnowledgeMgmt
        public ActionResult Index()
        {
            return View(db.Knowledges.OrderByDescending(k=>k.InitDate).ToList());
        }

        // GET: BackEnd/KnowledgeMgmt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackEnd/KnowledgeMgmt/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DownloadFileName,DownloadFileUrl,InitDate")] Knowledge knowledge, HttpPostedFileBase DownloadFile)
        {
            if (ModelState.IsValid)
            {
                if (DownloadFile != null)//如果有上傳成功
                {
                    if (DownloadFile.ContentLength > 0)//如果是檔案大小>0
                    {
                        // 從上傳的檔案擷取檔名（不含副檔名）
                        string fileName = Path.GetFileNameWithoutExtension(DownloadFile.FileName);

                        // 取得檔案的副檔名
                        string extension = Path.GetExtension(DownloadFile.FileName);

                        // 組合新的檔名：原始檔名 + 現在時間（年月日時分秒） + 副檔名
                        fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmss") + extension;

                        // 組合完整的檔案路徑
                        string savsAsPath = Path.Combine(Server.MapPath("/Areas/BackEnd/UploadKnowledge/"), fileName);

                        // 儲存圖片到伺服器上的指定路徑
                        DownloadFile.SaveAs(savsAsPath);

                        // 設定圖片的取得路徑（相對路徑）
                        knowledge.DownloadFileUrl = "/Areas/BackEnd/UploadKnowledge/" + fileName;

                        // 將圖片資訊存入資料庫
                        db.Knowledges.Add(knowledge);

                        // 執行資料庫變更，將圖片資訊寫入資料庫
                        db.SaveChanges();

                        // 清除模型狀態，以準備進行下一次操作
                        ModelState.Clear();

                        // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "上傳檔案未大於0";
                        return View(knowledge);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "上傳失敗";
                    return View(knowledge);
                }
            }

            return View(knowledge);
        }

        // GET: BackEnd/KnowledgeMgmt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledges.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: BackEnd/KnowledgeMgmt/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Knowledge knowledge, HttpPostedFileBase DownloadFile)
        {
            
            if (ModelState.IsValid)
            {
                if (DownloadFile == null)
                {
                    db.Entry(knowledge).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (DownloadFile.ContentLength > 0)//如果是檔案大小>0
                    {
                        //刪除舊檔案
                        string filePath = Server.MapPath(knowledge.DownloadFileUrl);

                        if (System.IO.File.Exists(filePath))
                        {
                            // 刪除檔案
                            System.IO.File.Delete(filePath);
                        }

                        // 從上傳的檔案擷取檔名（不含副檔名）
                        string fileName = Path.GetFileNameWithoutExtension(DownloadFile.FileName);

                        // 取得檔案的副檔名
                        string extension = Path.GetExtension(DownloadFile.FileName);

                        // 組合新的檔名：原始檔名 + 現在時間（年月日時分秒） + 副檔名
                        fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmss") + extension;

                        // 組合完整的檔案路徑
                        string savsAsPath = Path.Combine(Server.MapPath("/Areas/BackEnd/UploadKnowledge/"), fileName);

                        // 儲存圖片到伺服器上的指定路徑
                        DownloadFile.SaveAs(savsAsPath);

                        // 設定圖片的取得路徑（相對路徑）
                        knowledge.DownloadFileUrl = "/Areas/BackEnd/UploadKnowledge/" + fileName;

                        db.Entry(knowledge).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");

                        // 清除模型狀態，以準備進行下一次操作
                        ModelState.Clear();

                        // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                        return RedirectToAction("Index");
                    }
                }
            }
           
            return View(knowledge);
        }

        // GET: BackEnd/KnowledgeMgmt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledges.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: BackEnd/KnowledgeMgmt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knowledge knowledge = db.Knowledges.Find(id);

            // 設定要刪除的檔案路徑
            string filePath = Server.MapPath(knowledge.DownloadFileUrl);

            if (System.IO.File.Exists(filePath))
            {
                // 刪除檔案
                System.IO.File.Delete(filePath);
            }

            db.Knowledges.Remove(knowledge);
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
