using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ebis.Models;
using bcpp.Controllers;

namespace ebis.Controllers
{
    public class ScheduleController : BaseController
    {
        private dbEntities db = new dbEntities();

        public ActionResult ScheduleGridPartial(string akceId)
        {
            IEnumerable<fermany> fermany = db.fermany.ToList().Where(i => i.akce_id == Convert.ToInt32(akceId));
            ViewBag.lokace = db.lokace;
            ViewBag.idAkce = akceId;
            return PartialView("_scheduleGrid", fermany.ToList());
        }

        [HttpPost]
        public JsonResult QuickInsert(fermany inFerman)
        {
            int[] result = { 0, -1 };

            fermany f = db.fermany.SingleOrDefault(p => p.pk_id == inFerman.pk_id);
            
            if (f == null)
            {
                db.fermany.AddObject(inFerman);
                db.SaveChanges();
                result[0] = 1;
                result[1] = inFerman.pk_id;
            }
            else
            {
                result[0] = 0;
                result[1] = -1;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuickDelete(fermany inFerman)
        {
            String result = String.Empty;

            fermany f = db.fermany.SingleOrDefault(p => p.pk_id == inFerman.pk_id);
            
            if (f != null)
            {
                db.fermany.DeleteObject(f);
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
        public JsonResult QuickUpdate(fermany inFerman)
        {
            String result = String.Empty;
            fermany f = db.fermany.SingleOrDefault(p => p.pk_id == inFerman.pk_id);
            if (f != null)
            {
                db.fermany.Attach(f);

                f.orchestr = inFerman.orchestr;
                f.sbor = inFerman.sbor;
                f.solisti = inFerman.solisti;
                f.lokace_id = inFerman.lokace_id;
                f.cas = inFerman.cas;
                f.datum = inFerman.datum;

                db.ObjectStateManager.ChangeObjectState(f, EntityState.Modified);
                db.SaveChanges();
                result = "1";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result = "0";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        
        
        //
        // GET: /Schedule/Create

        public ActionResult Create(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis");
            ViewBag.lokace_id = new SelectList(db.lokace, "pk_id", "jmeno");
            ViewBag.akce_id_link = id;
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

            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis", fermany.akce_id);
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
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis", fermany.akce_id);
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
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "popis", fermany.akce_id);
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
        /*
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }*/
    }
}