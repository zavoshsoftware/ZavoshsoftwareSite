using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class PortfolioDetailViewModel : _BaseViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string ImageUrl { get; set; }
        public int CommentCount { get; set; }
        public List<PageListViewModel> SidebarPages { get; set; }
        public string UrlParameter { get; set; }
        public string PortfolioGroupTitle { get; set; }
        public string PortfolioGroupUrlParameter { get; set; }
        public string UrlAddress { get; set; }
        public string DateModified { get; set; }
    }
  


}