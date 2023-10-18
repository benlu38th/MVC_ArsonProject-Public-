using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class ViewMemberEdit
    {
        public Member MemberEdit { get; set; }

        [MaxLength(100, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "確認密碼必須與密碼相同")]
        public string ConfirmPassword { get; set; }
    }
}