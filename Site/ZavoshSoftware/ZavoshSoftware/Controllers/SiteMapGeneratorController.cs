using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;

namespace ZavoshSoftware.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SiteMapGeneratorController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("seo/sitemap")]
        public ActionResult Sitemap(string language)
        {
            Sitemap sm = new Sitemap();

            StaticPageSiteMap(sm);

            ProductsSiteMap(sm);
            PortfolioGroupSiteMap(sm);
            PortfolioSiteMap(sm);
            return new XmlResult(sm);
        }



        public void ProductsSiteMap(Sitemap sm)
        {
            List<Models.Page> pages = db.Pages.Where(current => current.IsDelete == false && current.IsActive == true).ToList();

            foreach (Page page in pages)
            {
                var encoded = HttpUtility.UrlPathEncode("https://zavoshsoftware.com/page/" + page.UrlParameter);

                if (page.PageGroup.ParentId == new Guid("30FA953C-403F-4796-B787-528238A48100"))
                {
                    AddToSiteMap(sm, encoded, 0.9D, Location.eChangeFrequency.daily);
                }
                else
                {
                    AddToSiteMap(sm, encoded, 0.8D, Location.eChangeFrequency.weekly);
                }

            }
        }
        public void PortfolioSiteMap(Sitemap sm)
        {
            List<Models.Portfolio> portfolios = db.Portfolios.Where(current => current.IsDelete == false && current.IsActive == true).ToList();

            foreach (Portfolio portfolio in portfolios)
            {
                var encoded = HttpUtility.UrlPathEncode("https://zavoshsoftware.com/Portfoliodetail/" + portfolio.UrlParameter);
                AddToSiteMap(sm, encoded, 0.7D, Location.eChangeFrequency.monthly);
            }
        }
        public void PortfolioGroupSiteMap(Sitemap sm)
        {
            List<Models.PortfolioGroup> portfolioGroups = db.PortfolioGroups.Where(current => current.IsDelete == false && current.ParentId != null && current.IsActive == true).ToList();

            foreach (PortfolioGroup portfolioGroup in portfolioGroups)
            {
                var encoded = HttpUtility.UrlPathEncode("https://zavoshsoftware.com/portfolio/" + portfolioGroup.UrlParameter);
                AddToSiteMap(sm, encoded, 0.7D, Location.eChangeFrequency.weekly);
            }
        }
        public void StaticPageSiteMap(Sitemap sm)
        {
            AddToSiteMap(sm, "https://zavoshsoftware.com", 0.9D, Location.eChangeFrequency.weekly);

            AddToSiteMap(sm, "https://zavoshsoftware.com/portfolio", 0.7D, Location.eChangeFrequency.weekly);

            AddToSiteMap(sm, "https://zavoshsoftware.com/contact", 0.5D, Location.eChangeFrequency.yearly);
        }

        public void AddToSiteMap(Sitemap sm, string url, double? priority, Location.eChangeFrequency frequency)
        {
            sm.Add(new Location()
            {
                Url = url,
                LastModified = DateTime.UtcNow,
                Priority = priority,
                ChangeFrequency = frequency
            });
        }
    }
}