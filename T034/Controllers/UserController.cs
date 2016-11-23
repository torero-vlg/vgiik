using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using NLog;
using T034.Api.Dto;
using T034.Api.Services.Administration;
using T034.Tools.Attribute;
using T034.ViewModel;

namespace T034.Controllers
{
    public class UserController : BaseController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IRoleService RoleService { get; set; }

        [Role("Administrator")]
        public ActionResult List()
        {
            try
            {
                var list = UserService.Select();
                var model = new List<UserViewModel>();
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
            var model = new UserViewModel();
            if (id.HasValue)
            {
                var dto = UserService.Get(id.Value);
                model = Mapper.Map(dto, model);
            }
            //добавим те роли, которых нет у пользователя, но есть в БД
            foreach (var role in RoleService.Select())
            {
                if (model.UserRoles.Any(ur => ur.Code == role.Code))
                    continue;
                var roleViewModel = new RoleViewModel();
                roleViewModel = Mapper.Map(role, roleViewModel);
                roleViewModel.Selected = false;
                model.UserRoles.Add(roleViewModel);
            }
            
            return View(model);
        }

        [Role("Administrator")]
        public ActionResult AddOrEdit(UserViewModel model)
        {
            if (model.Id > 0)
            {
                UserService.Update(Mapper.Map<UserDto>(model));
            }
            else
            {
                UserService.Create(model.Name, model.Email, model.Password);
            }


            return RedirectToAction("List");
        }

        public ActionResult Index(int id)
        {
            var model = new UserViewModel();

            var dto = UserService.Get(id);
            model = Mapper.Map(dto, model);

            return View(model);
        }
    }
}
