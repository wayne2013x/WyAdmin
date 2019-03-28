using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WyAdmin.Common;

namespace WyAdmin.Web.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost]
        public ActionResult Signin(string namaVal, string pwdVal)
        {
            return Success(new {
                result=1,
                url= AppConfig.HomePageUrl + "/Home/Index"
            });
        }
        /// <summary>
        /// 注销
        /// </summary>
        [HttpPost]
        public ActionResult Logout()
        {
            return View();
        }
    }
}