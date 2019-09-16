using System.Web;
using System.Web.Mvc;
using Web3.Helpers.AttributeFilters;

namespace Web3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(), 0);
            filters.Add(new KeepTempDataAttribute(), 1);
        }
    }
}
