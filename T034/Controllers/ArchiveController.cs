using System.Linq;
using System.Web.Mvc;
using T034.Api.Entity.Vgiik;
using T034.ViewModel;

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

        public ActionResult PersonList()
        {
            var persons = Db.Select<Person>().OrderBy(p => p.FullName).ToList();

            return PartialView("Archive/PersonList", persons);
        }

        public ActionResult DepartmentList()
        {
            var departments = Db.Select<Department>()
                        .Select(d => new DepartmentViewModel { Id = d.Id, Name = d.Name })
                        .ToList();

            return PartialView("Archive/DepartmentList", departments);
        }
    }
}
