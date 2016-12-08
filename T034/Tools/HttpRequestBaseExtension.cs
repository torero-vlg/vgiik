using System.Web;
using T034.ViewModel;

namespace T034.Tools
{
    public static class HttpRequestBaseExtension
    {
        public static string UserPermissions(this HttpRequestBase request)
        {
            return request.Cookies["permissions"]?.Value;
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
