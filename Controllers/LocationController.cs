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
    [Authorize]
    public class LocationController : Controller
    {
        private dbLocation db = new dbLocation();

        //
        // GET: /Location/

        public ActionResult Index()
        {
            return View(db.lokace.ToList());
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(int id = 0)
        {
            lokace lokace = db.lokace.Single(l => l.pk_id == id);
            if (lokace == null)
            {
                return HttpNotFound();
            }
            return View(lokace);
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Location/Create

        [HttpPost]
        public ActionResult Create(lokace lokace)
        {
            if (ModelState.IsValid)
            {
                db.lokace.AddObject(lokace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lokace);
        }

        //
        // GET: /Location/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lokace lokace = db.lokace.Single(l => l.pk_id == id);
            if (lokace == null)
            {
                return HttpNotFound();
            }
            return View(lokace);
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        public ActionResult Edit(lokace lokace)
        {
            if (ModelState.IsValid)
            {
                db.lokace.Attach(lokace);
                db.ObjectStateManager.ChangeObjectState(lokace, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lokace);
        }

        //
        // GET: /Location/Delete/5

        public ActionResult Delete(int id = 0)
        {
            lokace lokace = db.lokace.Single(l => l.pk_id == id);
            if (lokace == null)
            {
                return HttpNotFound();
            }
            return View(lokace);
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            lokace lokace = db.lokace.Single(l => l.pk_id == id);
            db.lokace.DeleteObject(lokace);
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