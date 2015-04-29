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
    public class InstrumentController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Instrument/

        public ActionResult Index()
        {
            return View(db.nastroje.ToList());
        }

        //
        // GET: /Instrument/Details/5

        public ActionResult Details(int id = 0)
        {
            nastroje nastroje = db.nastroje.Single(n => n.pk_id == id);
            if (nastroje == null)
            {
                return HttpNotFound();
            }
            return View(nastroje);
        }

        //
        // GET: /Instrument/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Instrument/Create

        [HttpPost]
        public ActionResult Create(nastroje nastroje)
        {
            if (ModelState.IsValid)
            {
                db.nastroje.AddObject(nastroje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nastroje);
        }

        //
        // GET: /Instrument/Edit/5

        public ActionResult Edit(int id = 0)
        {
            nastroje nastroje = db.nastroje.Single(n => n.pk_id == id);
            if (nastroje == null)
            {
                return HttpNotFound();
            }
            return View(nastroje);
        }

        //
        // POST: /Instrument/Edit/5

        [HttpPost]
        public ActionResult Edit(nastroje nastroje)
        {
            if (ModelState.IsValid)
            {
                db.nastroje.Attach(nastroje);
                db.ObjectStateManager.ChangeObjectState(nastroje, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nastroje);
        }

        //
        // GET: /Instrument/Delete/5

        public ActionResult Delete(int id = 0)
        {
            nastroje nastroje = db.nastroje.Single(n => n.pk_id == id);
            if (nastroje == null)
            {
                return HttpNotFound();
            }
            return View(nastroje);
        }

        //
        // POST: /Instrument/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            nastroje nastroje = db.nastroje.Single(n => n.pk_id == id);
            db.nastroje.DeleteObject(nastroje);
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