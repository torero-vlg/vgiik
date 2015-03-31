using Db.Entity.Vgiik;
using FluentNHibernate.Mapping;

namespace Db.Mapping.Vgiik
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id).Column("DepartmentId").GeneratedBy.Increment();

            Map(p => p.Name);
            Map(p => p.Text);
            Map(p => p.MainPhoto);
            HasMany(x => x.Albums).KeyColumn("PersonId").Not.LazyLoad();

            HasManyToMany(p => p.Staff)
                .Table("PersonDepartment")
                .ParentKeyColumn("DepartmentId")
                .ChildKeyColumn("PersonId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }
}
