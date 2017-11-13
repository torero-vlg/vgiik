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
using T034.Api.Dto.Vgiik;
using T034.Api.Services.Common;
using T034.Tools.Attribute;

namespace T034.Controllers
{
    public class PublicationController : BaseController
    {
        [Inject]
        public IPublicationService PublicationService { get; set; }

        [WebPermission("Альбомы.Редактирование")]
        public ActionResult List()
        {
            try
            {
                var list = PublicationService.Select();
                return View(list);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return View("ServerError", (object)"Получение списка");
            }
        }


        [HttpGet]
        [WebPermission("Альбомы.Редактирование")]
        public ActionResult AddOrEdit(int? id)
        {
            var dto = new PublicationDto();
            if (id.HasValue)
            {
                dto = PublicationService.Get(id.Value);
            }

            return View(dto);
        }

        [WebPermission("Альбомы.Редактирование")]
        public ActionResult AddOrEdit(PublicationDto model)
        {
            try
            {
                if (model.Id > 0)
                {
                    PublicationService.Update(model);
                }
                else
                {
                    var result = PublicationService.Create(model);

                    var path = $"/Content/images/publication/{result.Id}/";
                    if (!Directory.Exists(Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(Server.MapPath(path));
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new OperationResult { Status = StatusOperation.Error, Message = ex.Message });
            }
            return Json(new OperationResult { Status = StatusOperation.Success, Message = "Операция выполнена успешно" });
        }

        [WebPermission("Альбомы.Редактирование")]
        public ActionResult Index(int id)
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

        [HttpGet]
        [WebPermission("Альбомы.Редактирование")]
        public ActionResult Delete(int? id)
        {
            try
            {
                var result = PublicationService.Delete(id);
                if (result.Status != StatusOperation.Success)
                {
                    Logger.Error(result.Message);
                    return View("ServerError", (object)result.Message);
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return View("ServerError", (object)"Ошибка");
            }
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
    }
}
