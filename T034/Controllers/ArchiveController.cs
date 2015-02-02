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
            var model = GetPerson(personId);
            if (model == null)
            {
                return View("../404.cshtml");
            }
            return View(model);
        }

        private PersonViewModel GetPerson(int personId)
        {
            PersonViewModel model = null;

            switch (personId)
            {
                case 1:
                    model = new PersonViewModel
                        { 
                            FullName =
                                "Кондрашова Клавдия Петровна - ветеран Великой Отечественной войны, преподаватель, и.о. директора, заведующая Сталинградской-Волгоградской школы, училища культуры",
                            Docs = new CarouselViewModel("/Content/images/people/kpk/recollections/",
                                                       Server.MapPath("/Content/images/people/kpk/recollections/"),
                                                       "Воспоминания Клавдии Петровны", "")
                        };
                break;
                case 2:
                    model = new PersonViewModel
                        {
                            FullName =
                                "Переходник Борис Григорьевич - участник Великой Отечественной войны, преподаватель, зав.отделением Сталинградской-Волгоградской школы, училища культуры, заслуженный работник культуры РСФСР",
                            Docs = new CarouselViewModel("/Content/images/people/perehodnik/",
                                                         Server.MapPath("/Content/images/people/perehodnik/"),
                                                         "Документы Бориса Григорьевича", "")
                        };
                break;
                case 3:
                    model = new PersonViewModel
                        {
                            FullName =
                                "Веденеев Геннадий Александрович - художественный руководитель, доцент кафедры режиссуры",
                            Docs = new CarouselViewModel("/Content/images/people/vedeneev/",
                                                         Server.MapPath("/Content/images/people/vedeneev/"),
                                                         "Документы Веденеева Геннадия Александровича", "")
                        };
                break;
            }

            return model;
        }
    }
}
