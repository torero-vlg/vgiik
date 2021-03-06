﻿using System.Collections.Generic;

namespace T034.Api.Entity.Administration
{
    public class Role : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual ICollection<WebPermissionRole> WebPermissions { get; set; }
    }
}
