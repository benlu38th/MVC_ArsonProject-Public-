using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ArsonProject.Models
{
    public class ViewDownloadCreate
    {
        [Required]
        [Display(Name = "標題")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "內容")]
        [MaxLength(1000)]
        [AllowHtml]
        public string Description { get; set; }
    }
}