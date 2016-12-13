using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using T034.Api.Entity.Vgiik;
using T034.Tools.Attribute;
using T034.ViewModel;

namespace T034.Controllers
{
    public class VeteranController : BaseController
    {
        [WebPermission("Ветераны.Редактирование")]
        public ActionResult List()
        {
            try
            {
                var items = Db.Select<Veteran>();

                var model = new List<PersonViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка");
            }
        }

        [HttpGet]
        [WebPermission("Ветераны.Редактирование")]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new PersonViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Veteran>(id.Value);
                model = Mapper.Map(item, model);
                model.Docs.AddRange(
                    item.Albums.Select(a => new CarouselViewModel(a.Path, Server.MapPath(a.Path), a.Name, "")));
                model.Albums.AddRange(
                    item.Albums.Select(a => new AlbumViewModel { Id = a.Id, Path = a.Path, Name = a.Name}));
            }

            return View(model);
        }

        [ValidateInput(false)]
        [WebPermission("Ветераны.Редактирование")]
        public ActionResult AddOrEdit(PersonViewModel model)
        {
            var item = new Veteran();
            if (model.PersonId > 0)
            {
                item = Db.Get<Veteran>(model.PersonId);
            }
            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            var path = Path.Combine(Server.MapPath($"~/{"Content/images/veteran"}/{result}"));
            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Create();

            return RedirectToAction("List");
        }

        public ActionResult Index(int id)
        {
            var model = new PersonViewModel();

            var item = Db.Get<Veteran>(id);
            if (item == null)
            {
                return View("ServerError", (object)"Страница не найдена");
            }
            model = Mapper.Map(item, model);

            return View(model);
        }


        [WebPermission("Ветераны.Редактирование")]
        public ActionResult Delete(int id)
        {
            var path = Path.Combine(Server.MapPath($"~/{"Content/images/people"}/{id}"));
            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Delete(true);

            var result = Db.Delete(new Person { Id = id});

            return RedirectToAction("List");
        }
    }
}
