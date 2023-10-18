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
using System.Text;
using MVC_ArsonProject.Areas.BackEnd.Filter;

namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class UsersController : Controller
    {
        private Context db = new Context();

        StringBuilder _sbPermissionData = new StringBuilder();

        public ActionResult PermissionEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.PermissionData = GetPermissionData();

            string oldPermissions = "";

            if (user.Permission == null)
            {
                oldPermissions = "[]";
            }
            else
            {
                string[] permissionsStringArr = user.Permission.Split(',');
                oldPermissions = $"[{string.Join(",", permissionsStringArr.Select(p=>"'"+p+"'"))}]";
            }
            ViewBag.OldPermissions = oldPermissions;
            return View(user);
        }

        private string GetPermissionData()
        {
            //全抓
            List<Permission> permissions = db.Permissions.ToList();

            //抓第一層
            List<Permission> roots = db.Permissions.Where(p => p.ParentId == null).ToList();

            _sbPermissionData.Append("[");

            int totalCounts = roots.Count();
            int count = 0;

            foreach (var root in roots)
            {
                _sbPermissionData.Append("{");

                _sbPermissionData.Append($"\"id\":\"{root.Code}\",");
                _sbPermissionData.Append($"\"text\":\"{root.Subject}\"");

                if (root.Permissions.Count() > 0)
                {
                    _sbPermissionData.Append(",");
                    AddChildPermissionData(root);
                }

                _sbPermissionData.Append("}");

                count++;

                if (count < totalCounts)
                {
                    _sbPermissionData.Append(",");
                }
            }

            _sbPermissionData.Append("]");

            return _sbPermissionData.ToString();
        }

        private void AddChildPermissionData(Permission node)
        {
            _sbPermissionData.Append($"\"children\":");
            _sbPermissionData.Append("[");

            int totalCounts = node.Permissions.Count();
            int count = 0;

            foreach (var item in node.Permissions)
            {
                _sbPermissionData.Append("{");

                _sbPermissionData.Append($"\"id\":\"{item.Code}\",");
                _sbPermissionData.Append($"\"text\":\"{item.Subject}\"");

                if (item.Permissions.Count() > 0)
                {
                    _sbPermissionData.Append(",");
                    AddChildPermissionData(item);
                }

                _sbPermissionData.Append("}");

                count++;

                if (count < totalCounts)
                {
                    _sbPermissionData.Append(",");
                }
            }

            _sbPermissionData.Append("]");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionEdit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("PermissionEdit"); 
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

           
            if (user.Permission != null)
            {
                string[] userPermissionsArray = user.Permission.Split(',');

                var userPermissions = db.Permissions.Where(p => userPermissionsArray.Contains(p.Code)).Select(p=>p.Subject).ToList();

                ViewBag.UserPermissions = string.Join(", ", userPermissions);
            }
            

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.PermissionData = GetPermissionData();

            return View();
        }

        // POST: Users/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            var userAccountExist = db.Users.Any(u => u.Account == user.Account);

            if (userAccountExist)
            {
                ViewBag.UserAccountExistMessage = "請換一組帳號，該帳號已存在。";
                return View(user);
            }
            if (user.Password != user.ConfirmPassword)
            {
                ViewBag.DifferentPwd = "密碼 及 確認密碼 不相符。";
                return View(user);
            }
            if (ModelState.IsValid)
            {
                string salt = Security.CreateSalt(16);
                user.Salt = salt;

                if (user.ConfirmPassword == user.Password)
                {
                    string hashenPwd = Security.CreatePasswordHash(user.Password, salt);
                    user.Password = hashenPwd;
                    user.ConfirmPassword = hashenPwd;
                }
                else
                {
                    return View(user);
                }

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.BossCanNotDelete = (string)TempData["BossCanNotDelete"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.Permission != null)
            {
                string[] userPermissionsArray = user.Permission.Split(',');

                var userPermissions = db.Permissions.Where(p => userPermissionsArray.Contains(p.Code)).Select(p => p.Subject).ToList();

                ViewBag.UserPermissions = string.Join(", ", userPermissions);
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo.role == EnumList.Role.Top)
            {
                string BossCanNotDelete = "不能刪除老闆身分的帳號";
                TempData["BossCanNotDelete"] = BossCanNotDelete;

                return RedirectToAction("Delete", new { id = id });
            }

            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
