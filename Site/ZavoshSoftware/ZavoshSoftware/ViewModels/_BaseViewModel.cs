using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;
using Models;

namespace ViewModels
{
    public class _BaseViewModel
    {

        MenuData menu = new MenuData();
        public List<PageListViewModel> MenuServicePages
        {
            get { return menu.GetMenuData(); }
        }

        public List<FooterBlogItem> FooterBlog
        {
            get { return menu.GetFooterData(); }
        }

        public string Rate { get; set; }
    }

    public class FooterBlogItem
    {
        public string Title { get; set; }
        public string UrlParameter { get; set; }
        public string ImageUrl { get; set; }
        public string CreationDateStr { get; set; }
    }
}