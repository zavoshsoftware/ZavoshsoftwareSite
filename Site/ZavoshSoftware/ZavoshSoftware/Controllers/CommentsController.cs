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

        // GET: Comments
        public ActionResult Index(Guid? id)
        {
            List<Comment> comments = new List<Comment>();
            if (id==null)
             comments = db.Comments.Include(c => c.Page).Where(c=>c.IsDelete==false).OrderByDescending(c=>c.CreationDate).Include(c => c.Parent).Where(c=>c.IsDelete==false).OrderByDescending(c=>c.CreationDate).ToList();
            else
                comments = db.Comments.Include(c => c.Page).Where(c => c.IsDelete == false && c.Id==id).OrderByDescending(c => c.CreationDate).Include(c => c.Parent).Where(c => c.IsDelete == false).OrderByDescending(c => c.CreationDate).ToList();
            return View(comments.ToList());
        }

        public ActionResult Confirm(Guid? id)
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
            else
            {
                comment.IsActive = true;
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        public ActionResult NotConfirm(Guid? id)
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
            else
            {
                comment.IsActive = false;
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

     
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
        public ActionResult PostComment(string name, string email, string message,string pageId)
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
