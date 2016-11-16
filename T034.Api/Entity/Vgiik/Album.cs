using System.Collections.Generic;

namespace T034.Api.Entity.Vgiik
{
    public class Album : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Путь
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// Человек, которому принадлежит альбом
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Отдел, которому принадлежит альбом
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// Ветеран, которому принадлежит альбом
        /// </summary>
        public virtual Veteran Veteran { get; set; }

        public virtual IList<Node> Nodes { get; set; }
    }
}
