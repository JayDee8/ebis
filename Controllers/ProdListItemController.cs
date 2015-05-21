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
    public class ProdListItemController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /ProdListItem/Create

        public ActionResult Create(int id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            TempData["ev_id"] = id.ToString();
            ViewBag.akce_id_link = id;
            ViewBag.produkcni_listy_id = new SelectList(db.produkcni_listy, "pk_id", "jmeno_aktivity");
            return View();
        }

        //
        // POST: /ProdListItem/Create

        [HttpPost]
        public ActionResult Create(akce_produkcni_listy akce_produkcni_listy)
        {
            if (ModelState.IsValid)
            {
                akce_produkcni_listy.akce_id = Convert.ToInt32(TempData["ev_id"].ToString());
                db.akce_produkcni_listy.AddObject(akce_produkcni_listy);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.produkcni_listy_id = new SelectList(db.produkcni_listy, "pk_id", "jmeno_aktivity", akce_produkcni_listy.produkcni_listy_id);
            return View(akce_produkcni_listy);
        }

        //
        // GET: /ProdListItem/Edit/5

        public ActionResult Edit(int akce_id = 0, int prod_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            akce_produkcni_listy akce_produkcni_listy = db.akce_produkcni_listy.Single(a => a.akce_id == akce_id && a.produkcni_listy_id == prod_id);
            if (akce_produkcni_listy == null)
            {
                return HttpNotFound();
            }
            ViewBag.produkcni_listy_id = new SelectList(db.produkcni_listy, "pk_id", "jmeno_aktivity", akce_produkcni_listy.produkcni_listy_id);
            return View(akce_produkcni_listy);
        }

        //
        // POST: /ProdListItem/Edit/5

        [HttpPost]
        public ActionResult Edit(akce_produkcni_listy akce_produkcni_listy)
        {
            if (ModelState.IsValid)
            {
                db.akce_produkcni_listy.Attach(akce_produkcni_listy);
                db.ObjectStateManager.ChangeObjectState(akce_produkcni_listy, EntityState.Modified);
                db.SaveChanges();
                return Redirect(TempData["referrer"].ToString());
            }
            ViewBag.produkcni_listy_id = new SelectList(db.produkcni_listy, "pk_id", "jmeno_aktivity", akce_produkcni_listy.produkcni_listy_id);
            return View(akce_produkcni_listy);
        }

        //
        // GET: /ProdListItem/Delete/5

        public ActionResult Delete(int akce_id = 0, int prod_id = 0)
        {
            TempData["referrer"] = Request.UrlReferrer.AbsoluteUri.ToString();
            akce_produkcni_listy akce_produkcni_listy = db.akce_produkcni_listy.Single(a => a.akce_id == akce_id && a.produkcni_listy_id == prod_id);
            if (akce_produkcni_listy == null)
            {
                return HttpNotFound();
            }
            return View(akce_produkcni_listy);
        }

        //
        // POST: /ProdListItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int akce_id = 0, int prod_id = 0)
        {
            akce_produkcni_listy akce_produkcni_listy = db.akce_produkcni_listy.Single(a => a.akce_id == akce_id && a.produkcni_listy_id == prod_id);
            db.akce_produkcni_listy.DeleteObject(akce_produkcni_listy);
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