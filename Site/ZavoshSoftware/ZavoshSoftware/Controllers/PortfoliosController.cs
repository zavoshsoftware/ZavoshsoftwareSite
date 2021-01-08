using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace ZavoshSoftware.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PortfoliosController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Portfolios
        public ActionResult Index()
        {
            var portfolios = db.Portfolios.Include(p => p.PortfolioGroup).Where(p => p.IsDelete == false).OrderByDescending(p => p.CreationDate);
            return View(portfolios.ToList());
        }



        // GET: Portfolios/Create
        public ActionResult Create()
        {
            ViewBag.PortfolioGroupId = new SelectList(db.PortfolioGroups, "Id", "Title");
            return View();
        }

        // POST: Portfolios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Portfolio portfolio, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Portfolio/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    portfolio.ImageUrl = newFilenameUrl;
                }
                #endregion
                portfolio.IsDelete = false;
                portfolio.CreationDate = DateTime.Now;
                portfolio.LastModificationDate = DateTime.Now;
                portfolio.AverageRate = 5;

                portfolio.Id = Guid.NewGuid();
                db.Portfolios.Add(portfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PortfolioGroupId = new SelectList(db.PortfolioGroups, "Id", "Title", portfolio.PortfolioGroupId);
            return View(portfolio);
        }

        // GET: Portfolios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            ViewBag.PortfolioGroupId = new SelectList(db.PortfolioGroups, "Id", "Title", portfolio.PortfolioGroupId);
            return View(portfolio);
        }

        // POST: Portfolios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Portfolio portfolio, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Portfolio/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    portfolio.ImageUrl = newFilenameUrl;
                }
                #endregion
                portfolio.IsDelete = false;
                portfolio.LastModificationDate = DateTime.Now;
                db.Entry(portfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PortfolioGroupId = new SelectList(db.PortfolioGroups, "Id", "Title", portfolio.PortfolioGroupId);
            return View(portfolio);
        }

        // GET: Portfolios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Portfolio portfolio = db.Portfolios.Find(id);
            portfolio.IsDelete = true;
            portfolio.DeleteDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public string MigrateData()
        //{
        //    Guid id=new Guid("1336F99A-4116-4384-9841-D9DF66DFE360");

        //    List<Page> pages = db.Pages.Where(current => current.PageGroupId == id && current.IsDelete == false)
        //        .ToList();

        //    foreach (Page page in pages)
        //    {
        //        Portfolio portfolio = new Portfolio()
        //        {
        //            Id = Guid.NewGuid(),
        //            UrlParameter = page.UrlParameter,
        //            Title = page.Title,
        //            IsActive = true,
        //            Summery = page.Summery,
        //            ImageUrl = page.ImageUrl,
        //            Body = page.Body,
        //            CreationDate = page.CreationDate,
        //            DeleteDate = null,
        //            IsDelete = false,
        //            LastModificationDate = page.LastModificationDate,
        //            MetaDescription = page.MetaDescription,
        //            Notation = page.Notation,
        //            Order = page.Order,
        //            PortfolioGroupId = new Guid("3072CB36-5F0C-446D-9818-3E40C225D060")
        //        };

        //        db.Portfolios.Add(portfolio);
        //    }
        //    db.SaveChanges();

        //    return String.Empty;
        //}


        [Route("Portfolio/{pageGroupParam?}")]
        [AllowAnonymous]
        public ActionResult List(string pageGroupParam)
        {
            MenuData menu = new MenuData();


            List<Portfolio> portfolios;
            string title;
            string body;

            if (pageGroupParam == null)
            {
                portfolios = db.Portfolios.Where(current =>
                    current.IsActive == true && current.IsDelete == false).OrderBy(current => current.Order).ToList();
                body = "";
                title = "نمونه کارها";
            }
            else
            {
                portfolios = db.Portfolios.Where(current =>
                    current.IsActive == true && current.IsDelete == false &&
                    current.PortfolioGroup.UrlParameter == pageGroupParam).OrderBy(current => current.Order).ToList();

                PortfolioGroup portfolioGroup = db.PortfolioGroups.FirstOrDefault(current => current.UrlParameter == pageGroupParam);

                title = portfolioGroup?.Title;
                body = portfolioGroup?.Body;
            }


            PortfolioListViewModel portfolio = new PortfolioListViewModel()
            {
                Portfolio = portfolios,
                PageGroupBody = body,
                PageGroupTitle = title
            };
            ViewBag.Title = title;
            return View(portfolio);
        }
        // GET: Portfolios/Details/5
        [Route("Portfoliodetail/{pageParam?}")]
        [AllowAnonymous]
        public ActionResult Details(string pageParam)
        {
            MenuData menu = new MenuData();
            if (pageParam == null)
            {
                return RedirectToActionPermanent("List");
            }
            Portfolio portfolio = db.Portfolios.FirstOrDefault(current => current.UrlParameter == pageParam);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            string urlAddress = null;

            if (!string.IsNullOrEmpty(portfolio.AddressUrl))
            {
                urlAddress = "http://" + portfolio.AddressUrl;
            }
            PortfolioDetailViewModel pageDetail = new PortfolioDetailViewModel()
            {
                Title = portfolio.Title,
                Body = portfolio.Body,
                Date = portfolio.LastModificationDate.Value.ToShortDateString(),
                ImageUrl = portfolio.ImageUrl,
                CommentCount = db.Comments.Count(current => current.PageId == portfolio.Id && current.IsActive == true && current.IsDelete == false),
                SidebarPages = GetSidebarPages(portfolio.Id, portfolio.PortfolioGroupId),
                UrlParameter = portfolio.UrlParameter,
                PortfolioGroupUrlParameter = portfolio.PortfolioGroup.UrlParameter,
                PortfolioGroupTitle = portfolio.PortfolioGroup.Title,
                UrlAddress = urlAddress,
                Rate = ReturnRate(portfolio),
                DateModified = portfolio.LastModificationDate.ToString()
            };
            ViewBag.id = portfolio.Id;
            ViewBag.Title = portfolio.Title + " - زاوش";
            ViewBag.Description = portfolio.MetaDescription;
            ViewBag.Canonical = "https://zavoshsoftware.com/portfoliodetail/" + pageParam;
            ViewBag.rate = ReturnRate(portfolio);

            ViewBag.RatingCount = ReturnRatingCount(portfolio.Id);

            ViewBag.image = "https://zavoshsoftware.com" + portfolio.ImageUrl;
            ViewBag.creationDate = portfolio.CreationDate;
            return View(pageDetail);
        }


        public int ReturnRatingCount(Guid entityId)
        {
            int ratingCount = db.Stars.Count(current => current.EntityId == entityId);
            if (ratingCount == 0)
                return 1;
            else
                return ratingCount;
        }
        [AllowAnonymous]
        public List<PageListViewModel> GetSidebarPages(Guid currentPageId, Guid? pageGroupId)
        {
            List<PageListViewModel> sidebarPages = new List<PageListViewModel>();

            if (pageGroupId != null)
            {
                List<Portfolio> portfolioList = db.Portfolios
                    .Where(current =>
                        current.IsDelete == false && current.IsActive == true && current.PortfolioGroupId == pageGroupId &&
                        current.Id != currentPageId).OrderBy(current => current.Order).ToList();

                foreach (Portfolio portfolio in portfolioList)
                {
                    sidebarPages.Add(new PageListViewModel()
                    {
                        Title = portfolio.Title,
                        UrlParameter = portfolio.UrlParameter
                    });
                }
            }

            return sidebarPages;
        }

        [AllowAnonymous]
        public string ReturnRate(Portfolio portfolio)
        {
            if (portfolio.AverageRate != null)
                return portfolio.AverageRate.Value.ToString().Replace('/', '.');
            else
                return "5";
        }

    }
}
