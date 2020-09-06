﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class PageDetailViewModel:_BaseViewModel
    {
        public string Title { get; set; }
        public string TitleTag { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string ImageUrl { get; set; }
        public int CommentCount { get; set; }
        public List<PageListViewModel> SidebarPages { get; set; }
        public List<CommentListItems> Comments { get; set; }
        public string UrlParameter { get; set; }
        public string DateModified { get; set; }
        public List<Faq> Faqs { get; set; }
        public string FaqTitle { get; set; }
        public bool HasFaq { get; set; }
    }
    public class CommentListItems
    {
        public Comment  ParentCommnets { get; set; }
        public List<Comment> RespondComments { get; set; }
    }


}