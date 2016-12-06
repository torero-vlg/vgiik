using System.Collections.Generic;
using T034.Api.Dto.Common;

namespace T034.Api.Dto
{
    public class RoleDto : AbstractDto<int>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool Selected { get; set; }

        public virtual ICollection<WebPermissionDto> WebPermissions { get; set; }
    }
}
