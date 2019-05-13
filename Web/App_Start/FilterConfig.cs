using System.Web;
using System.Web.Mvc;
using Web.Attribute;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AopExceptionFilterAttribute(true));
        }
    }
}
