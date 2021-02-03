using System.Web;
using System.Web.Mvc;

namespace Project_for_App_Domain
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
