using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class Knowledge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(30, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [MinLength(6, ErrorMessage = "{0}長度至少必須為 {1}  個字元")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [Display(Name = "檔案")]
        public string DownloadFileUrl { get; set; }

        [Display(Name = "日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }
    }
}