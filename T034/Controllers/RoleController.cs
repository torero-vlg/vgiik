﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using NLog;
using T034.Api.Dto;
using T034.Api.Services.Administration;
using T034.Api.Services.Common;
using T034.Tools.Attribute;
using T034.ViewModel;

namespace T034.Controllers
{
    public class RoleController : BaseController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IRoleService RoleService { get; set; }

        [Tools.Attribute.Role("Administrator")]
        public ActionResult List()
        {
            try
            {
                var list = RoleService.Select();
                var model = new List<RoleViewModel>();
                model = Mapper.Map(list, model);

                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return View("ServerError", (object)"Получение списка");
            }
        }

        [HttpGet]
        [Role("Administrator")]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new RoleViewModel();
            if (id.HasValue)
            {
                var dto = RoleService.Get(id.Value);
                model = Mapper.Map(dto, model);
            }

            model.WebPermissions.AddRange(Mapper.Map<List<WebPermissionViewModel>>(MvcApplication.WebPermissions));

            return View(model);
        }

        [Tools.Attribute.Role("Administrator")]
        public ActionResult AddOrEdit(RoleViewModel model)
        {
            if (model.Id > 0)
            {
                RoleService.Update(Mapper.Map<RoleDto>(model));
            }
            else
            {
                RoleService.Create(Mapper.Map<RoleDto>(model));
            }


            return RedirectToAction("List");
        }

        [Tools.Attribute.Role("Administrator")]
        public ActionResult Index(int id)
        {
            var model = new RoleViewModel();

            var item = RoleService.Get(id);
            if (item == null)
            {
                return View("ServerError", (object)"Страница не найдена");
            }
            model = Mapper.Map(item, model);

            return View(model);
        }

        [Role("Administrator")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = RoleService.Delete(id);
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
    }
}