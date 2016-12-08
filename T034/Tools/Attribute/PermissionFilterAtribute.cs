using System.Linq;
using System.Web.Mvc;

namespace T034.Tools.Attribute
{
    public class WebPermissionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var webPermissionDto = MvcApplication.WebPermissions.FirstOrDefault(p => p.Action == action.ToLower() && p.Controller == controller.ToLower());
            if (webPermissionDto == null) return;

            var userPermissions = filterContext.RequestContext.HttpContext.Request.UserPermissions();
            if (userPermissions != null && userPermissions.Contains(webPermissionDto.Name))
                return;
            
            filterContext.Result = new RedirectResult("~/Errors/Unauthorized");
        }
    }

    public class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 403;
        }
    }
}