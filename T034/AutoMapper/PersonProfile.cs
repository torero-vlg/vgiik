using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using T034.Api.Entity.Vgiik;
using T034.ViewModel;

namespace T034.AutoMapper
{
    public class PersonProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Person, PersonViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.Docs, opt => opt.MapFrom(src => new List<CarouselViewModel>()));

            Mapper.CreateMap<PersonViewModel, Person>()
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));

            Mapper.CreateMap<Person, SelectListItem>()
             .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
             .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.FullName));
        }
    }
}