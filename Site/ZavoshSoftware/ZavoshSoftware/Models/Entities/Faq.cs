﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Faq:BaseEntity
    {
        public int Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid PageId { get; set; }
        public virtual Page Page { get; set; }
    }
}