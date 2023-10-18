using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "標題")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "發文者")]
        public int PosterId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("PosterId")]
        public virtual Member MyMember { get; set; }

        [Required]
        [Display(Name = "內容")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DataType(DataType.Date)]//送出時驗證是不是日期格式
        public DateTime InitDate { get; set; }

    }
}