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
    public class PageGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        #region CRUD
     
        public ActionResult Index()
        {
            var pageGroups = db.PageGroups.Include(p => p.Parent).Where(p=>p.IsDelete==false).OrderBy(p => p.Order);
            return View(pageGroups.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = db.PageGroups.Find(id);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return View(pageGroup);
        }

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.PageGroups, "Id", "Title");
            return View();
        }

        // POST: PageGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageGroup pageGroup, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/PageGroup/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    pageGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                pageGroup.IsDelete = false;
				pageGroup.CreationDate = DateTime.Now; 
				pageGroup.LastModificationDate = DateTime.Now; 
				
                pageGroup.Id = Guid.NewGuid();
                db.PageGroups.Add(pageGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.PageGroups, "Id", "Title", pageGroup.ParentId);
            return View(pageGroup);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = db.PageGroups.Find(id);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.PageGroups, "Id", "Title", pageGroup.ParentId);
            return View(pageGroup);
        }

        // POST: PageGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PageGroup pageGroup, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/PageGroup/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    pageGroup.ImageUrl = newFilenameUrl;
                }
                #endregion

                pageGroup.IsDelete=false;
				  pageGroup.LastModificationDate = DateTime.Now; 
                db.Entry(pageGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.PageGroups, "Id", "Title", pageGroup.ParentId);
            return View(pageGroup);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = db.PageGroups.Find(id);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return View(pageGroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PageGroup pageGroup = db.PageGroups.Find(id);
			pageGroup.IsDelete=true;
			pageGroup.DeleteDate=DateTime.Now;
 
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


        #endregion

        //[Route("category/{pageGroupParam?}")]
        //[AllowAnonymous]
        //public ActionResult List(string pageGroupParam)
        //{
        //    var pageGroups = db.PageGroups.Include(p => p.Parent).Where(p => p.IsDelete == false).OrderBy(p => p.Order);
        //    return View(pageGroups.ToList());
        //}

    }
}
