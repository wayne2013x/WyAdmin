using Common;
using DataFramework;
using Entity.SysClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attribute
{
    public class AopActionFilterAttribute : ActionFilterAttribute
    {
        private bool _IsExecute { get; set; }

        public AopActionFilterAttribute(bool IsExecute = true)
        {
            this._IsExecute = IsExecute;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //登陆超时验证
            this.CheckedLoginAccount(filterContext);
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 检查登录帐户
        /// </summary>
        private void CheckedLoginAccount(ActionExecutingContext filterContext)
        {
            if (this._IsExecute)
            {
                var accountM = Tools.GetSession<Account>("Account");

                if (accountM == null || accountM.UserID.ToGuid() == Guid.Empty)
                {

                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult()
                        {
                            Data = new ErrorModel(AppConfig.LoginPageUrl, EMsgStatus.登录超时20),
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        filterContext.Result = new ContentResult()
                        {
                            Content = @"<script type='text/javascript'>
                                        alert('登录超时！需要重新登录！');
                                        top.window.location='" + AppConfig.LoginPageUrl + @"';
                                    </script>",
                            ContentType = "text/html;charset=utf-8;"
                        };
                    }

                }
            }

        }





    }
}