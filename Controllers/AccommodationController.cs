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
        
        public ActionResult AccommodationGridPartial(string akceId)
        {
            IEnumerable<ubytovani> ubytovani = db.ubytovani.ToList().Where(i => i.akce_id == Convert.ToInt32(akceId));
            ViewBag.lokace = db.lokace;
            ViewBag.osoby = db.osoby;
            ViewBag.idAkce = akceId;
            return PartialView("_accommodationGrid", ubytovani.ToList());
        }

        [HttpPost]
        public JsonResult QuickInsert(ubytovani inUbytovani)
        {
            int[] result = { 0, -1 };

            ubytovani u = db.ubytovani.SingleOrDefault(p => p.pk_id == inUbytovani.pk_id);

            if (u == null)
            {
                db.ubytovani.AddObject(inUbytovani);
                db.SaveChanges();
                result[0] = 1;
                result[1] = inUbytovani.pk_id;
            }
            else
            {
                result[0] = 0;
                result[1] = -1;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuickDelete(ubytovani inUbytovani)
        {
            String result = String.Empty;

            ubytovani u = db.ubytovani.SingleOrDefault(p => p.pk_id == inUbytovani.pk_id);

            if (u != null)
            {
                db.ubytovani.DeleteObject(u);
                db.SaveChanges();
                result = "1";
            }
            else
            {
                result = "0";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuickUpdate(ubytovani inUbytovani)
        {
            String result = String.Empty;
            ubytovani u = db.ubytovani.SingleOrDefault(p => p.pk_id == inUbytovani.pk_id);
            if (u != null)
            {
                db.ubytovani.Attach(u);

                u.osoby_id = inUbytovani.osoby_id;
                u.lokace_id = inUbytovani.lokace_id;
                u.pokoj = inUbytovani.pokoj;
                u.cena1 = inUbytovani.cena1;
                u.cena2 = inUbytovani.cena2;
                u.cena3 = inUbytovani.cena3;
                
                db.ObjectStateManager.ChangeObjectState(u, EntityState.Modified);
                db.SaveChanges();
                result = "1";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result = "0";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

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