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
        public List<PageItemViewModel> BlogList { get; set; }
        public List<PortfolioItemViewModel> PortfolioList { get; set; }
    }

    public class PortfolioItemViewModel
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string UrlParameter { get; set; }
    }
    public class PageItemViewModel
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string UrlParameter { get; set; }
        public string Summery { get; set; }
        public DateTime CreationDate { get; set; }
    }
}