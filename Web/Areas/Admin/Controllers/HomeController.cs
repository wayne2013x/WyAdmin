using Application.SysClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /Admin/Home/
        protected override void Init()
        {
            base.Init();
            this.IsExecutePowerLogic = false;
        }

        Sys_MenuLogic _Sys_MenuLogic = new Sys_MenuLogic();

        public override ActionResult Index()
        {
            StringBuilder _StringBuilder = new StringBuilder();
            _Sys_MenuLogic.CreateMenus(Guid.Empty, _StringBuilder);
            ViewData["MenuHtml"] = _StringBuilder.ToString();
            return View(_Account);
        }

        /// <summary>
        /// Databorad
        /// </summary>
        public ActionResult Main()
        {
            return View();
        }

    }
}