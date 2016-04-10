using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity.Vgiik;
using T034.Tools.Attribute;
using T034.ViewModel;

namespace T034.Controllers
{
    public class PersonController : BaseController
    {
        [Role("Administrator")]
        public ActionResult List()
        {
            try
            {
                var items = Db.Select<Person>();

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
        [Role("Administrator")]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new PersonViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Person>(id.Value);
                model = Mapper.Map(item, model);
                model.Docs.AddRange(
                    item.Albums.Select(a => new CarouselViewModel(a.Path, Server.MapPath(a.Path), a.Name, "")));
                model.Albums.AddRange(
                    item.Albums.Select(a => new AlbumViewModel { Id = a.Id, Path = a.Path, Name = a.Name}));
            }

            return View(model);
        }

        [Role("Administrator")]
        public ActionResult AddOrEdit(PersonViewModel model)
        {
            var item = new Person();
            if (model.PersonId > 0)
            {
                item = Db.Get<Person>(model.PersonId);
            }
            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            var path = Path.Combine(Server.MapPath($"~/{"Content/images/people"}/{result}"));
            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Create();

            return RedirectToAction("List");
        }

        public ActionResult Index(int id)
        {
            var model = new PersonViewModel();

            var item = Db.Get<Person>(id);
            if (item == null)
            {
                return View("ServerError", (object)"Страница не найдена");
            }
            model = Mapper.Map(item, model);

            return View(model);
        }


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
