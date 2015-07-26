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

        [HttpPost]
        public JsonResult QuickInsert(akce_naklady inFinance)
        {
            String result = String.Empty;

            akce_naklady f = db.akce_naklady.SingleOrDefault(p => p.naklady_id == inFinance.naklady_id && p.akce_id == inFinance.akce_id);

            if (f == null)
            {
                db.akce_naklady.AddObject(inFinance);
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
        public JsonResult QuickDelete(int akce_id = 0, int naklady_id = 0)
        {
            String result = String.Empty;

            akce_naklady naklady = db.akce_naklady.SingleOrDefault(a => a.akce_id == akce_id && a.naklady_id == naklady_id);

            if (naklady != null)
            {
                db.akce_naklady.DeleteObject(naklady);
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
        public JsonResult QuickUpdate(akce_naklady inFinance)
        {
            String result = String.Empty;
            akce_naklady f = db.akce_naklady.SingleOrDefault(p => p.naklady_id == inFinance.naklady_id && p.akce_id == inFinance.akce_id);

            if (f != null)
            {
                db.akce_naklady.Attach(f);

                f.cena = inFinance.cena;

                db.ObjectStateManager.ChangeObjectState(f, EntityState.Modified);
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