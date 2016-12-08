using System;
using System.Collections.Generic;
using System.Web;

namespace T034.Tools
{
    public static class HttpResponseBaseExtension
    {
        public static void SetUserPermissions(this HttpResponseBase response, IEnumerable<string> permissions)
        {
            var permissionCookie = new HttpCookie("permissions") { Value = string.Join(",", permissions), Expires = DateTime.Now.AddDays(30) };
            response.Cookies.Set(permissionCookie);
        }

        public static void SetAuth(this HttpResponseBase response, string email)
        {
            var authCookie = new HttpCookie("auth") { Value = email, Expires = DateTime.Now.AddDays(30) };
            response.Cookies.Set(authCookie);
        }
    }
}
