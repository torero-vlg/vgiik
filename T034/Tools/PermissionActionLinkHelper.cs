using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace T034.Tools
{
    public static class PermissionActionLinkHelper
    {
        public static MvcHtmlString PermissionActionLink(this HtmlHelper htmlHelper, string text, string actionName, string controllerName)
        {
            return InternalPermissionActionLink(htmlHelper, text, actionName, controllerName, null, null);
        }

        public static MvcHtmlString PermissionActionLink(this HtmlHelper htmlHelper, string text, string actionName, string controllerName, object routeValues, IDictionary<string, object> htmlAttributes)
        {
            return InternalPermissionActionLink(htmlHelper, text, actionName, controllerName, routeValues, htmlAttributes);
        }

        private static MvcHtmlString InternalPermissionActionLink(this HtmlHelper htmlHelper, string text, string actionName, string controllerName, object routeValues, IDictionary<string, object> htmlAttributes)
        {
            var userPermissions = htmlHelper.ViewContext.HttpContext.Request.UserPermissions();

            if (userPermissions == null)
                return MvcHtmlString.Empty;

            var permission = MvcApplication
                .WebPermissions
                .FirstOrDefault(wp => string.Equals(wp.Action, actionName, StringComparison.CurrentCultureIgnoreCase)
                    && string.Equals(wp.Controller, controllerName, StringComparison.CurrentCultureIgnoreCase));

            if (permission == null || userPermissions.Contains(permission.Name))
                return htmlHelper.ActionLink(text, actionName, controllerName, routeValues, htmlAttributes);

            return MvcHtmlString.Empty;
        }
    }
}
