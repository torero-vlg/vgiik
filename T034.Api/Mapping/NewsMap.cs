using FluentNHibernate.Mapping;
using T034.Api.Entity;

namespace T034.Api.Mapping
{
    public class NewsMap : ClassMap<News>
    {
        public NewsMap()
        {
            Id(x => x.Id).Column("NewsId").GeneratedBy.Increment();

            Map(p => p.Body);
            Map(p => p.Title);
            Map(p => p.Resume);
            Map(p => p.LogDate);
            References(p => p.User).Column("UserId")
                .Not.LazyLoad();
        }
    }
}
