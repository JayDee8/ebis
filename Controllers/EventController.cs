using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ebis.Models;
using System.Dynamic;

namespace ebis.Controllers
{
    public class EventController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Event/

        public ActionResult Index()
        {
            var akce = db.akce.Include("titul");
            return View(akce.ToList());
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id = 0)
        {
            EventModel akceModel = new EventModel();
            akceModel.em_akce = db.akce.Single(a => a.pk_id == id);
            akceModel.em_fermany = db.fermany.Include("akce").ToList();
            akceModel.em_osoby_akce0 = db.osoby_akce.Include("nastroje").Where(i => i.nastroje.typ == 0).Where(a => a.akce_id == id).ToList();
            akceModel.em_osoby_akce1 = db.osoby_akce.Include("nastroje").Where(i => i.nastroje.typ == 1).Where(a => a.akce_id == id).ToList();
            akceModel.em_osoby_akce2 = db.osoby_akce.Include("nastroje").Where(i => i.nastroje.typ == 2).Where(a => a.akce_id == id).ToList();
            akceModel.em_akce_produkcni_listy0 = db.akce_produkcni_listy.Include("akce").Include("produkcni_listy").Where(i => i.produkcni_listy.typ == 0).ToList();
            akceModel.em_akce_produkcni_listy1 = db.akce_produkcni_listy.Include("akce").Include("produkcni_listy").Where(i => i.produkcni_listy.typ == 1).ToList();
            akceModel.em_akce_produkcni_listy2 = db.akce_produkcni_listy.Include("akce").Include("produkcni_listy").Where(i => i.produkcni_listy.typ == 2).ToList();
            akceModel.em_akce_naklady0 = db.akce_naklady.Include("akce").Include("naklady").Where(i => i.naklady.typ == 0).ToList();
            akceModel.em_akce_naklady1 = db.akce_naklady.Include("akce").Include("naklady").Where(i => i.naklady.typ == 1).ToList();
            akceModel.em_akce_naklady2 = db.akce_naklady.Include("akce").Include("naklady").Where(i => i.naklady.typ == 2).ToList();
            akceModel.em_akce_naklady3 = db.akce_naklady.Include("akce").Include("naklady").Where(i => i.naklady.typ == 3).ToList();
            akceModel.em_akce_naklady4 = db.akce_naklady.Include("akce").Include("naklady").Where(i => i.naklady.typ == 4).ToList();
            akceModel.em_akce_naklady5 = db.akce_naklady.Include("akce").Include("naklady").Where(i => i.naklady.typ == 5).ToList();
            akceModel.id = id;
            return View(akceModel);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "nazev");
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(akce akce)
        {
            if (ModelState.IsValid)
            {
                db.akce.AddObject(akce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "nazev", akce.titul_id);
            return View(akce);
        }

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(int id = 0)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            if (akce == null)
            {
                return HttpNotFound();
            }
            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "nazev", akce.titul_id);
            return View(akce);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(akce akce)
        {
            if (ModelState.IsValid)
            {
                db.akce.Attach(akce);
                db.ObjectStateManager.ChangeObjectState(akce, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.titul_id = new SelectList(db.titul, "pk_id", "nazev", akce.titul_id);
            return View(akce);
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(int id = 0)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            if (akce == null)
            {
                return HttpNotFound();
            }
            return View(akce);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            akce akce = db.akce.Single(a => a.pk_id == id);
            db.akce.DeleteObject(akce);
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