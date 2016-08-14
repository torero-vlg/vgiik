using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity.Vgiik;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class MuseumController : BaseController
    {
        public ActionResult Publication(int id)
        {
            var model = GetPublication(id);
            if (model == null)
            {
                return View("../404.cshtml");
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("Museum/Publication", model);
            }
            return View(model);
        }

        private PublicationViewModel GetPublication(int id)
        {
            var publication = Db.Get<Publication>(id);

            PublicationViewModel model = null;
            if (publication != null)
            {
                var path = string.Format("/Content/images/publication/{0}/", id);
                model = new PublicationViewModel
                {
                    Pages = new CarouselViewModel(path, Server.MapPath(path), ""),
                    PublicationId = publication.Id
                };
            }

            return model;
        }

        public ActionResult Veteran(int id)
        {
            var model = GetVeteran(id);
            if (model == null)
            {
                return View("../404.cshtml");
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("Archive/Person", model);
            }
            return View(model);
        }

        private PersonViewModel GetVeteran(int personId)
        {
            var person = Db.Get<Veteran>(personId);

            var model = new PersonViewModel();
            if (person != null)
            {
                model = Mapper.Map(person, model);

                model.Docs.AddRange(
                    person.Albums.Select(a => new CarouselViewModel(a.Path, Server.MapPath(a.Path), a.Name, "")));

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
