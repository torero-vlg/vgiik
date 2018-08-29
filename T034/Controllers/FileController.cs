using System;
using System.IO;
using System.Web.Mvc;
using Ionic.Zip;
using Ninject;
using NLog;
using T034.Api.Services.Vgiik;

namespace T034.Controllers
{
    public class FileController : BaseController
    {
        [Inject]
        public IPublicationService PublicationService { get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Скачать папку в виде архива
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult DownloadPublication(string path)
        {
            try
            {
                var zipPath = Path.Combine(path, "publication.zip");
                var zipServerPath = Server.MapPath(zipPath);

                if (System.IO.File.Exists(zipServerPath))
                    return File(zipServerPath,
                                   "application/zip", "publication.zip");

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(Server.MapPath(path));

                    zip.Save(zipServerPath);

                    return File(zipServerPath,
                                   "application/zip", "publication.zip");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
    }
}
