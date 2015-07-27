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

        public ActionResult FinanceGridPartial(string akceId)
        {
            var naklady = db.akce_naklady.Where(i => i.akce_id == Convert.ToInt32(akceId));
            
            return PartialView("_financeGrid", naklady.ToList());
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
        // GET: /FinanceItem/Create

        public ActionResult Create(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            TempData["ev_id"] = id.ToString();
            ViewBag.akce_id_link = id;
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
                akce_naklady.akce_id = Convert.ToInt32(TempData["ev_id"].ToString());
                db.akce_naklady.AddObject(akce_naklady);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.naklady_id = new SelectList(db.naklady, "pk_id", "jmeno");
            return View(akce_naklady);
        }

        //
        // GET: /FinanceItem/Edit/5

        public ActionResult Edit(int akce_id = 0, int naklady_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            akce_naklady akce_naklady = db.akce_naklady.Single(a => a.akce_id == akce_id && a.naklady_id == naklady_id);
            if (akce_naklady == null)
            {
                return HttpNotFound();
            }
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