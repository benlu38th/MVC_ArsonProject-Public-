using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ArsonProject.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100, ErrorMessage = "{0}長度不能超過為{1}個字元")]
        [Display(Name = "名稱")]
        public string Name { get; set; }

        [Display(Name = "內容")]
        [AllowHtml]
        public string Description { get; set; }
    }
}