using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Attribute;

namespace Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [AopActionFilter(false)]
    [AopExceptionFilter(false)]
    public class ErrorController : Controller
    {
        //
        // GET: /Admin/Error/

        [ValidateInput(false)]
        public ActionResult Index(ErrorModel em)
        {
            ViewData = new ViewDataDictionary<ErrorModel>(em);
            return View();
        }

    }
}