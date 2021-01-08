using System;
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
        public string SummeryInDetail { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string ImageUrl { get; set; }
        public int CommentCount { get; set; }
        public List<PageListViewModel> SidebarPages { get; set; }
        public List<CommentListItems> Comments { get; set; }
        public string UrlParameter { get; set; }
        public string DateModified { get; set; }
        public List<DetailFaqsViewModel> Faqs { get; set; }
        public string FaqTitle { get; set; }
        public bool HasFaq { get; set; }
    }
    public class CommentListItems
    {

        public CommentItem ParentCommnets { get; set; }
        public string Response { get; set; }
        public string ResponseDate { get; set; }
    }

    public class CommentItem
    {
        public string FullName { get; set; }
        public string CreationDateStr { get; set; }
        public string Body { get; set; }
        public Guid Id { get; set; }
    }
}