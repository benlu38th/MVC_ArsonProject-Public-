using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class ViewMemberLogin
    {
        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(30, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [MinLength(6, ErrorMessage = "{0}長度至少必須為 {1}  個字元")]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [MinLength(6, ErrorMessage = "{0}長度至少必須為 {1}  個字元")]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}