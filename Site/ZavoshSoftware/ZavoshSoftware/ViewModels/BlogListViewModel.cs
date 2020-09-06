using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BlogListViewModel:_BaseViewModel
    {
        public List<Page> Portfolio { get; set; }

        public string PageGroupTitle { get; set; }
        public string PageGroupBody { get; set; }
    }

   
}