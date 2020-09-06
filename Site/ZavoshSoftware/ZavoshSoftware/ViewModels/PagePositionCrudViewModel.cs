using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class PagePositionCrudViewModel
    {
        public Page Page { get; set; }
        public List<PostionCheckBoxListViewModel> Positions { get; set; }
        public Guid PageGroupId { get; set; }
    }

    public class PostionCheckBoxListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
    }
}