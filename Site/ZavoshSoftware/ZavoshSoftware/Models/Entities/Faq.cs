﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Faq:BaseEntity
    {
        public int Order { get; set; }
        [DataType(DataType.MultilineText)]
        public string Question { get; set; }
        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }
        public Guid PageId { get; set; }
        public virtual Page Page { get; set; }
    }
}