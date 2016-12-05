using T034.Api.Dto.Common;

namespace T034.Api.Dto
{
    public class WebPermissionDto : AbstractDto<int>
    {
        public string Name { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }

        public string RoleId { get; set; }

        public string Role { get; set; }
    }
}
