using System.Web.Mvc;
using Db.DataAccess;
using Db.Entity.Vgiik;
using T034.ViewModel;

namespace T034.Controllers
{
    public class MuseumController : Controller
    {
        private readonly IBaseDb _db;

        public MuseumController()
        {
            _db = MvcApplication.DbFactory.CreateBaseDb();
        }

        public ActionResult Publication(int id)
        {
            var model = GetPublication(id);
            if (model == null)
            {
                return View("../404.cshtml");
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("Museum/Publication", model);
            }
            return View(model);
        }

        private PublicationViewModel GetPublication(int id)
        {
            var publication = _db.Get<Publication>(id);

            PublicationViewModel model = null;
            if (publication != null)
            {
                var path = string.Format("/Content/images/publication/{0}/", id);
                model = new PublicationViewModel
                {
                    Pages = new CarouselViewModel(path, Server.MapPath(path), ""),
                    PublicationId = publication.Id
                };
            }

            return model;
        }
    }
}
