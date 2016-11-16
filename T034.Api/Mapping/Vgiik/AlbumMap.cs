using FluentNHibernate.Mapping;
using T034.Api.Entity.Vgiik;

namespace T034.Api.Mapping.Vgiik
{
    public class AlbumMap : ClassMap<Album>
    {
        public AlbumMap()
        {
            Id(x => x.Id).Column("AlbumId").GeneratedBy.Increment();

            Map(p => p.Name);
            Map(p => p.Path);
            References(p => p.Person).Column("PersonId")
                .Not.LazyLoad();
            References(p => p.Department).Column("DepartmentId")
                .Not.LazyLoad();
            References(p => p.Veteran).Column("VeteranId")
                .Not.LazyLoad();

            HasManyToMany(p => p.Nodes)
                .Table("AlbumNode")
                .ParentKeyColumn("AlbumId")
                .ChildKeyColumn("NodeId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }
}
