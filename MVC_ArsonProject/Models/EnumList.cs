using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_ArsonProject.Models
{
    public class EnumList
    {
        public enum Gender
        {
            男 = 0,
            女 = 1
        }
        public enum Application
        {
            正式會員,
            準會員,
            個人贊助會員,
            學生會員
        }
        public enum Role
        {
            Top,
            Manager,
            Staff
        }
    }
}