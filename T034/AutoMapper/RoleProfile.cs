using AutoMapper;
using T034.Api.Dto;
using T034.ViewModel;

namespace T034.AutoMapper
{
    public class RoleProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<RoleDto, RoleViewModel>()
                .ForMember(vm => vm.Selected, v => v.UseValue(true));
            Mapper.CreateMap<RoleViewModel, RoleDto>();
        }
    }
}