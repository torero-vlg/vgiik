using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using T034.Api.Entity.Vgiik;
using T034.ViewModel;
using T034.ViewModel.Common;
using T034.Api.Services.Vgiik;
using Ninject;

namespace T034.Controllers
{
    public class MuseumController : BaseController
    {
        [Inject]
        public IPublicationService PublicationService { get; set; }

        //TODO использовать PublicationController
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
            var publication = PublicationService.Get(id);

            PublicationViewModel model = null;
            if (publication != null)
            {
                model = new PublicationViewModel
                {
                    Pages = new CarouselViewModel(publication.Path, Server.MapPath(publication.Path), ""),
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
            }

            return model;
        }
    }
}
