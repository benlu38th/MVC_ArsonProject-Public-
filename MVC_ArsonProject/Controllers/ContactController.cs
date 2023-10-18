using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MVC_ArsonProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC_ArsonProject.Controllers
{
    public class ContactController : Controller
    {
        private Context db = new Context();

        // GET: Contact/Index
        public ActionResult Index()
        {
            return View();
        }

        // POST: Contact/Index
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactUser contactUser)
        {
            var apiKey = ConfigurationManager.AppSettings["RecaptchaSecretKey"];
            var success = VerifyCaptcha(apiKey);

            if (!success)
            {
                ViewBag.Message = "機器人驗證失敗！！！";

                return View(contactUser);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    contactUser.InitDate = DateTime.Now;
                    db.ContactUsers.Add(contactUser);
                    db.SaveChanges();

                    //以下開始寄信
                    // 取得收件者信箱
                    string toMail = "xxx@gmail.com";

                    // 設定寄件者信箱和應用程式密碼
                    string fromMail = "xxx@gmail.com";
                    string fromPassword = "password";

                    // 建立郵件訊息物件
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = $"{contactUser.Title} -國際縱火調查人員協會台灣分會-";
                    message.To.Add(new MailAddress(toMail));
                    message.Body = @"<html>
<head>
    <style>
        body {
            background-color: #f4ede8;
            font-family: 'Courier New', Courier, monospace;
            color: #603c39;
            text-align: center;
            line-height: 1.6;
        }

        h1 {
            font-size: 32px;
            color: #603c39;
            text-decoration: underline;
            margin: 20px 0;
        }

        p {
            color: #603c39;
            font-size: 20px;
            margin-bottom: 10px;
        }

        .contact-info {
            font-size: 16px;
            margin-bottom: 20px;
        }

    </style>
</head>" + $@"<body>
    <div class='contact-info'>
<h1> 聯絡我們 from國際縱火調查人員協會台灣分會 </h1>
          <p>姓 名：</p>
        <p>{contactUser.Name}</p>
        <p class='contact-label'>性 別：</p>
        <p>{contactUser.GenderType}</p >
<p class= 'contact-label'>聯絡電話：</p >
<p>{contactUser.Telephone}</p>
<p class= 'contact-label'>E-mail：</p>
<p>{contactUser.Email}</p>
<p class= 'contact-label'>詢問標題：</p>
<p>{contactUser.Title}</p>
<p class= 'contact-label' >詢問內容：</p>
<p>{contactUser.Description}</p>
</div>
</body>
</html>";
                    message.IsBodyHtml = true;

                    // 設定 SMTP 客戶端
                    var smptClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };

                    // 送出郵件
                    smptClient.Send(message);

                    return RedirectToAction("Success");
                }
                else
                {
                    return View(contactUser);
                }
            }
        }

        // GET: Contact/Success
        public ActionResult Success()
        {
            return View();
        }

        private bool VerifyCaptcha(string apiKey)
        {
            var url = "https://www.google.com/recaptcha/api/siteverify";
            var wc = new System.Net.WebClient();
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var data = "secret=" + apiKey + "&response=" + Request.Form["g-recaptcha-response"];
            var json = wc.UploadString(url, data);
            // JSON 反序化取 .success 屬性 true/false 判斷
            var success = JsonConvert.DeserializeObject<JObject>(json).Value<bool>("success");

            return success;
        }
    }
}
