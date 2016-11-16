using FluentNHibernate.Mapping;
using T034.Api.Entity.Vgiik;

namespace T034.Api.Mapping.Vgiik
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id).Column("DepartmentId").GeneratedBy.Increment();

            Map(p => p.Name);
            Map(p => p.Text);
            Map(p => p.MainPhoto);
            Map(p => p.MainPhotoDescription);

            HasMany(x => x.Albums).KeyColumn("DepartmentId").Not.LazyLoad();

            HasManyToMany(p => p.Staff)
                .Table("PersonDepartment")
                .ParentKeyColumn("DepartmentId")
                .ChildKeyColumn("PersonId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();

            HasManyToMany(p => p.Nodes)
                .Table("DepartmentNode")
                .ParentKeyColumn("DepartmentId")
                .ChildKeyColumn("NodeId")
                .Not.LazyLoad();
                //.Cascade.SaveUpdate();
        }
    }
}
