using Common;
using Common.LogService;
using DataFramework.SqlServerContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Attribute;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DbContextSqlServer.SetDefaultConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            LogHelper.CreateRepository(Server.MapPath("/Log4Net/log4net.config"));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles); //不需要
            //注册自定义视图
            ViewEngines.Engines.Clear();                    // 清除原MVC视图引擎规则
            ViewEngines.Engines.Add(new CustomViewEngine());  // 使用自定义视图引擎 /Sys/Base

            Tools.Log.WriteLog("应用程序已启动");
        }
        protected void Application_Error()
        {
            if (Server.GetLastError() != null) Tools.Log.WriteLog((Exception)Server.GetLastError().GetBaseException(), Request.UserHostAddress);
        }

        protected void Application_End()
        {
            Tools.Log.WriteLog("Web程序已结束");
        }
    }
}
