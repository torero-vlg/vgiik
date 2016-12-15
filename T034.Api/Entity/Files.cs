using System;
using T034.Api.Entity.Administration;

namespace T034.Api.Entity
{
    public class Files : Entity
    {
        public virtual string Name { get; set; }
        public virtual int DownloadCounter { get; set; }
        
        public virtual Folder Folder { get; set; }
        
        public virtual DateTime LogDate { get; set; }
        public virtual int Size { get; set; }
        public virtual User User { get; set; }
    }
}
