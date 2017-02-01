using System.Web;
using System.Web.Mvc;

namespace MOHFW_API_SERVICE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
