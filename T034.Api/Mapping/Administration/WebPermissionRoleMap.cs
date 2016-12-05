using FluentNHibernate.Mapping;
using T034.Api.Entity.Administration;

namespace T034.Api.Mapping.Administration
{
    public class WebPermissionRoleMap : ClassMap<WebPermissionRole>
    {
        public WebPermissionRoleMap()
        {
            Id(x => x.Id).Column("WebPermissionRoleId").GeneratedBy.Increment();

            Map(p => p.Name);

            References(p => p.Role).Column("RoleId")
                .Not.LazyLoad();
        }
    }
}
