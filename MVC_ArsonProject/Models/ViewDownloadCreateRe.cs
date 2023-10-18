using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ArsonProject.Models
{
    public class ViewDownloadCreateRe
    {
        [Display(Name = "主題編號")]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "回覆內容")]
        [MaxLength(1000)]
        [AllowHtml]
        public string Description { get; set; }
    }
}