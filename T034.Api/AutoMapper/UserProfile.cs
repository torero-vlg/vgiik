﻿using System.Linq;
using AutoMapper;
using T034.Api.Dto;
using T034.Api.Entity.Administration;

namespace T034.Api.AutoMapper
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
                  //.ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => AutoMapperWebConfiguration.IdsToString(src.UserRoles)));

            Mapper.CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserRoles,
                       opt => opt.MapFrom(src => src.UserRoles.Where(ur => ur.Selected)))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore());
        }
    }
}