using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class ContactUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0}必填")]
        [MaxLength(100, ErrorMessage = "{0}長度不能超過為{1}個字元")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "性別")]
        public EnumList.Gender GenderType { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(30)]
        [Display(Name = "連絡電話")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(30)]
        [Display(Name = "詢問標題")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(1000)]
        [Display(Name = "詢問內容")]
        public string Description { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? InitDate { get; set; }

    }
}