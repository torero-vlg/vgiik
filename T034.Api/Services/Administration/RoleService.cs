using System.Collections.Generic;
using AutoMapper;
using Ninject;
using T034.Api.DataAccess;
using T034.Api.Dto;
using T034.Api.Entity.Administration;
using T034.Api.Services.Common;

namespace T034.Api.Services.Administration
{
    public interface IRoleService : IService
    {
        Role Create(RoleDto dto);
        Role Update(UserDto dto);
        IEnumerable<RoleDto> Select();
        RoleDto Get(int id);
    }

    public class RoleService : IRoleService
    {
        [Inject]
        public IBaseDb Db { get; set; }

        public Role Create(RoleDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Role Update(UserDto dto)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<RoleDto> Select()
        {
            var list = new List<RoleDto>();
            var items = Db.Select<Role>();
            list = Mapper.Map(items, list);
            return list;
        }

        public RoleDto Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
