using FluentNHibernate.Mapping;
using T034.Api.Entity.Vgiik;

namespace T034.Api.Mapping.Vgiik
{
    public class VeteranMap : ClassMap<Veteran>
    {
        public VeteranMap()
        {
            Id(x => x.Id).Column("VeteranId").GeneratedBy.Increment();

            Map(p => p.FullName);
            Map(p => p.Title);
            Map(p => p.Text);
            HasMany(x => x.Albums).KeyColumn("VeteranId").Not.LazyLoad();
        }
    }
}
