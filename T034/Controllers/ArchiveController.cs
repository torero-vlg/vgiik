using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Db.DataAccess;
using Db.Entity;
using Db.Entity.Vgiik;
using T034.ViewModel;

namespace T034.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IBaseDb _db;

        public ArchiveController()
        {
            _db = MvcApplication.DbFactory.CreateBaseDb();
        }

        public ActionResult Person(int personId)
        {
            var model = GetPerson(personId);
            if (model == null)
            {
                return View("../404.cshtml");
            }
            return View(model);
        }

        private PersonViewModel GetPerson(int personId)
        {
            var person = _db.Get<Person>(personId);

            PersonViewModel model = null;
            if (person != null)
            {
                model = new PersonViewModel
                    {
                        FullName =
                            string.Format("{0} - {1}", person.FullName, person.Title),
                        Docs = new List<CarouselViewModel>(),
                        PersonId = person.Id
                    };
                foreach (var album in person.Albums)
                {
                    var nodes = album.Nodes.Select(n => new NodeViewModel{Description = n.Description, Path = n.Path});
                    model.Docs.Add(new CarouselViewModel(nodes, album.Name, ""));
                }

                IEnumerable<string> files = new List<string>();

                try
                {
                    var directory = new DirectoryInfo(Server.MapPath(model.FilesFolder));
                    files = directory.GetFiles().Select(f => f.Name);
                }
                catch (Exception ex)
                {
                }

                model.Files = files;
            }



            return model;
        }
    }
}
