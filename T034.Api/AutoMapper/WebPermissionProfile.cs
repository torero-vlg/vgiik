using AutoMapper;
using T034.Api.Dto;
using T034.Api.Entity.Administration;

namespace T034.Api.AutoMapper
{
    public class WebPermissionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<WebPermissionRole, WebPermissionDto>()
                .ForMember(vm => vm.Role, v => v.MapFrom(wp => wp.Role.Name))
                .ForMember(vm => vm.RoleId, v => v.MapFrom(wp => wp.Role.Id));

            Mapper.CreateMap<WebPermissionDto, WebPermissionRole>()
                .ForMember(vm => vm.Role, v => v.MapFrom(wp => new Role { Id = wp.RoleId }));
        }
    }
}