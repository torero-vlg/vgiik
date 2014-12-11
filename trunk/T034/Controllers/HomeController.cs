using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using T034.Tools.Auth;
using T034.ViewModel;

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
            //http://blueimp.github.io/Bootstrap-Image-Gallery/
            //получить адрес сайта
            //string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            var model = new List<CarouselViewModel>
                {
                    GetCarouselViewModel("/Content/images/photo/dpi/", "Кафедра ДПИ"),
                    GetCarouselViewModel("/Content/images/photo/staropoltavka/", "Профориентация в Старополтавке")
                };

            return View(model);
        }

        private CarouselViewModel GetCarouselViewModel(string folder, string header)
        {
            var directory = new DirectoryInfo(Server.MapPath(folder));
            var files = directory.GetFiles().Select(f => f.Name);
            var carousel = new CarouselViewModel
                {
                    Header = header,
                    Files = files,
                    Folder = folder
                };
            return carousel;
        }

        public ActionResult Museum(int room)
        {
            var viewName = "Rooms/Room" + room;
            return View(viewName);
        }

        public ActionResult Presentation()
        {
            return View();
        }
    }
}
