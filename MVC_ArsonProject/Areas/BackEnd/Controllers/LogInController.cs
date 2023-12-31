﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_ArsonProject.Models;
using MVC_ArsonProject.Functions;

namespace MVC_ArsonProject.Areas.BackEnd.Controllers
{
    public class LogInController : Controller
    {
        public Context db = new Context();

        public ActionResult LogOut()
        {
            //登出驗證表單
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }

        // GET: BackEnd/LogIn
        public ActionResult Index()
        {
            //檢查用戶是否已驗證
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "UsersList");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string account, string password)
        {
            var userInfo = db.Users.Where(u => u.Account == account).FirstOrDefault();

            if (userInfo == null) //帳號不存在
            {
                ViewBag.AccountError = "帳號錯誤";
                return View();
            }
            else
            {
                string salt = userInfo.Salt;

                string inputPwd = Security.CreatePasswordHash(password, salt);

                if (inputPwd != userInfo.Password) //密碼錯誤
                {
                    ViewBag.PwdError = "密碼錯誤";
                    return View();
                }
                else
                {
                    //宣告驗證票要夾帶的資料 (用;區隔)
                    string userData = userInfo.Account + ";" + userInfo.role.ToString();

                    //設定驗證票(夾帶資料，cookie 命名) // 需額外引入using System.Web.Configuration;
                    var tempt = Security.SetAuthenTicket(userData, Convert.ToString(userInfo.Id));

                    //將 Cookie 寫入回應
                    Response.Cookies.Add(tempt);

                    return RedirectToAction("Index", "UsersList");
                }
            }
        }
    }
}