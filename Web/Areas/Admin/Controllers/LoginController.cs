using Application.SysClass;
using Common;
using Common.ValidateHelper;
using Entity.SysClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Attribute;

namespace Web.Areas.Admin.Controllers
{
    [AopActionFilter(false)]
    public class LoginController : BaseController
    {
        //
        // GET: /Admin/Login/
        AccountLogic _AccountLogic = new AccountLogic();

        protected override void Init()
        {
            base.Init();
            this.IsExecutePowerLogic = false;
        }

        public override ActionResult Index()
        {
            Tools.SetSession("Account", new Account());
            return View();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Checked(string uName, string uPwd, string loginCode)
        {
            _AccountLogic.Checked(uName, uPwd, loginCode);
            return this.Success(new
            {
                status = 1,
                url = AppConfig.HomePageUrl
            });
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAuthCode()
        {
            ValidateCodeHelper vch = new ValidateCodeHelper();
            string code = vch.GetRandomNumberString(4);
            Tools.SetCookie("loginCode", code, 2);
            return File(vch.CreateImage(code), "image/jpeg");
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Out()
        {
            return RedirectToAction("Index");
        }


    }
}