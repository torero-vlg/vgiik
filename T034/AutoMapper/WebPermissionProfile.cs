using AutoMapper;
using T034.Api.Dto;
using T034.ViewModel;

namespace T034.AutoMapper
{
    public class WebPermissionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<WebPermissionDto, WebPermissionViewModel>()
                .ForMember(vm => vm.Selected, v => v.MapFrom(src => src.Id != 0));
            Mapper.CreateMap<WebPermissionViewModel, WebPermissionDto>();
        }
    }
}