using T034.Api.Dto.Common;

namespace T034.Api.Dto
{
    public class PersonDto : AbstractDto<int>
    {
        public string Name { get; set; }
    }
}
