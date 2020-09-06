using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class PortfolioListViewModel:_BaseViewModel
    {
        public List<Portfolio> Portfolio { get; set; }

        public string PageGroupTitle { get; set; }
        public string PageGroupBody { get; set; }
    }

   
}