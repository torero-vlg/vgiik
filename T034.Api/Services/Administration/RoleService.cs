using System.Collections.Generic;
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
    }
}
