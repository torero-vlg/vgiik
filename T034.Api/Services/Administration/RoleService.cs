using System.Collections.Generic;
using AutoMapper;
using T034.Api.Dto;
using T034.Api.Entity.Administration;
using T034.Api.Services.Common;

namespace T034.Api.Services.Administration
{
    public interface IRoleService : IService
    {
        Role Create(RoleDto dto);
        Role Update(RoleDto dto);
        IEnumerable<RoleDto> Select();
        RoleDto Get(object id);
        OperationResult Delete(object id);
    }

    public class RoleService : AbstractRepository<Role, RoleDto, int>, IRoleService
    {
        public new Role Update(RoleDto dto)
        {
            var item = Db.Get<Role>((object)dto.Id);

            foreach (var wp in item.WebPermissions)
                Db.Delete(wp);

            item.WebPermissions.Clear();

            item = Mapper.Map(dto, item);

            var result = Db.SaveOrUpdate(item);

            return item;
        }
    }
}
