using AutoMapper;

namespace T034.ViewModel.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DepartmentProfile());
            });
        }
    }
}