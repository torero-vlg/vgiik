using System;

namespace T034.Models
{
    [Obsolete("использовать WebPermission")]
    public class ActionRole
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Role { get; set; }
    }
}