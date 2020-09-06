using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace ZavoshSoftware.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PortfolioGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PortfolioGroups
        public ActionResult Index()
        {
            var portfolioGroups = db.PortfolioGroups.Include(p => p.Parent).Where(p => p.IsDelete == false).OrderByDescending(p => p.CreationDate);
            return View(portfolioGroups.ToList());
        }

        // GET: PortfolioGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioGroup portfolioGroup = db.PortfolioGroups.Find(id);
            if (portfolioGroup == null)
            {
                return HttpNotFound();
            }
            return View(portfolioGroup);
        }

        // GET: PortfolioGroups/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.PortfolioGroups, "Id", "Title");
            return View();
        }

        // POST: PortfolioGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PortfolioGroup portfolioGroup, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/PortfolioGroup/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    portfolioGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                portfolioGroup.IsDelete = false;
                portfolioGroup.CreationDate = DateTime.Now;
                portfolioGroup.LastModificationDate = DateTime.Now;

                portfolioGroup.Id = Guid.NewGuid();
                db.PortfolioGroups.Add(portfolioGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.PortfolioGroups, "Id", "Title", portfolioGroup.ParentId);
            return View(portfolioGroup);
        }

        // GET: PortfolioGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioGroup portfolioGroup = db.PortfolioGroups.Find(id);
            if (portfolioGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.PortfolioGroups, "Id", "Title", portfolioGroup.ParentId);
            return View(portfolioGroup);
        }

        // POST: PortfolioGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PortfolioGroup portfolioGroup, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/PortfolioGroup/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    portfolioGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                portfolioGroup.IsDelete = false;
                portfolioGroup.LastModificationDate = DateTime.Now;
                db.Entry(portfolioGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.PortfolioGroups, "Id", "Title", portfolioGroup.ParentId);
            return View(portfolioGroup);
        }

        // GET: PortfolioGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioGroup portfolioGroup = db.PortfolioGroups.Find(id);
            if (portfolioGroup == null)
            {
                return HttpNotFound();
            }
            return View(portfolioGroup);
        }

        // POST: PortfolioGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PortfolioGroup portfolioGroup = db.PortfolioGroups.Find(id);
            portfolioGroup.IsDelete = true;
            portfolioGroup.DeleteDate = DateTime.Now;

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
    }
}
