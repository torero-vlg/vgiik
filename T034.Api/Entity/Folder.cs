using System;
using T034.Api.Entity.Administration;

namespace T034.Api.Entity
{
    public class Folder : Entity
    {
        public virtual string Name { get; set; }
        public virtual Folder ParentFolder { get; set; }
        
        public virtual DateTime LogDate { get; set; }
        public virtual User User { get; set; }
        public virtual string Path { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
