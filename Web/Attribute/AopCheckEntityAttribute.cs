using DataFramework.SqlServerContext;
using Entity.Class;
using Application.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Web.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AopCheckEntityAttribute : ActionFilterAttribute
    {
        private DbContextSqlServer db = new DbContextSqlServer();
        private CheckModel<BaseClass> _CheckModel = new CheckModel<BaseClass>();
        private string[] ParamName { get; set; }

        public AopCheckEntityAttribute()
        {
            this.ParamName = new string[] { "model" };
        }

        public AopCheckEntityAttribute(string[] _ParamName)
        {
            this.ParamName = _ParamName;
        }

        /// <summary>
        /// 每次请求Action之前发生，，在行为方法执行前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            foreach (var item in ParamName)
            {
                var _Value = (BaseClass)filterContext.ActionParameters[item];
                if (_Value != null)
                {
                    if (!_CheckModel.Check(_Value))
                    {
                        throw new MessageBox(_CheckModel.ErrorMessage);
                    }
                }
            }


        }

    }
}