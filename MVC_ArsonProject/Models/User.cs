﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "帳號")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "確認密碼")]
        [MaxLength(100)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "鹽")]
        [MaxLength(30)]
        public string Salt { get; set; }

        [Required]
        [Display(Name = "職位")]
        public EnumList.Role role { get; set; }

        [Display(Name = "權限")]
        [MaxLength(500)]
        public string Permission { get; set; }
    }
}