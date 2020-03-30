using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Week0329.Mvc.Models;
using System.Configuration;

namespace Week0329.Mvc.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserInfo u)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                UserInfo Users = conn.GetAll<UserInfo>().Where(s => s.LoginName == u.LoginName && s.Pwd == u.Pwd).FirstOrDefault();
                if (Users != null)
                {
                    Session["u"] = Users;
                    return Content("<script>alert('登陆成功！');location.href='/Buy/Show';</script>");
                }
                else
                {
                    return Content("<script>alert('用户名或密码不正确！');location.href='/Login/Login';</script>");
                }
            }

        }
    }
}