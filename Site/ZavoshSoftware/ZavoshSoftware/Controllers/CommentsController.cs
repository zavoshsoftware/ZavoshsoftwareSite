using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Models;

namespace ZavoshSoftware.Controllers
{
    public class CommentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Authorize(Roles = "Administrator")]

        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Page).Where(c=>c.IsDelete==false).OrderByDescending(c=>c.CreationDate);
            return View(comments.ToList());
        }
         

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title");
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Body,IsActive,PageId,Response,ResponseDate,IsDelete,CreationDate,DeleteDate,LastModificationDate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
				comment.IsDelete = false;
				comment.CreationDate = DateTime.Now; 
				comment.LastModificationDate = DateTime.Now; 
				
                comment.Id = Guid.NewGuid();
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title", comment.PageId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title", comment.PageId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Body,IsActive,PageId,Response,ResponseDate,IsDelete,CreationDate,DeleteDate,LastModificationDate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
				comment.IsDelete=false;
				comment.LastModificationDate = DateTime.Now; 
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PageId = new SelectList(db.Pages, "Id", "Title", comment.PageId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Comment comment = db.Comments.Find(id);
			comment.IsDelete=true;
			comment.DeleteDate=DateTime.Now;
 
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

        [AllowAnonymous]
        public ActionResult PostComment(string name, string email, string message, string pageId)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail)
            {
                Comment c = new Comment();
                c.Id = Guid.NewGuid();
                c.Email = email;
                c.IsDelete = false;
                c.Body = message;
                c.Name = name;
                c.PageId = new Guid(pageId);
                c.CreationDate = DateTime.Now;

                db.Comments.Add(c);
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("false", JsonRequestBehavior.AllowGet);

        }
    }
}
