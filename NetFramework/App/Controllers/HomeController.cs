using System.Web.Mvc;
using NetFramework.App.Providers;

namespace NetFramework.App.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DirtyRequestProvider requestProvider = new DirtyRequestProvider();
            var result = requestProvider.GetQueryValueOrDefault<int>("param");
            return Content(result.ToString());
        }
    }
}