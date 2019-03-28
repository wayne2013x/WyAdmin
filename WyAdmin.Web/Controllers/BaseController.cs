using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WyAdmin.Web.Controllers
{
    [RouteArea("Login")]
    public class BaseController : Controller
    {
        /// <summary>
        /// 返回 Json 信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult Success(object obj = null)
        {
            if (obj == null)
                return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
            return Json(obj, JsonRequestBehavior.DenyGet);
        }

    }
}