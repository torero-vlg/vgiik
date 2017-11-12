using AutoMapper;
using T034.Api.Dto.Vgiik;
using T034.Api.Entity.Vgiik;

namespace T034.Api.AutoMapper.Vgiik
{
    public class PublicationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Publication, PublicationDto>()
                .ForMember(dest => dest.Path,
                       opt => opt.MapFrom(src => $"/Content/images/publication/{src.Id}/"));
            CreateMap<PublicationDto, Publication>();
        }
    }
}
