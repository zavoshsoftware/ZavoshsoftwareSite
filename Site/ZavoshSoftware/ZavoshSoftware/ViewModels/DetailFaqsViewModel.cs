using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class DetailFaqsViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid Id { get; set; }
    }
}