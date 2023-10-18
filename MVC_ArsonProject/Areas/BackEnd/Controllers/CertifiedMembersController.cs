using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Models;
using MVC_ArsonProject.Functions;
using System.IO;
using MVC_ArsonProject.Areas.BackEnd.Filter;

namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class CertifiedMembersController : Controller
    {
        private Context db = new Context();

        // GET: BackEnd/CertifiedMembers
        public ActionResult Index()
        {
            return View(db.CertifiedMembers.ToList());
        }

        // GET: BackEnd/CertifiedMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.CertifiedMembers.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // GET: BackEnd/CertifiedMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackEnd/CertifiedMembers/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PictureUrl,FirstName,LastName,Counrty,Title,Company,InitDate")] CertifiedMember certifiedMember, HttpPostedFileBase ImageFile)
        {
            if(ModelState.IsValid)
            {
                if (ImageFile != null)//如果有上傳成功
                {
                    if (General.IsPicture(ImageFile.FileName))//如果是檔案尾標符合條件
                    {
                        if (General.IsImage(ImageFile) != null)//如果是圖片檔案
                        {
                            if (ImageFile.ContentLength > 0)//如果是檔案大小>0
                            {
                                // 從上傳的圖片擷取檔名（不含副檔名）
                                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);

                                // 取得圖片的副檔名
                                string extension = Path.GetExtension(ImageFile.FileName);

                                // 組合新的檔名：原始檔名 + 現在時間（年月日時分秒毫秒） + 副檔名
                                fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                                // 組合完整的檔案路徑
                                string savsAsPath = Path.Combine(Server.MapPath("~/Areas/BackEnd/UploadImages/CertifiedMemberPic"), fileName);

                                // 儲存圖片到伺服器上的指定路徑
                                ImageFile.SaveAs(savsAsPath);

                                // 設定圖片的取得路徑（相對路徑）
                                certifiedMember.PictureUrl = "/Areas/BackEnd/UploadImages/CertifiedMemberPic/" + fileName;

                                //塞時間
                                certifiedMember.InitDate = DateTime.Now;

                                // 將圖片資訊存入資料庫
                                db.CertifiedMembers.Add(certifiedMember);

                                // 執行資料庫變更，將圖片資訊寫入資料庫
                                db.SaveChanges();

                                // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "檔案大小位大於0";
                                return View(certifiedMember);
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "不符合圖片格式";
                            return View(certifiedMember);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "檔案尾標不符合";
                        return View(certifiedMember);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "上傳失敗";
                    return View(certifiedMember);
                }
            }
            return View(certifiedMember);
        }

        // GET: BackEnd/CertifiedMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.CertifiedMembers.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // POST: BackEnd/CertifiedMembers/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PictureUrl,FirstName,LastName,Counrty,Title,Company,InitDate")] CertifiedMember certifiedMember, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)//如果有上傳檔案
                {
                    if (General.IsPicture(ImageFile.FileName))//如果是檔案尾標符合條件
                    {
                        if (General.IsImage(ImageFile) != null)//如果是圖片檔案
                        {
                            if (ImageFile.ContentLength > 0)//如果是檔案大小>0
                            {
                                // 從上傳的圖片擷取檔名（不含副檔名）
                                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);

                                // 取得圖片的副檔名
                                string extension = Path.GetExtension(ImageFile.FileName);

                                // 組合新的檔名：原始檔名 + 現在時間（年月日時分秒毫秒） + 副檔名
                                fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                                // 組合完整的檔案路徑
                                string savsAsPath = Path.Combine(Server.MapPath("~/Areas/BackEnd/UploadImages/CertifiedMemberPic"), fileName);

                                // 儲存圖片到伺服器上的指定路徑
                                ImageFile.SaveAs(savsAsPath);

                                // 設定圖片的取得路徑（相對路徑）
                                certifiedMember.PictureUrl = "/Areas/BackEnd/UploadImages/CertifiedMemberPic/" + fileName;

                                //塞時間
                                certifiedMember.InitDate = DateTime.Now;

                                // 將 certifiedMember 實體的狀態設置為已修改
                                // 這是為了告訴 Entity Framework，我們對這個實體進行了變更，
                                // 需要將這些變更反映到資料庫中
                                db.Entry(certifiedMember).State = EntityState.Modified;

                                // 執行資料庫變更，將圖片資訊寫入資料庫
                                db.SaveChanges();

                                // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "檔案大小位大於0";
                                return View(certifiedMember);
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "不符合圖片格式";
                            return View(certifiedMember);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "檔案尾標不符合";
                        return View(certifiedMember);
                    }
                }
                else//沒上傳檔案
                {
                    //塞時間
                    certifiedMember.InitDate = DateTime.Now;

                    // 將 certifiedMember 實體的狀態設置為已修改
                    // 這是為了告訴 Entity Framework，我們對這個實體進行了變更，
                    // 需要將這些變更反映到資料庫中
                    db.Entry(certifiedMember).State = EntityState.Modified;

                    // 執行資料庫變更，將圖片資訊寫入資料庫
                    db.SaveChanges();

                    // 返回到預設的視圖（可能是指示操作已完成的訊息頁面）
                    return RedirectToAction("Index");
                }
            }
            return View(certifiedMember);
        }

        // GET: BackEnd/CertifiedMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.CertifiedMembers.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // POST: BackEnd/CertifiedMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CertifiedMember certifiedMember = db.CertifiedMembers.Find(id);
            db.CertifiedMembers.Remove(certifiedMember);
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
