﻿using System.Web;
using AutoMapper;

namespace T034.ViewModel.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure(HttpServerUtility server)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DepartmentProfile(server));
                cfg.AddProfile(new PersonProfile());
                cfg.AddProfile(new MenuItemProfile());
            });
        }
    }
}