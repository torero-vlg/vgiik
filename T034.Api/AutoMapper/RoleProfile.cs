using AutoMapper;
using T034.Api.Dto;
using T034.Api.Entity.Administration;

namespace T034.Api.AutoMapper
{
    public class RoleProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Role, RoleDto>()
                .ForMember(vm => vm.Selected, v => v.UseValue(true));
            Mapper.CreateMap<RoleDto, Role>();
        }
    }
}