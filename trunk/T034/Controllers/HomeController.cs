using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Db.DataAccess;
using Db.Entity.Vgiik;
using T034.Tools;
using T034.Tools.Auth;
using T034.ViewModel;

namespace T034.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBaseDb _db;

        public HomeController()
        {
            _db = MvcApplication.DbFactory.CreateBaseDb();
        }

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
            var model = new ArchiveViewModel
                {
                    Persons = _db.Select<Person>().OrderBy(p => p.FullName).ToList(),
                    Departments = _db.Select<Department>()
                        .Select(d => new DepartmentViewModel{Id = d.Id, Name = d.Name})
                        .ToList()
                };

            
            return View(model);
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
            var files = directory.GetFiles().Select(f => f.Name);
            var dpi = files.Select(file => new NodeViewModel {Path = "/Content/images/photo/dpi/" + file, Description = ""});

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/staropoltavka/"));
            files = directory.GetFiles().Select(f => f.Name);
            var staropoltavka = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/staropoltavka/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/24012015/"));
            files = directory.GetFiles().Select(f => f.Name);
            var nodes = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/24012015/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/summer2012/"));
            files = directory.GetFiles().Select(f => f.Name);
            var summer2012 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/summer2012/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/autumn2012/"));
            files = directory.GetFiles().Select(f => f.Name);
            var autumn2012 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/autumn2012/" + file, Description = "" });

            directory = new DirectoryInfo(Server.MapPath("/Content/images/photo/motorship2013/"));
            files = directory.GetFiles().Select(f => f.Name);
            var motorship2013 = files.Select(file => new NodeViewModel { Path = "/Content/images/photo/motorship2013/" + file, Description = "" });
            
            var model = new List<CarouselViewModel>
                {
                    new CarouselViewModel(dpi, "Кафедра ДПИ"),
                    new CarouselViewModel(staropoltavka, "Профориентация в Старополтавке"),
                    new CarouselViewModel(nodes, ""),
                    new CarouselViewModel(summer2012, "Лето - 2012"),
                    new CarouselViewModel(autumn2012, "Осень - 2012"),
                    new CarouselViewModel(motorship2013, "Теплоход - 2013")
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
