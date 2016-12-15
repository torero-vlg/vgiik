using FluentNHibernate.Mapping;
using T034.Api.Entity;

namespace T034.Api.Mapping
{
    public class FolderMap : ClassMap<Folder>
    {
        public FolderMap()
        {
            Id(x => x.Id).Column("FolderId").GeneratedBy.Increment();

            Map(p => p.Name);
            Map(p => p.LogDate);

            References(p => p.User).Column("UserId")
                .Not.LazyLoad();
            References(p => p.ParentFolder).Column("ParentFolderId")
                .Not.LazyLoad();
        }
    }
}
