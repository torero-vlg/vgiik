using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace T034.Tools
{
    public static class HttpResponseBaseExtension
    {
        /// <remarks> по информации с http://stackoverflow.com/questions/4181198/how-to-hash-a-password </remarks>
        public static void SetUserPermissions(this HttpResponseBase response, IEnumerable<string> permissions)
        {
            var salt = new byte[16];

            var str = "";
            foreach (var permission in permissions)
            {
                var r = new Rfc2898DeriveBytes(permission, salt, 10000);
                var hash = r.GetBytes(32);
                str += Convert.ToBase64String(hash);
            }

            var permissionCookie = new HttpCookie("permissions") { Value = str, Expires = DateTime.Now.AddDays(30) };
            response.Cookies.Set(permissionCookie);
        }

        public static void SetAuth(this HttpResponseBase response, string email)
        {
            var authCookie = new HttpCookie("auth") { Value = email, Expires = DateTime.Now.AddDays(30) };
            response.Cookies.Set(authCookie);
        }
    }
}
