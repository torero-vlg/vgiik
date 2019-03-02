using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using T034.Api.Entity.Vgiik;
using T034.Tools.Attribute;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class PersonController : BaseController
    {
        [WebPermission("Люди.Редактирование")]
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
        [WebPermission("Люди.Редактирование")]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new PersonViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Person>((object)id.Value);
                model = Mapper.Map(item, model);

                model.Files = GetFiles(model);

                model.Docs.AddRange(
                    item.Albums.Select(a => new CarouselViewModel(a.Path, Server.MapPath(a.Path), a.Name, "")));
                model.Albums.AddRange(
                    item.Albums.Select(a => new AlbumViewModel { Id = a.Id, Path = a.Path, Name = a.Name}));
            }

            return View(model);
        }

        [WebPermission("Люди.Редактирование")]
        public ActionResult AddOrEdit(PersonViewModel model)
        {
            var item = new Person();
            if (model.PersonId > 0)
            {
                item = Db.Get<Person>((object)model.PersonId);
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
            var model = GetPerson(id);
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

        [WebPermission("Люди.Редактирование")]
        public ActionResult Delete(int id)
        {
            var path = Path.Combine(Server.MapPath($"~/{"Content/images/people"}/{id}"));
            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Delete(true);

            var result = Db.Delete(new Person { Id = id});

            return RedirectToAction("List");
        }

        private PersonViewModel GetPerson(int personId)
        {
            var person = Db.Get<Person>((object)personId);

            PersonViewModel model = new PersonViewModel();
            if (person != null)
            {
                model = Mapper.Map(person, model);

                model.Docs.AddRange(
                    person.Albums.Select(a => new CarouselViewModel(a.Path, Server.MapPath(a.Path), a.Name, "")));

                model.Files = GetFiles(model);
            }

            if (personId == 24)
                model.Videos = new List<VideoViewModel>
                    {
                        new VideoViewModel
                        {
                            Width = "420",
                            Height = "315",
                            Source = "https://www.youtube.com/embed/c_obyoeuPGo"
                        },
                        new VideoViewModel
                        {
                            Width = "420",
                            Height = "315",
                            Source = "https://www.youtube.com/embed/CqI7FuD_ytc"
                        }
                    };

            if (personId == 8)
                model.Videos = new List<VideoViewModel>
                    {
                        new VideoViewModel
                        {
                            Width = "560",
                            Height = "315",
                            Source = "https://www.youtube.com/embed/0Qk_yy8jOH4"
                        },
                        new VideoViewModel
                        {
                            Width = "560",
                            Height = "315",
                            Source = "https://www.youtube.com/embed/8cri-sIzH84"
                        },
                    };

            return model;
        }

        private IEnumerable<string> GetFiles(PersonViewModel model)
        {
            IEnumerable<string> files = new List<string>();

            try
            {
                var directory = new DirectoryInfo(Server.MapPath(model.FilesFolder));
                files = directory.GetFiles().Select(f => f.Name);
            }
            catch (Exception ex)
            {
            }

            return files;
        }
    }
}
