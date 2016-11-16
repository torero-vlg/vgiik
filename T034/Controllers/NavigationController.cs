using System.Web.Mvc;
using Ninject;
using T034.Api.Services.Administration;

namespace T034.Controllers
{
    public class NavigationController : BaseController
    {
        [Inject]
        public IUserService UserService { get; set; }

        public ActionResult ManagementMenu()
        {
            //если есть пользователь в БД, то показываем меню
            if (UserInfo != null)
            {
                var user = UserService.GetUser(UserInfo.Email);
                if (user != null)
                {
                    return PartialView(user);
                }
            }
            return null;
        }
    }
}
