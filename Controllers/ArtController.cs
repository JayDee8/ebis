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
    public class ArtController : Controller
    {
        private dbArt db = new dbArt();

        //
        // GET: /Art/

        public ActionResult Index()
        {
            return View(db.titul.ToList());
        }

        //
        // GET: /Art/Details/5

        public ActionResult Details(int id = 0)
        {
            titul titul = db.titul.Single(t => t.pk_id == id);
            if (titul == null)
            {
                return HttpNotFound();
            }
            return View(titul);
        }

        //
        // GET: /Art/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Art/Create

        [HttpPost]
        public ActionResult Create(titul titul)
        {
            if (ModelState.IsValid)
            {
                db.titul.AddObject(titul);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(titul);
        }

        //
        // GET: /Art/Edit/5

        public ActionResult Edit(int id = 0)
        {
            titul titul = db.titul.Single(t => t.pk_id == id);
            if (titul == null)
            {
                return HttpNotFound();
            }
            return View(titul);
        }

        //
        // POST: /Art/Edit/5

        [HttpPost]
        public ActionResult Edit(titul titul)
        {
            if (ModelState.IsValid)
            {
                db.titul.Attach(titul);
                db.ObjectStateManager.ChangeObjectState(titul, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(titul);
        }

        //
        // GET: /Art/Delete/5

        public ActionResult Delete(int id = 0)
        {
            titul titul = db.titul.Single(t => t.pk_id == id);
            if (titul == null)
            {
                return HttpNotFound();
            }
            return View(titul);
        }

        //
        // POST: /Art/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            titul titul = db.titul.Single(t => t.pk_id == id);
            db.titul.DeleteObject(titul);
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