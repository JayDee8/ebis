using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ebis.Models;

namespace ebis.Controllers
{
    public class ContactController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View(db.kontakty.ToList());
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id = 0)
        {
            kontakty kontakty = db.kontakty.Single(k => k.pk_id == id);
            if (kontakty == null)
            {
                return HttpNotFound();
            }
            return View(kontakty);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(kontakty kontakty)
        {
            if (ModelState.IsValid)
            {
                db.kontakty.AddObject(kontakty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kontakty);
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(int id = 0)
        {
            kontakty kontakty = db.kontakty.Single(k => k.pk_id == id);
            if (kontakty == null)
            {
                return HttpNotFound();
            }
            return View(kontakty);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(kontakty kontakty)
        {
            if (ModelState.IsValid)
            {
                db.kontakty.Attach(kontakty);
                db.ObjectStateManager.ChangeObjectState(kontakty, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kontakty);
        }

        //
        // GET: /Contact/Delete/5

        public ActionResult Delete(int id = 0)
        {
            kontakty kontakty = db.kontakty.Single(k => k.pk_id == id);
            if (kontakty == null)
            {
                return HttpNotFound();
            }
            return View(kontakty);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            kontakty kontakty = db.kontakty.Single(k => k.pk_id == id);
            db.kontakty.DeleteObject(kontakty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}