using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace T034.ViewModel
{
    public class NodeViewModel
    {
        /// <summary>
        /// Путь к файлу, url и тд
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// Описание, название для пользователя
        /// </summary>
        public virtual string Description { get; set; }
    }
}