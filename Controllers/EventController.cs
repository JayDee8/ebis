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
    public class EventController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Event/

        public ActionResult Index()
        {
            var akce = db.akce.Include("titul");
            return View(akce.ToList());
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id = 0)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            if (akce == null)
            {
                return HttpNotFound();
            }
            return View(akce);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "titul1");
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(akce akce)
        {
            if (ModelState.IsValid)
            {
                db.akce.AddObject(akce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "titul1", akce.titul_id);
            return View(akce);
        }

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(int id = 0)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            if (akce == null)
            {
                return HttpNotFound();
            }
            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "titul1", akce.titul_id);
            return View(akce);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(akce akce)
        {
            if (ModelState.IsValid)
            {
                db.akce.Attach(akce);
                db.ObjectStateManager.ChangeObjectState(akce, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "titul1", akce.titul_id);
            return View(akce);
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(int id = 0)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            if (akce == null)
            {
                return HttpNotFound();
            }
            return View(akce);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            db.akce.DeleteObject(akce);
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