﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WyAdmin.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Signin(string namaVal,string pwdVal)
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

    }
}