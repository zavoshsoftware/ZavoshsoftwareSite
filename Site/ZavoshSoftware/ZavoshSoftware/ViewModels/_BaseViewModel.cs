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

        public List<Page> FooterBlog
        {
            get { return menu.GetFooterData(); }
        }

        public string Rate { get; set; }
    }
}