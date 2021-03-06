﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ninject;
using T034.Api.Services.Administration;
using T034.Tools;
using T034.Tools.Auth;
using T034.ViewModel;

namespace T034.Controllers
{
    public class AuthController : BaseController
    {
        [Inject]
        public IUserService UserService { get; set; }

        public ActionResult LoginWithYandex(string code)
        {
            var accessToken = YandexAuth.GetAuthorizationCookie(Response.Cookies, code, Db);
            FormsAuthentication.SetAuthCookie(accessToken, true);

            return RedirectToActionPermanent("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;

            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Value = "";
                Response.Cookies.Set(aCookie);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RedirectToYandex()
        {
            return Redirect(string.Format("https://oauth.yandex.ru/authorize?response_type=code&client_id={0}", YandexAuth.ClientId)); ;
        }

        public ActionResult Login(LogonViewModel model)
        {
            var result = UserService.Authenticate(model.Email, model.Password);

            if (result.IsAuthenticated)
            {
                Response.SetUserPermissions(result.User.UserRoles.SelectMany(r => r.WebPermissions).Select(wp => wp.Name));
                Response.SetAuth(result.User.Email); 

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Logon", "Account", new LogonViewModel { Email = model.Email, Message = result.Message });
            }
        }
    }
}
