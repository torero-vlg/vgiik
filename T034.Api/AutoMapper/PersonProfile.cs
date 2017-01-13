using AutoMapper;
using T034.Api.Dto;
using T034.Api.Entity.Vgiik;

namespace T034.Api.AutoMapper
{
    public class PersonProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Person, PersonDto>();
        }
    }
}