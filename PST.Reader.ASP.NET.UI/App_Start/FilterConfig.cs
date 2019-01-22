using System.Web;
using System.Web.Mvc;

namespace PST.Reader.ASP.NET.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
