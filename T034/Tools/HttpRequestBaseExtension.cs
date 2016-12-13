using System;
using System.Security.Cryptography;
using System.Web;
using T034.ViewModel;

namespace T034.Tools
{
    public static class HttpRequestBaseExtension
    {
        public static bool HasUserPermissions(this HttpRequestBase request, string permission)
        {
            var salt = new byte[16];

            var r = new Rfc2898DeriveBytes(permission, salt, 10000);
            var hash = r.GetBytes(32);
            var str = Convert.ToBase64String(hash);

            var permissionCookie = request.Cookies["permissions"]?.Value;
            return permissionCookie != null && permissionCookie.Contains(str);
        }

        public static bool IsAuthorized(this HttpRequestBase request)
        {
            return string.IsNullOrEmpty(request.Cookies["auth"]?.Value);
        }

        public static UserViewModel GetUser(this HttpRequestBase request)
        {
            UserViewModel user = null;
            if (request.Cookies["auth"] != null)
            {
                user = new UserViewModel
                {
                    Email = request.Cookies["auth"].Value
                };
            }
            return user;
        }
    }
}
