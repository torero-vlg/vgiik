using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T034.ViewModel;

namespace T034.Controllers
{
    public class ArchiveController : Controller
    {
        public ActionResult Person(int personId)
        {
            var model = new PersonViewModel();

            if (personId == 1)
            {
                model.FullName =
                    "Кондрашова Клавдия Петровна - ветеран Великой Отечественной войны, преподаватель, и.о. директора, заведующая Сталинградской-Волгоградской школы, училища культуры";
                model.Docs = new CarouselViewModel("/Content/images/people/kpk/recollections/",
                                                   Server.MapPath("/Content/images/people/kpk/recollections/"),
                                                   "Воспоминания Клавдии Петровны", "");
            }
            if (personId == 2)
            {
                model.FullName =
                    "Переходник Борис Григорьевич - участник Великой Отечественной войны, преподаватель, зав.отделением Сталинградской-Волгоградской школы, училища культуры, заслуженный работник культуры РСФСР";
                model.Docs = new CarouselViewModel("/Content/images/people/perehodnik/",
                                                   Server.MapPath("/Content/images/people/perehodnik/"),
                                                   "Документы Бориса Григорьевича", "");
            }
            return View(model);
        }
    }
}
