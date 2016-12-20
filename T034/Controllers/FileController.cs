using System;
using System.IO;
using System.Web.Mvc;
using Ninject;
using NLog;
using T034.Api.Entity;
using T034.Api.Services;
using T034.Tools.Attribute;

namespace T034.Controllers
{
    public class FileController : BaseController
    {
        [Inject]
        public IFileService FileService { get; set; }

        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        [HttpPost]
        [WebPermission("Файлы.Редактирование")]
        public ActionResult UploadFile()
        {
            try
            {

                var folder = "Upload/Files";
                //var path = Path.Combine(Server.MapPath($"~/{MvcApplication.FilesFolder}"));
                var path = Path.Combine(Server.MapPath($"~/{folder}"));
                var r = FileService.Upload(path, Request, UserInfo.Email);
                //TODO надо что-то возвращать
                return Json(r);

            }
            catch (Exception ex)
            {
                return Json(string.Empty);
            }
        }

        [WebPermission("Файлы.Редактирование")]
        public ActionResult DeleteFile(int id)
        {
            try
            {
                var folder = FileService.DeleteFile(id);

                return RedirectToAction("Edit", new { id = folder.Id });
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return View("ServerError", (object)string.Format("Ошибка при удалении файла."));
            }
        }
        
        public ActionResult Download(int id)
        {
            try
            {
                var item = Db.Get<Files>(id);
                if (item == null)
                {
                    //TODO обработка
                }

                //TODO в Files должна храниться папка, в которой сохранён файл
                var folder = "";
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath($"/{folder}/{item.Name}"));
                string fileName = item.Name;

                item.DownloadCounter++;
                Db.SaveOrUpdate(item);
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(fileBytes, GetMimeType(fileName));
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return View("ServerError", (object)"Ошибка при загрузке файла");
            }
        }

        private string GetMimeType(string filename)
        {

            var extension = string.IsNullOrEmpty(filename) ? "" : filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
            switch (extension)
            {
                case ".pdf": return "application/pdf";

                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".png": return "image/png";
                case ".tiff": return "image/tiff";

                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";

                case ".zip": return "application/zip";
                case ".rar": return "application/x-rar-compressed";
                case ".7z": return "application/zip";
                case ".txt": return "text/plain";
                case ".mp3": return "audio/mpeg";
                case ".avi": return "video/mpeg";
                case ".cs": return "text/plain";
                case ".ppt": return "application/vnd.ms-powerpoint";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
