using FrontPagePackages.DataContext;
using System.Web.Mvc;

namespace FrontPagePackages.Controllers
{
    public class PackagesController : Controller
    {
        private DataProvider dataProvider;

        public PackagesController()
        {
            dataProvider = new DataProvider();
        }

        public RedirectToRouteResult Index()
        {
            return RedirectToAction("Package");
        }

        public ActionResult Package()
        {
            return View();
        }

        public JsonResult GetListOfItems()
        {
            return Json(dataProvider.GetItems(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListOfPackages()
        {
            return Json(dataProvider.GetPackages(), JsonRequestBehavior.AllowGet);
        }
    }
}