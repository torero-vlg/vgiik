using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using T034.Api.Entity.Vgiik;
using T034.Api.Services;
using T034.Tools.Attribute;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class HomeController : BaseController
    {
        [Inject]
        public IPersonService PersonService { get; set; }

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
			
            var model = new List<CarouselViewModel>
                {
                    new CarouselViewModel(dpi, "Кафедра ДПИ", ""),
                    new CarouselViewModel(staropoltavka, "Профориентация в Старополтавке", ""),
                    new CarouselViewModel(nodes, "", ""),
                    new CarouselViewModel(summer2012, "Лето - 2012", ""),
                    new CarouselViewModel(autumn2012, "Осень - 2012", ""),
                    new CarouselViewModel(motorship2013, "Теплоход - 2013", ""),
                    new CarouselViewModel(photo13082015, "", ""),
					new CarouselViewModel(traditional, "Кафедра традиционной культуры", "")
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

        [WebPermission("Администрирование")]
        public ActionResult FileFolderToAllPerson()
        {
            var personList = PersonService.Select();

            foreach (var person in personList)
            {
                var filesFolder = Path.Combine(Server.MapPath("~/Content/images/people"), person.Id.ToString(), "files");
                if(Directory.Exists(filesFolder))
                    continue;
                var directoryInfo = new DirectoryInfo(filesFolder);
                directoryInfo.Create();
            }
            
            return View("Index");
        }
    }
}
