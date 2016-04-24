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
    public class AlbumController : BaseController
    {
        [Role("Administrator")]
        public ActionResult List(int? personId, int? departmentId)
        {
            try
            {
                var items = GetAlbums(personId, departmentId);

                var model = new List<AlbumViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка");
            }
        }

        private List<Album> GetAlbums(int? personId, int? departmentId)
        {
            if (personId.HasValue)
                return Db.Where<Album>(a => a.Person.Id == personId.Value);
            if (departmentId.HasValue)
                return Db.Where<Album>(a => a.Department.Id == departmentId.Value);

            return Db.Select<Album>();
        }

        [HttpGet]
        [Role("Administrator")]
        public ActionResult AddOrEdit(int? id, int? personId, int? departmentId)
        {
            var model = new AlbumViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Album>(id.Value);
                model = Mapper.Map(item, model);

                var directory = new DirectoryInfo(Server.MapPath(item.Path));
                model.Files = directory.GetFiles().Select(f => f.Name);
            }
            else
            {
                model.PersonId = personId;
                model.DepartmentId = departmentId;
            }

            var persons = Db.Select<Person>();
            model.Persons = Mapper.Map<ICollection<SelectListItem>>(persons);

            model.Persons.Add(new SelectListItem { Value = null, Text = "-", Selected = model.PersonId.HasValue == false });

            if (model.Persons.All(m => m.Selected == false))
            {
                var selected = model.Persons.FirstOrDefault(m => m.Value == model.PersonId.Value.ToString());
                selected.Selected = true;
            }

            var departments = Db.Select<Department>();
            model.Departments = Mapper.Map<ICollection<SelectListItem>>(departments);

            model.Departments.Add(new SelectListItem { Value = null, Text = "-", Selected = model.DepartmentId.HasValue == false });

            if (model.Departments.All(m => m.Selected == false))
            {
                var selected = model.Departments.FirstOrDefault(m => m.Value == model.DepartmentId.Value.ToString());
                selected.Selected = true;
            }


            return View(model);
        }

        [Role("Administrator")]
        public ActionResult AddOrEdit(AlbumViewModel model)
        {
            var item = new Album();
            if (model.Id > 0)
            {
                item = Db.Get<Album>(model.Id);
            }
            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            var path = Path.Combine(Server.MapPath("~" + item.Path));
            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Create();


            return RedirectToAction("List");
        }

        public ActionResult Index(int id)
        {
            var model = new AlbumViewModel();

            var item = Db.Get<Album>(id);
            if (item == null)
            {
                return View("ServerError", (object)"Страница не найдена");
            }
            model = Mapper.Map(item, model);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = Db.Get<Album>(id);
            var result = Db.Delete(item);

            var path = Path.Combine(Server.MapPath($"~/{item.Path}"));
            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Delete(true);

            return RedirectToAction("List");
        }

        [Role("Moderator")]
        public JsonResult DeleteFile(string filePath)
        {
            try
            {
                var file = new FileInfo(Server.MapPath(filePath));
                file.Delete();

                return Json(new { Message = $"Файл удалён", FilePath = filePath }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = $"Ошибка при удалении файла: {ex.Message}", FilePath = filePath }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
