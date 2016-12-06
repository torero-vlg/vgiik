using AutoMapper;
using T034.Api.Dto;

namespace T034.ViewModel.AutoMapper
{
    public class WebPermissionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<WebPermissionDto, WebPermissionViewModel>()
                .ForMember(vm => vm.Selected, v => v.UseValue(true));
            Mapper.CreateMap<WebPermissionViewModel, WebPermissionDto>();
        }
    }
}