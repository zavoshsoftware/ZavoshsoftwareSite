using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel:_BaseViewModel
    {
        public List<Page> ServicePages { get; set; }
        public List<Page> DetailServicePages { get; set; }
        public List<Page> BlogList { get; set; }
        public List<Portfolio> PortfolioList { get; set; }
    }
}