using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ArsonProject.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [Display(Name = "摘要")]
        public string Sumary { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [Display(Name = "內文")]
        [AllowHtml]
        public string Description { get; set; }

        [MaxLength(1000, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [Display(Name = "封面照")]
        public string CoverUrl { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }
    }
}