using System.Collections.Generic;
using System.Web.Mvc;
using T034.ViewModel;

namespace T034.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Logon(LogonViewModel model)
        {
            model.Clients = new List<LoginInfoModel>();
            return View(model);
        }
    }
}
