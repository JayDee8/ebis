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
    public class AccommodationController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Accommodation/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.akce_id_link = id;
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis");
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno");
            ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno");
            return View();
        }

        //
        // POST: /Accommodation/Create

        [HttpPost]
        public ActionResult Create(ubytovani ubytovani)
        {
            if (ModelState.IsValid)
            {
                db.ubytovani.AddObject(ubytovani);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }

            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis", ubytovani.akce_id);
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno", ubytovani.lokace_id);
            ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", ubytovani.osoby_id);
            return View(ubytovani);
        }

        //
        // GET: /Accommodation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            ubytovani ubytovani = db.ubytovani.Single(u => u.pk_id == id);
            if (ubytovani == null)
            {
                return HttpNotFound();
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis", ubytovani.akce_id);
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno", ubytovani.lokace_id);
            ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", ubytovani.osoby_id);
            return View(ubytovani);
        }

        //
        // POST: /Accommodation/Edit/5

        [HttpPost]
        public ActionResult Edit(ubytovani ubytovani)
        {
            if (ModelState.IsValid)
            {
                db.ubytovani.Attach(ubytovani);
                db.ObjectStateManager.ChangeObjectState(ubytovani, EntityState.Modified);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis", ubytovani.akce_id);
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno", ubytovani.lokace_id);
            ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", ubytovani.osoby_id);
            return View(ubytovani);
        }

        //
        // GET: /Accommodation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            ubytovani ubytovani = db.ubytovani.Single(u => u.pk_id == id);
            if (ubytovani == null)
            {
                return HttpNotFound();
            }
            return View(ubytovani);
        }

        //
        // POST: /Accommodation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ubytovani ubytovani = db.ubytovani.Single(u => u.pk_id == id);
            db.ubytovani.DeleteObject(ubytovani);
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