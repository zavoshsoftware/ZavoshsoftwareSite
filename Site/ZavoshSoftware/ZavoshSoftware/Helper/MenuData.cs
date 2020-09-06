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

            List<PagePosition> pagePositions =
                db.PagePositions.Where(current => current.PositionId == serviceMenuPositionId).OrderBy(current => current.Page.Order).ToList();

            foreach (PagePosition pagePosition in pagePositions)
            {
                Page page = db.Pages.FirstOrDefault(current => current.IsDelete == false && current.IsActive == true &&
                        current.Id == pagePosition.PageId);

                if (page != null)
                    sidebarPages.Add(new PageListViewModel()
                    {
                        Title = page.Title,
                        UrlParameter = page.UrlParameter
                    });
            }

            return sidebarPages;
        }


        public List<Page> GetFooterData()
        {
            List<Page> pageList = new List<Page>();

            Guid pageGroupId = new Guid("ECD18815-6452-4A49-805D-A99533EFEE6E");

            pageList = db.Pages.Where(current => current.IsDelete == false && current.IsActive == true &&
                                                 current.PageGroup.ParentId == pageGroupId)
                .OrderByDescending(current => current.CreationDate).Take(3).ToList();

            return pageList;
        }
    }
}