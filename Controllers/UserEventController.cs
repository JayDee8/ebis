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
    public class UserEventController : Controller
    {
        private dbEntities db = new dbEntities();

        
        //
        // GET: /UserEvent/Edit/5

        public ActionResult Edit(int akce_id = 0,int osoby_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            
            osoby_akce osoby_akce = db.osoby_akce.Single(o => o.osoby_id == osoby_id && o.akce_id == akce_id);
            if (osoby_akce == null)
            {
                return HttpNotFound();
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", osoby_akce.akce_id);
            ViewBag.nastroje_id = new SelectList(db.nastroje, "pk_id", "jmeno", osoby_akce.nastroje_id);
            ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", osoby_akce.osoby_id);
            return View(osoby_akce);
        }

        //
        // POST: /UserEvent/Edit/5

        [HttpPost]
        public ActionResult Edit(osoby_akce osoby_akce)
        {
            if (ModelState.IsValid)
            {
                db.osoby_akce.Attach(osoby_akce);
                db.ObjectStateManager.ChangeObjectState(osoby_akce, EntityState.Modified);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.akce_id = new SelectList(db.akce, "pk_id", "jmeno", osoby_akce.akce_id);
            ViewBag.nastroje_id = new SelectList(db.nastroje, "pk_id", "jmeno", osoby_akce.nastroje_id);
            ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", osoby_akce.osoby_id);
            return View(osoby_akce);
        }

        //
        // GET: /UserEvent/Delete/5

        public ActionResult Delete(int akce_id = 0, int osoby_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();

            osoby_akce osoby_akce = db.osoby_akce.Single(o => o.osoby_id == osoby_id && o.akce_id == akce_id);
            if (osoby_akce == null)
            {
                return HttpNotFound();
            }
            return View(osoby_akce);
        }

        //
        // POST: /UserEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int akce_id = 0, int osoby_id = 0)
        {
            osoby_akce osoby_akce = db.osoby_akce.Single(o => o.osoby_id == osoby_id && o.akce_id == akce_id);
            db.osoby_akce.DeleteObject(osoby_akce);
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