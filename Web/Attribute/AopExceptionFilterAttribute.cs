using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AopExceptionFilterAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 是否 执行
        /// </summary>
        private bool _IsExecute { get; set; }
        public AopExceptionFilterAttribute(bool IsExecute)
        {
            _IsExecute = IsExecute;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (_IsExecute)
                ExceptionWeb(filterContext);
            base.OnException(filterContext);
        }

        /// <summary>
        /// 后端异常处理
        /// </summary>
        private void ExceptionWeb(ExceptionContext filterContext)
        {
            //判断是否是自定义异常类型
            if (filterContext.Exception is MessageBox)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    //返回错误信息
                    filterContext.Result = new JsonResult() { Data = MessageBox.errorModel };
                else
                {
                    var errorModel = new ErrorModel(filterContext.Exception.Message);
                    var sb = new StringBuilder();
                    sb.Append("<script src=\"/Scripts/jquery-3.3.1.min.js\"></script>");
                    sb.Append("<script src=\"/Scripts/libs/layer-v3.1.1/layer/layer.js\"></script>");
                    sb.Append("<script src=\"/Scripts/admin.js\"></script>");
                    sb.Append("<script type='text/javascript'>");
                    sb.Append("$(function(){ admin.alert('" + errorModel.msg.Trim().Replace("'", "“").Replace("\"", "”") + "', '警告'); });");
                    sb.Append("</script>");
                    filterContext.Result = new ContentResult() { Content = sb.ToString(), ContentType = "text/html;charset=utf-8;" };
                }
                filterContext.HttpContext.Response.StatusCode = 200;
            }
            else
            {
                Tools.Log.WriteLog(filterContext.Exception, filterContext.HttpContext.Request.UserHostAddress);
                ErrorModel em = new ErrorModel(filterContext.Exception.Message);
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    //返回错误信息
                    filterContext.Result = new JsonResult() { Data = em };
                    filterContext.HttpContext.Response.StatusCode = 200;
                }
                else
                {
                    filterContext.Result = new ViewResult() { ViewName = AppConfig.ErrorPageUrl, ViewData = new ViewDataDictionary<ErrorModel>(em) };
                }
            }
            //表示异常已处理
            filterContext.ExceptionHandled = true;
        }




    }
}