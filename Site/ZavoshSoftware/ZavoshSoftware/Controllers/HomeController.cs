using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace ZavoshSoftware.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Home
        public ActionResult Index()
        {
            MenuData menu = new MenuData();

            HomeViewModel home = new HomeViewModel()
            {
                ServicePages = GetPages(new Guid("EDEB818D-E965-4DEF-B855-17E04F40F1A6")),

                DetailServicePages = GetPages(new Guid("28B4C30B-F5E4-4C82-A67E-9109E9F28CD1")),

                BlogList = GetPagesByGroup(new Guid("ECD18815-6452-4A49-805D-A99533EFEE6E"))
                    .OrderByDescending(current => current.CreationDate).Take(3).ToList(),

                PortfolioList =
                    db.Portfolios.Where(current => current.IsInHome && current.IsActive && current.IsDelete == false)
                        .Take(3).ToList()
            };
            ViewBag.Canonical = "https://zavoshsoftware.com/";

            return View(home);
        }

        public List<Page> GetPages(Guid positionId)
        {
            List<Page> servicePages = new List<Page>();

            List<PagePosition> pagePositions =
                db.PagePositions.Where(current => current.PositionId == positionId)
                    .OrderBy(current => current.Page.Order).ToList();

            foreach (PagePosition pagePosition in pagePositions)
            {
                Page page = db.Pages.FirstOrDefault(current => current.Id == pagePosition.PageId &&
                current.IsDelete == false && current.IsActive);

                if (page != null)
                    servicePages.Add(page);
            }

            return servicePages;
        }

        public List<Page> GetPagesByGroup(Guid pageGroupId)
        {

            List<Page> pages = db.Pages.Where(current => current.IsDelete == false && current.IsActive == true &&
                                                         current.PageGroup.ParentId == pageGroupId).ToList();

            return pages;
        }



        [Route("contact")]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            MenuData menu = new MenuData();
            ContactViewModel contact = new ContactViewModel()
            {
            };
            return View(contact);
        }

        [Route("pages/submitstar")]
        public ActionResult Redirect1()
        {
            return Redirect("/");
        }

        [Route("contactusforms/submitcontactform")]
        public ActionResult Redirect2()
        {
            return Redirect("/");
        }


    }
}