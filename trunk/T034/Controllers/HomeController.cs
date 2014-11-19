using System.Web.Mvc;
using T034.Tools.Auth;

namespace T034.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auth()
        {
            var model = YandexAuth.GetUser(Request);

            return PartialView("AuthPartialView", model);
        }

        public ActionResult Archive()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }

        public ActionResult Photo()
        {
            return View();
        }

        public ActionResult Museum(int room)
        {
            return View();
        }

        public ActionResult Presentation()
        {
            return View();
        }
    }
}
