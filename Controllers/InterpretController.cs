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
    public class InterpretController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Interpret/

        public ActionResult Index()
        {
            return View(db.osoby.ToList());
        }

        //
        // GET: /Interpret/Details/5

        public ActionResult Details(int id = 0)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        //
        // GET: /Interpret/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Interpret/Create

        [HttpPost]
        public ActionResult Create(osoby osoby)
        {
            if (ModelState.IsValid)
            {
                db.osoby.AddObject(osoby);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoby);
        }

        //
        // GET: /Interpret/Edit/5

        public ActionResult Edit(int id = 0)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        //
        // POST: /Interpret/Edit/5

        [HttpPost]
        public ActionResult Edit(osoby osoby)
        {
            if (ModelState.IsValid)
            {
                db.osoby.Attach(osoby);
                db.ObjectStateManager.ChangeObjectState(osoby, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoby);
        }

        //
        // GET: /Interpret/Delete/5

        public ActionResult Delete(int id = 0)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        //
        // POST: /Interpret/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            db.osoby.DeleteObject(osoby);
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