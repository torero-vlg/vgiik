using FluentNHibernate.Mapping;
using T034.Api.Entity.Administration;

namespace T034.Api.Mapping.Administration
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id).Column("RoleId").GeneratedBy.Increment();

            Map(p => p.Name);
            Map(p => p.Code);

            HasMany(x => x.WebPermissions)
                .KeyColumn("RoleId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }
}
