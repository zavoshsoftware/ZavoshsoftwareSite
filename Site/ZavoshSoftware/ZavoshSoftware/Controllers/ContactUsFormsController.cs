﻿using System;
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
    //[Authorize(Roles = "Administrator")]
    public class ContactUsFormsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ContactUsForms
        public ActionResult Index()
        {
            return View(db.ContactUsForms.Where(a => a.IsDelete == false).OrderByDescending(a => a.CreationDate).ToList());
        }

        // GET: ContactUsForms/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

        // GET: ContactUsForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Message,Ip,IsDelete,CreationDate,DeleteDate,LastModificationDate")] ContactUsForm contactUsForm)
        {
            if (ModelState.IsValid)
            {
                contactUsForm.IsDelete = false;
                contactUsForm.CreationDate = DateTime.Now;
                contactUsForm.LastModificationDate = DateTime.Now;

                contactUsForm.Id = Guid.NewGuid();
                db.ContactUsForms.Add(contactUsForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUsForm);
        }

        // GET: ContactUsForms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

        // POST: ContactUsForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Message,Ip,IsDelete,CreationDate,DeleteDate,LastModificationDate")] ContactUsForm contactUsForm)
        {
            if (ModelState.IsValid)
            {
                contactUsForm.IsDelete = false;
                contactUsForm.LastModificationDate = DateTime.Now;
                db.Entry(contactUsForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactUsForm);
        }

        // GET: ContactUsForms/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

        // POST: ContactUsForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            contactUsForm.IsDelete = true;
            contactUsForm.DeleteDate = DateTime.Now;

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
