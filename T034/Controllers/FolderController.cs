using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using NLog;
using T034.Api.Entity;
using T034.Api.Services;
using T034.Api.Services.Common.Exceptions;
using T034.Tools.Attribute;
using T034.ViewModel;

namespace T034.Controllers
{
    public class FolderController : BaseController
    {
        [Inject]
        public IFileService FileService { get; set; }

        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index(int? id)
        {
            //ViewData["MetMenuActive"] = folder.Contains("Методическая работа/") ? "active" : "";
            //ViewData["DocsMenuActive"] = folder.Contains("Документы/") ? "active" : "";


            var model = new FolderViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Folder>(id.Value);
                model = Mapper.Map(item, model);

                if (item != null)
                {
                    model = CreateFolderViewModel(model);
                    return View(model);
                }
                return View("ServerError", (object)string.Format("Отсутствует папка."));
            }

            model.SubDirectories = GetSubDirectories(null);

            return View(model);
        }

        [WebPermission("Файлы.Редактирование")]
        public ActionResult Edit(int? id)
        {
            var model = new FolderViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Folder>(id.Value);
                model = Mapper.Map(item, model);
                
                if (item != null)
                {
                    model = CreateFolderViewModel(model);
                    return View(model);
                }
                return View("ServerError", (object)string.Format("Отсутствует папка."));
            }

            model.SubDirectories = GetSubDirectories(null);

            return View(model);
        }

        private FolderViewModel CreateFolderViewModel(FolderViewModel model)
        {
            var items = Mapper.Map<IEnumerable<FileViewModel>>(Db.Where<Files>(f => f.Folder.Id == model.Id));

            var subs = GetSubDirectories(model.Id);

            model.Files = items;
            model.SubDirectories = subs;
                
            return model;
        }

        private IEnumerable<FolderViewModel> GetSubDirectories(int? folderId)
        {
            var subDirectory = Db.Where<Folder>(f => f.ParentFolder.Id == folderId);

            var subs = subDirectory.Select(subDir => new FolderViewModel
                {
                    Id = subDir.Id,
                    Name = subDir.Name,
                    Files = Mapper.Map<IEnumerable<FileViewModel>>(Db.Where<Files>(ff => ff.Folder.Id == subDir.Id))
                }).ToList();
            return subs;
        }

        [WebPermission("Файлы.Редактирование")]
        public ActionResult DeleteFolder(FolderViewModel model)
        {
            try
            {
                var item = Mapper.Map<Folder>(model);
                FileService.DeleteFolder(item);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return View("ServerError", (object) $"Ошибка при удалении папки {model.Name}.");
            }

            return RedirectToAction("Edit", new { id = model.ParentFolderId });
        }

        [WebPermission("Файлы.Редактирование")]
        public ActionResult CreateFolder(FolderViewModel model)
        {
            var item = new Folder();
            if (model.Id > 0)
            {
                item = FileService.GetFolder(model.Id); 
            }
            item = Mapper.Map(model, item);

            var errorMessage = "";

            try
            {
                FileService.CreateFolder(UserInfo.Email, item);
            }
            catch (UserNotFoundException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                errorMessage = "Произошла непредвиденная ошибка";
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new { Message = errorMessage == "" ? "Папка успешно создана" : errorMessage, IsSuccess = errorMessage == ""});
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return View("ServerError", (object)errorMessage);

            return RedirectToAction("Edit", new { id = item.Id });
        }
    }
}
