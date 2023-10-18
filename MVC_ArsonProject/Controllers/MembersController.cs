using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC_ArsonProject.Models;
using MVC_ArsonProject.Functions;
using Newtonsoft.Json;
using System.Web.Security;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace MVC_ArsonProject.Controllers
{
    public class MembersController : Controller
    {
        private Context db = new Context();

        // GET: Members/LogOff
        public ActionResult LogOff()
        {
            ////登出驗證表單
            //FormsAuthentication.SignOut();

            Session.Remove("LoggedIn");
            Session.Remove("UserId");
            Session.Remove("UserData");

            return RedirectToAction("Login");
        }

        // GET: Members/Login
        public ActionResult Login()
        {
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"] == true)
            {
                return RedirectToAction("Index", "Download");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewMemberLogin viewMemberLogin)
        {
            if (ModelState.IsValid)
            {
                string inputAccount = viewMemberLogin.Account;
                string inputPassword = viewMemberLogin.Password;

                var memberInfo = db.Members.FirstOrDefault(m => m.Account == inputAccount);

                if (memberInfo == null)//無此帳號
                {
                    ViewBag.ErrorMessage = "帳號錯誤!";

                    return View();
                }
                else
                {
                    string inputHashPassword = Security.CreatePasswordHash(inputPassword, memberInfo.Salt);

                    if (inputHashPassword != memberInfo.Password)
                    {
                        ViewBag.ErrorMessage = "帳號或密碼錯誤!";

                        return View();
                    }

                    string userData = JsonConvert.SerializeObject(memberInfo);

                    // 使用 Session 變數記錄登入狀態
                    Session["LoggedIn"] = true;
                    Session["UserId"] = memberInfo.Id;
                    Session["UserData"] = userData;

                    //var tempt = Security.SetAuthenTicket(userData, Convert.ToString(memberInfo.Id));

                    ////將 Cookie 寫入回應
                    //Response.Cookies.Add(tempt);

                    return RedirectToAction("Index", "Download");
                }
            }

            return View();
        }

        // GET: Members/Register
        public ActionResult Register()
        {
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"] == true)
            {
                return RedirectToAction("Index", "Download");
            }

            return View();
        }

        // POST: Members/Register
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member member, HttpPostedFileBase[] MembershipFiles)
        {
            bool sameAccount = db.Members.Any(m => m.Account == member.Account);

            if (sameAccount)
            {
                ViewBag.ErrorMessage = "帳號已有人使用，請變更帳號！";

                return View(member);
            }


            Boolean MembershipFilesSuccess = true;
            List<string> errorFiles = new List<string>();

            //跑迴圈判斷檔案(會員證影本)是否沒問題
            foreach (HttpPostedFileBase MembershipFile in MembershipFiles)
            {
                if (MembershipFile != null)//如果有上傳成功
                {
                    if (MembershipFile.ContentLength > 0)//如果是檔案大小>0
                    {
                    }
                    else
                    {
                        errorFiles.Add(MembershipFile.FileName);
                        MembershipFilesSuccess = false;
                    }
                }
                else
                {
                    errorFiles.Add(MembershipFile.FileName);
                    MembershipFilesSuccess = false;
                }
            }

            if (!MembershipFilesSuccess)//上傳的會員證影本有誤
            {
                ViewBag.ErrorFiles = errorFiles;

                return View(member);
            }
            else//會員證影本正確
            {
                List<string> allMembershipFiles = new List<string>();//存入資料庫的會員證影本集合
                if (ModelState.IsValid)
                {
                    foreach (HttpPostedFileBase MembershipFile in MembershipFiles)
                    {
                        //從上傳檔案擷取檔名
                        string fileName = Path.GetFileNameWithoutExtension(MembershipFile.FileName);

                        //取得檔案副檔名
                        string extension = Path.GetExtension(MembershipFile.FileName);

                        //組合成新的檔名:原始檔名+現在時間(年月日時分秒毫秒)+副檔名
                        fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                        //組合成完整檔案路徑
                        string saveAsPath = Path.Combine(Server.MapPath("~/Upload/MembershipFiles/"), fileName);

                        //將檔案儲存到伺服器上的指定路徑
                        MembershipFile.SaveAs(saveAsPath);

                        //將檔案名稱加入List allMembershipFiles中
                        allMembershipFiles.Add(fileName);
                    }

                    //製造鹽巴
                    string salt = Security.CreateSalt(16);

                    //製造存入資料庫的密碼
                    string password = Security.CreatePasswordHash(member.Password, salt);

                    //整理存入SQL的資料
                    member.InitDate = DateTime.Now;
                    member.MembershipFileUrl = string.Join(";", allMembershipFiles);
                    member.Salt = salt;
                    member.Password = password;
                    member.ConfirmPassword = password;

                    db.Members.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    return View(member);
                }
            }
        }

        // GET: Members/Edit
        public ActionResult Edit()
        {
            if (Session["LoggedIn"] == null || (bool)Session["LoggedIn"] != true)
            {
                return RedirectToAction("Login");
            }

            // 取出認證票證。
            //FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;

            // 您可以從票證中獲取存儲的用戶相關數據和其他信息。
            //string userData = ticket.UserData;
            //int userId = Convert.ToInt32(ticket.Name);

            string userData = Convert.ToString(Session["UserData"]);
            int userId = Convert.ToInt32(Session["UserId"]);

            Member member = db.Members.Find(userId);

            if (member == null)
            {
                return HttpNotFound();
            }

            //初始化
            ViewMemberEdit viewMemberEdit = new ViewMemberEdit
            {
                MemberEdit = member,
                Password = "",
                ConfirmPassword = ""
            };

            return View(viewMemberEdit);
        }

        // POST: Members/Edit
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewMemberEdit viewMemberEdit, HttpPostedFileBase[] MembershipFiles)
        {
            if (ModelState.IsValid)
            {
                Boolean MembershipFilesSuccess = true;
                List<string> errorFiles = new List<string>();

                if (MembershipFiles[0] != null)
                {
                    //跑迴圈判斷檔案(會員證影本)是否沒問題
                    foreach (HttpPostedFileBase MembershipFile in MembershipFiles)
                    {
                        if (MembershipFile != null)//如果有上傳成功
                        {
                            if (MembershipFile.ContentLength > 0)//如果是檔案大小>0
                            {
                            }
                            else
                            {
                                errorFiles.Add(MembershipFile.FileName);
                                MembershipFilesSuccess = false;
                            }
                        }
                        else
                        {
                            errorFiles.Add(MembershipFile.FileName);
                            MembershipFilesSuccess = false;
                        }
                    }
                }

                if (!MembershipFilesSuccess)//上傳的會員證影本有誤
                {
                    ViewBag.ErrorFiles = errorFiles;

                    return View(viewMemberEdit);
                }
                else//會員證影本正確
                {
                    List<string> allMembershipFiles = new List<string>();//存入資料庫的會員證影本集合

                    if (MembershipFiles[0] != null)
                    {
                        foreach (HttpPostedFileBase MembershipFile in MembershipFiles)
                        {
                            //從上傳檔案擷取檔名
                            string fileName = Path.GetFileNameWithoutExtension(MembershipFile.FileName);

                            //取得檔案副檔名
                            string extension = Path.GetExtension(MembershipFile.FileName);

                            //組合成新的檔名:原始檔名+現在時間(年月日時分秒毫秒)+副檔名
                            fileName = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                            //組合成完整檔案路徑
                            string saveAsPath = Path.Combine(Server.MapPath("~/Upload/MembershipFiles/"), fileName);

                            //將檔案儲存到伺服器上的指定路徑
                            MembershipFile.SaveAs(saveAsPath);

                            //將檔案名稱加入List allMembershipFiles中
                            allMembershipFiles.Add(fileName);
                        }

                        //整理存入SQL的資料
                        viewMemberEdit.MemberEdit.MembershipFileUrl = string.Join(";", allMembershipFiles);
                    }



                    //判斷是否修改密碼
                    if (!string.IsNullOrEmpty(viewMemberEdit.Password) && !string.IsNullOrEmpty(viewMemberEdit.ConfirmPassword))//修改密碼
                    {
                        //製造存入資料庫的密碼
                        string password = Security.CreatePasswordHash(viewMemberEdit.MemberEdit.Password, viewMemberEdit.MemberEdit.Salt);

                        viewMemberEdit.MemberEdit.Password = password;
                        viewMemberEdit.MemberEdit.ConfirmPassword = password;
                    }

                    // 需要將這些變更反映到資料庫中
                    db.Entry(viewMemberEdit.MemberEdit).State = EntityState.Modified;

                    // 保存更改
                    db.SaveChanges();

                    return RedirectToAction("Index", "Download");
                }
            }

            return View(viewMemberEdit);

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
