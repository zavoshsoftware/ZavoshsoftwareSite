using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;
using ViewModels;

namespace ZavoshSoftware.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        #region CRUD

        public ActionResult Index()
        {
            var pages = db.Pages.Include(p => p.PageGroup).Where(p => p.IsDelete == false).OrderBy(p => p.Order);
            return View(pages.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.PageGroupId = new SelectList(db.PageGroups.Where(current => current.IsDelete == false), "Id", "Title");

            PagePositionCrudViewModel page = new PagePositionCrudViewModel();

            page.Positions = GetPosition(null);
            return View(page);
        }

        public List<PostionCheckBoxListViewModel> GetPosition(Guid? pageId)
        {
            List<Position> positions = db.Positions.Where(current => current.IsDelete == false).ToList();
            List<PostionCheckBoxListViewModel> pagePositions = new List<PostionCheckBoxListViewModel>();
            if (pageId == null)
            {
                foreach (Position position in positions)
                {
                    pagePositions.Add(new PostionCheckBoxListViewModel()
                    {
                        Id = position.Id,
                        Title = position.Title,
                        Checked = false
                    });
                }
            }
            else
            {
                List<PagePosition> selectedPositions =
                    db.PagePositions.Where(current => current.PageId == pageId).ToList();

                foreach (Position position in positions)
                {
                    bool check = selectedPositions.Any(current => current.PositionId == position.Id);

                    pagePositions.Add(new PostionCheckBoxListViewModel()
                    {
                        Id = position.Id,
                        Title = position.Title,
                        Checked = check
                    });
                }
            }
            return pagePositions;
        }
        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagePositionCrudViewModel pagePosition, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Page/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    pagePosition.Page.ImageUrl = newFilenameUrl;
                }
                #endregion
                pagePosition.Page.IsDelete = false;
                pagePosition.Page.CreationDate = DateTime.Now;
                pagePosition.Page.LastModificationDate = DateTime.Now;

                pagePosition.Page.Id = Guid.NewGuid();
                pagePosition.Page.PageGroupId = pagePosition.PageGroupId;
                pagePosition.Page.AverageRate = 5;
                db.Pages.Add(pagePosition.Page);

                InsertInotPagePosition(pagePosition.Positions, pagePosition.Page.Id);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PageGroupId = new SelectList(db.PageGroups.Where(current => current.IsDelete == false), "Id", "Title", pagePosition.Page.PageGroupId);
            //ViewBag.PositionId = new SelectList(db.Positions.Where(current => current.IsDelete == false), "Id", "Title", page.PositionId);
            pagePosition.Positions = GetPosition(null);
            return View(pagePosition);
        }

        public void InsertInotPagePosition(List<PostionCheckBoxListViewModel> positions, Guid pageId)
        {
            foreach (PostionCheckBoxListViewModel position in positions)
            {
                if (position.Checked)
                {
                    PagePosition pagePosition = new PagePosition()
                    {
                        PageId = pageId,
                        PositionId = position.Id,
                        CreationDate = DateTime.Now,
                        LastModificationDate = DateTime.Now,
                        IsDelete = false,
                    };

                    db.PagePositions.Add(pagePosition);
                }
            }
        }

        public void DeleteCurrentPagePositions(Guid pageId)
        {
            List<PagePosition> pagePositions = db.PagePositions.Where(current => current.PageId == pageId).ToList();

            foreach (PagePosition pagePosition in pagePositions)
            {
                db.PagePositions.Remove(pagePosition);
            }
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageGroupId = new SelectList(db.PageGroups.Where(current => current.IsDelete == false), "Id", "Title", page.PageGroupId);

            PagePositionCrudViewModel pagePosition = new PagePositionCrudViewModel
            {
                Positions = GetPosition(id),
                Page = page,
                PageGroupId = page.PageGroupId.Value
            };

            return View(pagePosition);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PagePositionCrudViewModel pagePosition, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Page/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    pagePosition.Page.ImageUrl = newFilenameUrl;
                }
                #endregion

                pagePosition.Page.IsDelete = false;
                pagePosition.Page.LastModificationDate = DateTime.Now;
                pagePosition.Page.PageGroupId = pagePosition.PageGroupId;

                db.Entry(pagePosition.Page).State = EntityState.Modified;
                DeleteCurrentPagePositions(pagePosition.Page.Id);
                InsertInotPagePosition(pagePosition.Positions, pagePosition.Page.Id);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PageGroupId = new SelectList(db.PageGroups.Where(current => current.IsDelete == false), "Id", "Title", pagePosition.PageGroupId);
            //ViewBag.PageGroupId = new SelectList(db.PageGroups.Where(current => current.IsDelete == false), "Id", "Title", page.PageGroupId);

            return View(pagePosition);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Page page = db.Pages.Find(id);
            page.IsDelete = true;
            page.DeleteDate = DateTime.Now;

            DeleteFromPosition(id);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DeleteFromPosition(Guid pageId)
        {
            List<PagePosition> pagePositions = db.PagePositions.Where(current => current.PageId == pageId).ToList();

            foreach (PagePosition pagePosition in pagePositions)
            {
                db.PagePositions.Remove(pagePosition);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #endregion

        [Route("page/{pageParam}")]
        [AllowAnonymous]
        public ActionResult Details(string pageParam)
        {
            MenuData menu = new MenuData();

            if (pageParam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Page page = db.Pages.FirstOrDefault(current =>
                current.UrlParameter == pageParam && current.IsDelete == false && current.IsActive == true);

            if (page == null)
            {
                Portfolio portfolio = db.Portfolios.FirstOrDefault(current =>
                    current.UrlParameter == pageParam && current.IsDelete == false);

                if (portfolio != null)
                    return RedirectToActionPermanent("Details", "Portfolios", new { @pageParam = pageParam });

                return HttpNotFound();
            }


            PageDetailViewModel pageDetail = new PageDetailViewModel()
            {
                Title = page.Title,
                Body = page.Body,
                Date = page.LastModificationDate.Value.ToShortDateString(),
                ImageUrl = page.ImageUrl,
                CommentCount = db.Comments.Count(current =>
                    current.PageId == page.Id && current.IsActive == true && current.IsDelete == false),
                SidebarPages = GetSidebarPages(page.Id, page.PageGroupId),
                Comments = ReturnComments(page.Id),
                UrlParameter = page.UrlParameter,
                Rate = ReturnRate(page),
                DateModified = page.LastModificationDate.ToString(),
                Faqs = GetPageFaq(page.Id),
                HasFaq = page.HasFaq,
                FaqTitle = page.FaqTitle,
                TitleTag = GetTitleTag(page)
            };

            ViewBag.id = page.Id;

            ViewBag.Description = page.MetaDescription;
            ViewBag.Canonical = "https://zavoshsoftware.com/page/" + page.UrlParameter;

            ViewBag.rate = ReturnRate(page);
            ViewBag.RatingCount = ReturnRatingCount(page.Id);

            ViewBag.image = "https://zavoshsoftware.com" + page.ImageUrl;
            ViewBag.creationDate = page.CreationDate;


            if (page.PageGroup.ParentId == new Guid("30FA953C-403F-4796-B787-528238A48100"))
            {
                ViewBag.ListUrl = "https://zavoshsoftware.com/page";
                ViewBag.ListTitle = "خدمات";
            }
            else
            {
                ViewBag.ListUrl = "https://zavoshsoftware.com/blog";
                ViewBag.ListTitle = "مطالب وبلاگ";
            }
            return View(pageDetail);
        }

        public string GetTitleTag(Page page)
        {
            if (string.IsNullOrEmpty(page.TitleTag))
                return page.Title + " | زاوش";

            return page.TitleTag;
        }

        public List<Faq> GetPageFaq(Guid pageId)
        {
            List<Faq> faqs = db.Faqs.Where(current => current.PageId == pageId && current.IsDelete == false).OrderBy(current => current.Order)
                .ToList();

            return faqs;
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
        public string ReturnRate(Page page)
        {
            if (page.AverageRate != null)
                return page.AverageRate.Value.ToString().Replace('/', '.');
            else
                return "5";
        }
        [AllowAnonymous]
        public List<CommentListItems> ReturnComments(Guid id)
        {
            List<CommentListItems> commnetList = new List<CommentListItems>();
            List<Comment> comments = db.Comments.Where(current => current.PageId == id && current.IsDelete == false && current.IsActive == true && current.ParentId == null).ToList();
            foreach (var item in comments)
            {
                commnetList.Add(new CommentListItems
                {
                    ParentCommnets = item,
                    RespondComments = db.Comments.Where(current => current.IsDelete == false
&& current.IsActive == true && current.ParentId == item.Id).ToList()
                });
            }
            return commnetList;


        }
        [AllowAnonymous]
        public List<PageListViewModel> GetSidebarPages(Guid currentPageId, Guid? pageGroupId)
        {
            List<PageListViewModel> sidebarPages = new List<PageListViewModel>();

            if (pageGroupId != null)
            {
                List<Page> pages = db.Pages
                    .Where(current =>
                        current.IsDelete == false && current.IsActive && current.PageGroupId == pageGroupId &&
                        current.Id != currentPageId).OrderBy(current => current.Order).ToList();

                if (!pages.Any())
                {
                    Guid serviceGuid = new Guid("30FA953C-403F-4796-B787-528238A48100");

                    pages = db.Pages
                        .Where(current =>
                            current.IsDelete == false && current.IsActive == true && current.PageGroup.ParentId == serviceGuid &&
                            current.Id != currentPageId).OrderBy(current => current.Order).ToList();
                }

                foreach (Page page in pages)
                {
                    sidebarPages.Add(new PageListViewModel()
                    {
                        Title = page.Title,
                        UrlParameter = page.UrlParameter
                    });
                }
            }

            return sidebarPages;
        }

        [Route("blog/{pageGroupParam?}")]
        [AllowAnonymous]
        public ActionResult BlogList(string pageGroupParam)
        {
            MenuData menu = new MenuData();

            Guid pageGroupId = new Guid("ECD18815-6452-4A49-805D-A99533EFEE6E");

            List<Page> Pages;
            string title;
            string body;

            if (pageGroupParam == null)
            {
                Pages = db.Pages.Where(current =>
                    current.IsActive == true && current.IsDelete == false &&
                    current.PageGroup.ParentId == pageGroupId).ToList();
                body = "";
                title = "مطالب وبلاگ";
            }
            else
            {
                Pages = db.Pages.Where(current =>
                    current.IsActive == true && current.IsDelete == false &&
                    current.PageGroup.UrlParameter == pageGroupParam).ToList();

                PageGroup pageGroup = db.PageGroups.FirstOrDefault(current => current.UrlParameter == pageGroupParam);

                title = pageGroup.Title;
                body = pageGroup.Body;
            }


            BlogListViewModel portfolio = new BlogListViewModel()
            {
                Portfolio = Pages,
                PageGroupBody = body,
                PageGroupTitle = title
            };
            ViewBag.Title = title;
            return View(portfolio);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostSubmitComment(string name, string email, string body, string id, string parentId)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail)
                return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
            else
            {
                string pageId = id.Replace("%", "");
                Comment comment = new Comment();

                comment.Name = name;
                comment.Email = email;
                comment.Body = body;
                comment.CreationDate = DateTime.Now;
                comment.IsDelete = false;
                comment.Id = Guid.NewGuid();
                comment.PageId = new Guid(pageId);
                comment.IsActive = false;
                comment.CreationDate = DateTime.Now;
                if (parentId != "")
                {
                    comment.ParentId = new Guid(parentId);
                }

                db.Comments.Add(comment);
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult teststar(string rateStar, string id)
        {


            try
            {

                Guid paramId = new Guid(id);

                decimal rate;
                if (!rateStar.Any(char.IsDigit))
                    rate = ReturnRate(rateStar);
                else
                    rate = Convert.ToDecimal(rateStar.Replace('.', '/'));

                Page param = db.Pages.Find(paramId);
                if (param == null)
                {
                    Portfolio portfolio = db.Portfolios.Find(paramId);
                    portfolio.AverageRate = (portfolio.AverageRate + rate) / 2;
                }
                else
                {
                    param.AverageRate = (param.AverageRate + rate) / 2;
                }
                Star star = new Star();
                star.Id = Guid.NewGuid();
                star.EntityId = paramId;
                star.CreationDate = DateTime.Now;
                star.IsDelete = false;
                star.IP = Request.UserHostAddress;
                star.Rate = rate;
                star.Type = 1;



                db.Stars.Add(star);
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);

            }


        }
        //[AllowAnonymous]
        //public ActionResult SubmitStar(string ratestar, string id)
        //{


        //}
        public decimal ReturnRate(string rateStar)
        {
            decimal rate = 0;
            switch (rateStar.ToLower())
            {
                case "one stars":
                    {
                        rate = 1;
                        break;
                    }
                case "two stars":
                    {
                        rate = 2;
                        break;
                    }
                case "three stars":
                    {
                        rate = 3;
                        break;
                    }
                case "four stars":
                    {
                        rate = 4;
                        break;
                    }
                case "five stars":
                    {
                        rate = 5;
                        break;
                    }
                case "one & half star":
                    {
                        rate = Convert.ToDecimal(1.5);
                        break;
                    }
                case "two & half stars":
                    {
                        rate = Convert.ToDecimal(2.5);
                        break;
                    }
                case "three & half stars":
                    {
                        rate = Convert.ToDecimal(3.5);
                        break;
                    }
                case "four & half stars":
                    {
                        rate = Convert.ToDecimal(4.5);
                        break;
                    }
                case "half star":
                    {
                        rate = Convert.ToDecimal(0.5);
                        break;
                    }

            }
            return rate;
        }

        [Route("page")]
        [AllowAnonymous]
        public ActionResult ServiceList(string pageParam)
        {
            MenuData menu = new MenuData();

            Guid pageGroupId = new Guid("ECD18815-6452-4A49-805D-A99533EFEE6E");


            string title;
            string body;


            List<Page> pages = GetPages(new Guid("EDEB818D-E965-4DEF-B855-17E04F40F1A6"));
            body = "شرکت مشاوران سیستم های پیشرفته زاوش از سال 1390 با هدف ارتقا عملکرد سازمان ها از طریقه مکانیزه کردن فرآیند های سازمانی فعالیت خود را آغاز نموده است و هم اکنون در چهار حوزه زیر فعالیت می کند:";
            title = "خدمات زاوش";


            BlogListViewModel portfolio = new BlogListViewModel()
            {
                Portfolio = pages,
                PageGroupBody = body,
                PageGroupTitle = title
            };
            ViewBag.Title = title;
            return View(portfolio);

            //  List<Page> pages = GetPages(new Guid("EDEB818D-E965-4DEF-B855-17E04F40F1A6"));
            return View(pages);
        }

        public List<Page> GetPages(Guid positionId)
        {
            List<Page> servicePages = new List<Page>();
            List<PagePosition> pagePositions =
                db.PagePositions.Where(current => current.PositionId == positionId)
                    .OrderBy(current => current.Page.Order).ToList();

            foreach (PagePosition pagePosition in pagePositions)
            {
                Page page = db.Pages.FirstOrDefault(current => current.IsDelete == false && current.IsActive == true &&
                                                               current.Id == pagePosition.PageId);

                if (page != null)
                    servicePages.Add(page);

            }

            return servicePages;
        }

    }
}
