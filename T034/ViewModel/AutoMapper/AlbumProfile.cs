using AutoMapper;
using T034.Api.Entity.Vgiik;

namespace T034.ViewModel.AutoMapper
{
    public class AlbumProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Album, AlbumViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person == null ? (int?)null : src.Person.Id))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person == null ? "" : src.Person.FullName))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department == null ? (int?)null : src.Department.Id))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department == null ? "" : src.Department.Name));

            Mapper.CreateMap<AlbumViewModel, Album>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.PersonId.HasValue ? new Person { Id = src.PersonId.Value} : null))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentId.HasValue ? new Department { Id = src.DepartmentId.Value} : null))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => "/Content/images/" + 
                    (src.DepartmentId.HasValue ? 
                        "department/" + src.DepartmentId.Value.ToString() : 
                        "people/" + src.PersonId.Value.ToString()) 
                    + "/" + src.Path + "/"));
        }
    }
}