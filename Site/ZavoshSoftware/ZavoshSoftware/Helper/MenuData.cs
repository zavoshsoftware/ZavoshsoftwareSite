using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using ViewModels;

namespace Helper
{
    public class MenuData
    {
        DatabaseContext db = new DatabaseContext();

        public List<PageListViewModel> GetMenuData()
        {
            List<PageListViewModel> pageList = new List<PageListViewModel>();

            pageList = GetServicePages();

            return pageList;
        }

        public List<PageListViewModel> GetServicePages()
        {
            List<PageListViewModel> sidebarPages = new List<PageListViewModel>();
            Guid serviceMenuPositionId = new Guid("A06BA1F6-69F3-424B-B629-69AC5EEC99A8");

           var pagePositions =
                db.PagePositions.Where(current => current.PositionId == serviceMenuPositionId).OrderBy(current => current.Page.Order).Select(c=>new{ c.PageId}).ToList();

            foreach (var pagePosition in pagePositions)
            {
                var page = db.Pages.Where(current =>
                        current.Id == pagePosition.PageId && current.IsDelete == false && current.IsActive)
                    .Select(c => new {c.Title, c.UrlParameter}).FirstOrDefault();


                if (page != null)
                    sidebarPages.Add(new PageListViewModel()
                    {
                        Title = page.Title,
                        UrlParameter = page.UrlParameter
                    });
            }

            return sidebarPages;
        }


        public List<FooterBlogItem> GetFooterData()
        {
            Guid pageGroupId = new Guid("ECD18815-6452-4A49-805D-A99533EFEE6E");

            var pageList = db.Pages.Where(current =>
                    current.PageGroup.ParentId == pageGroupId && current.IsDelete == false && current.IsActive)
                .OrderByDescending(current => current.CreationDate)
                .Select(c => new {c.Title, c.UrlParameter, c.ImageUrl, c.CreationDate}).Take(3).ToList();

            List<FooterBlogItem> footerBlogItems= new List<FooterBlogItem>();

            foreach (var page in pageList)
            {
                footerBlogItems.Add(new FooterBlogItem()
                {
                    ImageUrl = page.ImageUrl,
                    CreationDateStr = page.CreationDate.ToShortDateString(),
                    UrlParameter = page.UrlParameter,
                    Title = page.Title
                });
            }

            return footerBlogItems;
        }
    }
}