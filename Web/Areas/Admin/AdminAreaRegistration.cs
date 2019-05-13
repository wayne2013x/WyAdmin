using System.Web.Mvc;

namespace Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] {
                    "Web.Areas.Admin.Controllers",
                    "Web.Areas.Admin.Controllers.Sys",
                    "Web.Areas.Admin.Controllers.Base"
                }
            );
        }
    }
}