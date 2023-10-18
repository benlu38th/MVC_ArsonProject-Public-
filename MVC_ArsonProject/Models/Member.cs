using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(30, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [MinLength(6, ErrorMessage = "{0}長度至少必須為 {1}  個字元")]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [MaxLength(30)]
        [MinLength(6)]
        [Display(Name = "鹽")]
        public string Salt { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [MinLength(6, ErrorMessage = "{0}長度至少必須為 {1}  個字元")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "確認密碼必須與密碼相同")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "{0}長度不能超過為 {1}  個字元")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "性別")]
        public EnumList.Gender GenderType { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [Display(Name = "生日")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "申請類別")]
        public EnumList.Application ApplicationType { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(30)]
        [Display(Name = "連絡電話(公)")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(30)]
        [Display(Name = "手機")]
        public string Cellphone { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(30)]
        [Display(Name = "通訊處")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "國際會籍")]
        public Boolean InternationalMembership { get; set; }

        [Display(Name = "會員證影本")]
        public string MembershipFileUrl { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(100)]
        [Display(Name = "現職單位")]
        public string Job { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(100)]
        [Display(Name = "最高學歷")]
        public string HighestEducation { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(100)]
        [Display(Name = "服務單位")]
        public string ServiceUnit1 { get; set; }

        [Required(ErrorMessage = "{0}為必填欄位")]
        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string JobTitle1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^(19|20)\d\d$")]
        [Display(Name = "年")]
        public string StartYear1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])$")]
        [Display(Name = "月")]
        public string StartMonth1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^(19|20)\d\d$")]
        [Display(Name = "年")]
        public string EndYear1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])$")]
        [Display(Name = "月")]
        public string EndMonth1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "服務單位")]
        public string ServiceUnit2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string JobTitle2 { get; set; }

        [RegularExpression(@"^(19|20)\d\d$")]
        [Display(Name = "年")]
        public string StartYear2 { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])$")]
        [Display(Name = "月")]
        public string StartMonth2 { get; set; }

        [RegularExpression(@"^(19|20)\d\d$")]
        [Display(Name = "年")]
        public string EndYear2 { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])$")]
        [Display(Name = "月")]
        public string EndMonth2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "服務單位")]
        public string ServiceUnit3 { get; set; }

        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string JobTitle3 { get; set; }

        [RegularExpression(@"^(19|20)\d\d$")]
        [Display(Name = "年")]
        public string StartYear3 { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])$")]
        [Display(Name = "月")]
        public string StartMonth3 { get; set; }

        [RegularExpression(@"^(19|20)\d\d$")]
        [Display(Name = "年")]
        public string EndYear3 { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])$")]
        [Display(Name = "月")]
        public string EndMonth3 { get; set; }

        [Required(ErrorMessage = "相關年資(年)必填")]
        [Display(Name = "相關年資合計(年)")]
        public int TotalYear { get; set; }

        [Required(ErrorMessage = "相關年資(月)必填")]
        [Display(Name = "相關年資合計(月)")]
        public int TotalMonth { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? InitDate { get; set; }
    }
}