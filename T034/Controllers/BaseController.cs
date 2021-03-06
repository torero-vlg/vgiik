﻿using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Ninject;
using NLog;
using T034.Api.DataAccess;
using T034.Api.Entity.Administration;
using T034.Tools;
using T034.Tools.Auth;
using T034.ViewModel;

namespace T034.Controllers
{
    public class BaseController : Controller
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IBaseDb Db { get; set; }

        protected UserViewModel UserInfo;

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (controllerName == "Base") return;
            var actionName = context.ActionDescriptor.ActionName;

            var user = "";
            Logger.Debug("Controller: {0}, Action: {1}, UserHost: {2}, User:{3}, Request: {4}", controllerName, actionName, Request.UserHostAddress, user, Request?.Url?.Query);

            if (controllerName.ToLower() != "account" && actionName.ToLower() != "auth")
                SetUserInfo();
        }

        protected override void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.ActionName;
            var controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;

            var user = "";
            Logger.Debug("Controller: {0}, Action: {1}, UserHost: {2}, User:{3}, Request: {4}", controllerName, actionName, Request.UserHostAddress, user, Request?.Url?.Query);
        }

        private void SetUserInfo()
        {
            try
            {
                UserInfo = Request.GetUser();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
            }
        }
    }
}