using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace ZavoshSoftware.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FaqsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var faqs = db.Faqs.Include(f => f.Page).Where(f=>f.IsDelete==false).OrderByDescending(f=>f.CreationDate);
            return View(faqs.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order,Question,Answer,PageId,IsDelete,CreationDate,DeleteDate,LastModificationDate")] Faq faq)
        {
            if (ModelState.IsValid)
            {
				faq.IsDelete = false;
				faq.CreationDate = DateTime.Now; 
				faq.LastModificationDate = DateTime.Now; 
				
                faq.Id = Guid.NewGuid();
                db.Faqs.Add(faq);

                //faq.Page.HasFaq = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title", faq.PageId);
            return View(faq);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title", faq.PageId);
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order,Question,Answer,PageId,IsDelete,CreationDate,DeleteDate,LastModificationDate")] Faq faq)
        {
            if (ModelState.IsValid)
            {
				faq.IsDelete=false;
				faq.LastModificationDate = DateTime.Now; 
                db.Entry(faq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title", faq.PageId);
            return View(faq);
        }

        // GET: Faqs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Faq faq = db.Faqs.Find(id);
			faq.IsDelete=true;
			faq.DeleteDate=DateTime.Now;
 
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
