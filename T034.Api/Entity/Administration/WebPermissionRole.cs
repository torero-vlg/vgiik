using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T034.Api.Entity.Administration
{
    /// <summary>
    /// Разрешение, содержащееся в роли
    /// </summary>
    public class WebPermissionRole : Entity
    {
        public virtual string Name { get; set; }
        public virtual Role Role { get; set; }
    }
}
