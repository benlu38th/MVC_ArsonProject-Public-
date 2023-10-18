using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class CertifiedMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "照片")]
        public string PictureUrl { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "名字")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "姓氏")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "國家")]
        public string Counrty { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "職稱")]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "公司")]
        public string Company { get; set; }


        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? InitDate { get; set; }
    }
}