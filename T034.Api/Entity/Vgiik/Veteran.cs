﻿using System.Collections.Generic;

namespace T034.Api.Entity.Vgiik
{
    public class Veteran : Entity
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Звание, титул
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Список альбомов
        /// </summary>
        public virtual IList<Album> Albums { get; set; }
    }
}
