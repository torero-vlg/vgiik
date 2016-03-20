using System.Collections.Generic;
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
        }
    }
}