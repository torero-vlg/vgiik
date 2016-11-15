using System.Web.Mvc;
using Ninject;
using NLog;
using T034.Api.DataAccess;
using T034.Api.Entity.Administration;
using T034.Tools.Auth;

namespace T034.Controllers
{
    public class BaseController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IBaseDb Db { get; set; }

        [Inject]
        public IAuthentication Auth { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (controllerName == "Base") return;
            var actionName = context.ActionDescriptor.ActionName;

            var user = "";
            logger.Debug("Controller: {0}, Action: {1}, UserHost: {2}, User:{3}, Request: {4}", controllerName, actionName, Request.UserHostAddress, user, Request?.Url?.Query);
        }

        protected override void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.ActionName;
            var controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;

            var user = "";
            logger.Debug("Controller: {0}, Action: {1}, UserHost: {2}, User:{3}, Request: {4}", controllerName, actionName, Request.UserHostAddress, user, Request?.Url?.Query);
        }
    }
}