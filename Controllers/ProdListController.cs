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
    public class ProdListController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /ProdList/

        public ActionResult Index()
        {
            return View(db.produkcni_listy.ToList());
        }

        //
        // GET: /ProdList/Details/5

        public ActionResult Details(int id = 0)
        {
            produkcni_listy produkcni_listy = db.produkcni_listy.Single(p => p.pk_id == id);
            if (produkcni_listy == null)
            {
                return HttpNotFound();
            }
            return View(produkcni_listy);
        }

        //
        // GET: /ProdList/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ProdList/Create

        [HttpPost]
        public ActionResult Create(produkcni_listy produkcni_listy)
        {
            if (ModelState.IsValid)
            {
                db.produkcni_listy.AddObject(produkcni_listy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produkcni_listy);
        }

        //
        // GET: /ProdList/Edit/5

        public ActionResult Edit(int id = 0)
        {
            produkcni_listy produkcni_listy = db.produkcni_listy.Single(p => p.pk_id == id);
            if (produkcni_listy == null)
            {
                return HttpNotFound();
            }
            return View(produkcni_listy);
        }

        //
        // POST: /ProdList/Edit/5

        [HttpPost]
        public ActionResult Edit(produkcni_listy produkcni_listy)
        {
            if (ModelState.IsValid)
            {
                db.produkcni_listy.Attach(produkcni_listy);
                db.ObjectStateManager.ChangeObjectState(produkcni_listy, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produkcni_listy);
        }

        //
        // GET: /ProdList/Delete/5

        public ActionResult Delete(int id = 0)
        {
            produkcni_listy produkcni_listy = db.produkcni_listy.Single(p => p.pk_id == id);
            if (produkcni_listy == null)
            {
                return HttpNotFound();
            }
            return View(produkcni_listy);
        }

        //
        // POST: /ProdList/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            produkcni_listy produkcni_listy = db.produkcni_listy.Single(p => p.pk_id == id);
            db.produkcni_listy.DeleteObject(produkcni_listy);
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