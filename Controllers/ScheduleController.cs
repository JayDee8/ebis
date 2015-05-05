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
    public class ScheduleController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Schedule/Create

        public ActionResult Create()
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno");
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno");
            return View();
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(fermany fermany)
        {
            if (ModelState.IsValid)
            {
                db.fermany.AddObject(fermany);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }

            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", fermany.akce_id);
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno", fermany.lokace_id);
            return View(fermany);
        }

        //
        // GET: /Schedule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            fermany fermany = db.fermany.Single(f => f.pk_id == id);
            if (fermany == null)
            {
                return HttpNotFound();
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", fermany.akce_id);
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno", fermany.lokace_id);
            return View(fermany);
        }

        //
        // POST: /Schedule/Edit/5

        [HttpPost]
        public ActionResult Edit(fermany fermany)
        {
            if (ModelState.IsValid)
            {
                db.fermany.Attach(fermany);
                db.ObjectStateManager.ChangeObjectState(fermany, EntityState.Modified);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", fermany.akce_id);
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno", fermany.lokace_id);
            return View(fermany);
        }

        //
        // GET: /Schedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            fermany fermany = db.fermany.Single(f => f.pk_id == id);
            if (fermany == null)
            {
                return HttpNotFound();
            }
            return View(fermany);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            fermany fermany = db.fermany.Single(f => f.pk_id == id);
            db.fermany.DeleteObject(fermany);
            db.SaveChanges();
            return Redirect(TempData["referrer"].ToString());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}