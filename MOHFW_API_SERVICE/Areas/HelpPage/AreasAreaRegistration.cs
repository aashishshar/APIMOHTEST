using System.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace MOHFW_API_SERVICE.Areas.Areas
{
    public class AreasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Areas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Areas_default",
                "Areas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}