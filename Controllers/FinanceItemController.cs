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
    public class FinanceItemController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /FinanceItem/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.akce_id_link = id;
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno");
            ViewBag.naklady_id = new SelectList(db.naklady, "pk_id", "jmeno");
            return View();
        }

        //
        // POST: /FinanceItem/Create

        [HttpPost]
        public ActionResult Create(akce_naklady akce_naklady)
        {
            if (ModelState.IsValid)
            {
                db.akce_naklady.AddObject(akce_naklady);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }

            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", akce_naklady.akce_id);
            ViewBag.naklady_id = new SelectList(db.naklady, "pk_id", "jmeno", akce_naklady.naklady_id);
            return View(akce_naklady);
        }

        //
        // GET: /FinanceItem/Edit/5

        public ActionResult Edit(int akce_id = 0, int naklady_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            akce_naklady akce_naklady = db.akce_naklady.Single(a => a.akce_id == akce_id && a.naklady_id == a.naklady_id);
            if (akce_naklady == null)
            {
                return HttpNotFound();
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", akce_naklady.akce_id);
            ViewBag.naklady_id = new SelectList(db.naklady, "pk_id", "jmeno", akce_naklady.naklady_id);
            return View(akce_naklady);
        }

        //
        // POST: /FinanceItem/Edit/5

        [HttpPost]
        public ActionResult Edit(akce_naklady akce_naklady)
        {
            if (ModelState.IsValid)
            {
                db.akce_naklady.Attach(akce_naklady);
                db.ObjectStateManager.ChangeObjectState(akce_naklady, EntityState.Modified);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", akce_naklady.akce_id);
            ViewBag.naklady_id = new SelectList(db.naklady, "pk_id", "jmeno", akce_naklady.naklady_id);
            return View(akce_naklady);
        }

        //
        // GET: /FinanceItem/Delete/5

        public ActionResult Delete(int akce_id = 0, int naklady_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            akce_naklady akce_naklady = db.akce_naklady.Single(a => a.akce_id == akce_id && a.naklady_id == naklady_id);
            if (akce_naklady == null)
            {
                return HttpNotFound();
            }
            return View(akce_naklady);
        }

        //
        // POST: /FinanceItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int akce_id = 0, int naklady_id = 0)
        {
            akce_naklady akce_naklady = db.akce_naklady.Single(a => a.akce_id == akce_id && a.naklady_id == naklady_id);
            db.akce_naklady.DeleteObject(akce_naklady);
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