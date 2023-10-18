using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "留言者")]
        public int MessagerId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("MessagerId")]
        public virtual Member MyMember { get; set; }

        [Display(Name = "所屬貼文")]
        public int MyPostId { get; set; }

        [Display(Name = "內容")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.Date)]//送出時驗證是不是日期格式
        public DateTime InitDate { get; set; }
    }
}