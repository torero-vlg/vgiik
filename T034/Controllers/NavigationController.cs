using System.Web.Mvc;
using AutoMapper;
using Ninject;
using T034.Api.Services.Administration;
using T034.ViewModel;

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
                var dto = UserService.Get(UserInfo.Email);
                if (dto != null)
                {
                    var model = new UserViewModel();
                    model = Mapper.Map(dto, model);
                    return PartialView(model);
                }
            }
            return null;
        }
    }
}
