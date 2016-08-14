using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity.Vgiik;

namespace T034.ViewModel.AutoMapper
{
    public class VeteranProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Veteran, PersonViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => HttpUtility.HtmlDecode(src.Text)))
                .ForMember(dest => dest.Docs, opt => opt.MapFrom(src => new List<CarouselViewModel>()));

            Mapper.CreateMap<PersonViewModel, Veteran>()
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));

            Mapper.CreateMap<Veteran, SelectListItem>()
             .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
             .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.FullName));
        }
    }
}