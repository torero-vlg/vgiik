﻿using System;
using System.ComponentModel;
using T034.Api.Entity.Administration;

namespace T034.Api.Entity
{
    public class News : Entity
    {
        [DisplayName("Содержание")]
        public virtual string Body { get; set; }
        
        [DisplayName("Заголовок")]
        public virtual string Title { get; set; }
        
        [DisplayName("Краткое описание")]
        public virtual string Resume { get; set; }

        public virtual DateTime LogDate { get; set; }
        public virtual User User { get; set; }
    }
}
