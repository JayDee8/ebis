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
    public class FinanceController : Controller
    {
        private dbFinance db = new dbFinance();

        //
        // GET: /Finance/

        public ActionResult Index()
        {
            return View(db.naklady.ToList());
        }

        //
        // GET: /Finance/Details/5

        public ActionResult Details(int id = 0)
        {
            naklady naklady = db.naklady.Single(n => n.pk_id == id);
            if (naklady == null)
            {
                return HttpNotFound();
            }
            return View(naklady);
        }

        //
        // GET: /Finance/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Finance/Create

        [HttpPost]
        public ActionResult Create(naklady naklady)
        {
            if (ModelState.IsValid)
            {
                db.naklady.AddObject(naklady);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(naklady);
        }

        //
        // GET: /Finance/Edit/5

        public ActionResult Edit(int id = 0)
        {
            naklady naklady = db.naklady.Single(n => n.pk_id == id);
            if (naklady == null)
            {
                return HttpNotFound();
            }
            return View(naklady);
        }

        //
        // POST: /Finance/Edit/5

        [HttpPost]
        public ActionResult Edit(naklady naklady)
        {
            if (ModelState.IsValid)
            {
                db.naklady.Attach(naklady);
                db.ObjectStateManager.ChangeObjectState(naklady, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(naklady);
        }

        //
        // GET: /Finance/Delete/5

        public ActionResult Delete(int id = 0)
        {
            naklady naklady = db.naklady.Single(n => n.pk_id == id);
            if (naklady == null)
            {
                return HttpNotFound();
            }
            return View(naklady);
        }

        //
        // POST: /Finance/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            naklady naklady = db.naklady.Single(n => n.pk_id == id);
            db.naklady.DeleteObject(naklady);
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