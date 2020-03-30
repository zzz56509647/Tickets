using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week0329.Mvc.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "用户名")]
        [Required]
        public string LoginName { get; set; }
        [Display(Name = "密码")]
        [Required]
        public string Pwd { get; set; }
        public string Remark { get; set; }
    }
}