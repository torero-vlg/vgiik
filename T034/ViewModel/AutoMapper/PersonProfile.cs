using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity.Vgiik;

namespace T034.ViewModel.AutoMapper
{
    public class PersonProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Person, PersonViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Docs, opt => opt.MapFrom(src => new List<CarouselViewModel>()));

            Mapper.CreateMap<PersonViewModel, Person>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));

            Mapper.CreateMap<Person, SelectListItem>()
             .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
             .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.FullName));
        }
    }
}