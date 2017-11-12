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
                    PublicationService.Create(model);
                }
            }
            catch (Exception ex)
            {
                return Json(new OperationResult { Status = StatusOperation.Error, Message = ex.Message });
            }
            return Json(new OperationResult { Status = StatusOperation.Success, Message = "Операция выполнена успешно" });
        }
    }
}
