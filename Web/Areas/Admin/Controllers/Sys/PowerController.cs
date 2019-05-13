using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers.Sys
{
    public class PowerControlController : Controller
    {
        //权限控制
        // GET: /ManageSys/PowerControl/

        public ActionResult Index()
        {
            return PartialView();
        }

    }
}