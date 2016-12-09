using System;

namespace T034.Tools.Attribute
{
    [Obsolete("использовать WebPermissionAttribute")]
    public class RoleAttribute : System.Attribute
    {
        private readonly string _role;

        public RoleAttribute(string role)
        {
            _role = role;
        }

        public string Role
        {
            get { return _role; }
        }
    }
}