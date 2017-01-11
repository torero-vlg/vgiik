using FluentNHibernate.Mapping;
using T034.Api.Entity.Vgiik;

namespace T034.Api.Mapping.Vgiik
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id).Column("PersonId").GeneratedBy.Increment();

            Map(p => p.FullName);
            Map(p => p.Title);
            HasMany(x => x.Albums).KeyColumn("PersonId").Not.LazyLoad();
            References(p => p.FilesFolder).Column("FolderId")
                .Nullable()
                .Not.LazyLoad();
        }
    }
}
