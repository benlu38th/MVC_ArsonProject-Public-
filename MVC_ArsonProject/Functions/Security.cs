using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace MVC_ArsonProject.Functions
{
    public class Security
    {
        // 建立一個指定長度的隨機salt值
        public static string CreateSalt(int saltLenght)
        {
            //生成一個加密的隨機數
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);

            //返回一個Base64隨機數的字串
            return Convert.ToBase64String(buff);
        }

        //// 返回加密後的字串(密碼 , salt)
        //public static string CreatePasswordHash(string pwd, string strSalt)
        //{
        //    //把密碼和Salt連起來
        //    string saltAndPwd = String.Concat(pwd, strSalt);
        //    //對密碼進行雜湊
        //    string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
        //    //轉為小寫字元並擷取前16個字串
        //    hashenPwd = hashenPwd.ToLower().Substring(0, 16);
        //    //返回雜湊後的值
        //    return hashenPwd;
        //}

        // 新方法 返回加密後的字串(密碼 , salt)
        public static string CreatePasswordHash(string password, string salt)
        {
            // 把密碼和Salt連起來
            string saltedPassword = string.Concat(password, salt);

            // 建立SHA-256哈希算法實例
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // 創建一個StringBuilder對象，用於動態構建最終的十六進制字串表示形式
                StringBuilder builder = new StringBuilder();

                // 遍歷SHA-256雜湊的每一個位元組
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    // 將每個位元組轉換為十六進制字串，並追加到StringBuilder中
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        /// <summary>
        /// 設定認證票證並創建用戶的認證 Cookie。
        /// </summary>
        /// <param name="userData">要存儲在認證票證中的用戶特定數據。</param>
        /// <param name="userId">用戶的識別符。</param>
        /// <returns>包含加密認證票證的認證 Cookie。</returns>
        public static HttpCookie SetAuthenTicket(string userData, string userId)
        {
            // 聲明一個認證票證。
            // 注意：需要額外引入 using System.Web.Security;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(1), false, userData);

            // 加密認證票證。
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // 創建一個 Cookie。
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // 寫入 Cookie 到回應。
            return authenticationCookie;
        }
    }
}