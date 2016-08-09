using Db.Entity.Vgiik;
using FluentNHibernate.Mapping;

namespace Db.Mapping.Vgiik
{
    public class VeteranMap : ClassMap<Veteran>
    {
        public VeteranMap()
        {
            Id(x => x.Id).Column("VeteranId").GeneratedBy.Increment();

            Map(p => p.FullName);
            Map(p => p.Title);
            HasMany(x => x.Albums).KeyColumn("VeteranId").Not.LazyLoad();
        }
    }
}
