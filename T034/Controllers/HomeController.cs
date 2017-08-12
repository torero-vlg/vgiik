using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using T034.Api.Entity.Vgiik;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
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

            var directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/dpi/"));

            var files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var dpi = files.Select(file => new NodeViewModel {Path = "/Content/images/photo/dpi/" + file, Description = ""});

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/staropoltavka/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var staropoltavka = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/staropoltavka/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/24012015/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var nodes = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/24012015/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/summer2012/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var summer2012 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/summer2012/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/autumn2012/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var autumn2012 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/autumn2012/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/motorship2013/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var motorship2013 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/motorship2013/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/13082015/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var photo13082015 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/13082015/" + file, Description = "" });

			directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/traditional/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var traditional = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/traditional/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/conference2017/"));
            files = directory.Exists ? directory.GetFiles().Select(f => f.Name) : new List<string>();
            var conference2017 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/conference2017/" + file, Description = "" });

            var model = new List<CarouselViewModel>
                {
                    new CarouselViewModel(dpi, "Кафедра ДПИ", ""),
                    new CarouselViewModel(staropoltavka, "Профориентация в Старополтавке", ""),
                    new CarouselViewModel(nodes, "", ""),
                    new CarouselViewModel(summer2012, "Лето - 2012", ""),
                    new CarouselViewModel(autumn2012, "Осень - 2012", ""),
                    new CarouselViewModel(motorship2013, "Теплоход - 2013", ""),
                    new CarouselViewModel(photo13082015, "", ""),
					new CarouselViewModel(traditional, "Кафедра традиционной культуры", ""),
					new CarouselViewModel(conference2017, "Городская-студенческая научно-практическая конференция: «Царицын – Сталинград - Волгоград». Волгоградский государственный институт искусств и культуры, Волгоград, 30.05.2017, ул. Циолковского, 4, кабинет информатики и технических средств.", "")
                };

            return View(model);
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
