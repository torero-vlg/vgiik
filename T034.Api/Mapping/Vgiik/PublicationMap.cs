using FluentNHibernate.Mapping;
using T034.Api.Entity.Vgiik;

namespace T034.Api.Mapping.Vgiik
{
    public class PublicationMap : ClassMap<Publication>
    {
        public PublicationMap()
        {
            Id(x => x.Id).Column("PublicationId").GeneratedBy.Increment();
        }
    }
}
