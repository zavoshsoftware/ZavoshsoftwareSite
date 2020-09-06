using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class _BaseViewModel
    {
        public List<PageListViewModel> MenuServicePages { get; set; }
        public List<Page> FooterBlog { get; set; }
        public string Rate { get; set; }
    }
}