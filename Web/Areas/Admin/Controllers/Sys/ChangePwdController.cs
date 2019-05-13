using Application.SysClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers.Sys
{
    public class ChangePwdController : BaseController
    {
        //
        // GET: /ManageSys/ChangePwd/
        public AccountLogic _Logic = new AccountLogic();

        protected override void Init()
        {
            this.MenuID = "Z-150";
        }

        public override ActionResult Index()
        {
            ViewData["userName"] = _Logic.Get(_Account.UserID).User_Name;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePwd(string oldpwd, string newpwd, string newlypwd)
        {
            _Logic.ChangePwd(oldpwd, newpwd, newlypwd);
            return this.Success();
        }

    }
}