using System.Web.Mvc;

namespace T034.Controllers
{
    public class ArchiveController : BaseController
    {
        public ActionResult Person(int personId)
        {
            return RedirectToAction("Index", "Person", new { id = personId });
        }

        public ActionResult Department(int departmentid)
        {
            return RedirectToAction("Index", "Department", new {departmentid});
        }

        public ActionResult PhotoList()
        {
            return PartialView("Archive/PhotoList");
        }
    }
}
