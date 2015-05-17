using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ebis.Models;
using System.Dynamic;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace ebis.Controllers
{

    public class InterpretController : Controller
    {
        private dbEntities db = new dbEntities();

        //
        // GET: /Interpret/

        public ActionResult Index()
        {
            return View(db.osoby.ToList());
        }

        //
        // GET: /Interpret/Details/5

        public ActionResult Details(int id = 0)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        //
        // GET: /Interpret/Create

        public ActionResult Create()
        {
            InterpretInstrumentModel model = new InterpretInstrumentModel();

            model.AvailableItems = db.nastroje;
            List<int> selIds = new List<int>();
            selIds.Add(-1);
            model.SelectedItemIds = selIds;
                
                
            return View(model);
        }

        //
        // POST: /Interpret/Create

        [HttpPost]
        public ActionResult Create(InterpretInstrumentModel model)
        {
            if (ModelState.IsValid)
            {
                var res = db.osoby.Any()? db.osoby.Max(o=>o.id):0;

                int id = res + 1;
                model.Interpret.id = id;

                if (model.SelectedItemIds != null)
                {
                    foreach (var i in model.SelectedItemIds)
                        model.Interpret.nastroje.Add(db.nastroje.Single(n => n.pk_id == i));
                }
                    db.osoby.AddObject(model.Interpret);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Interpret/Edit/5

        public ActionResult Edit(int id = 0)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            InterpretInstrumentModel model = new InterpretInstrumentModel();
            model.AvailableItems = db.nastroje;
            model.Interpret = osoby;

            List<int> selectedInstruments = new List<int>();

            foreach(var i in osoby.nastroje)
                selectedInstruments.Add(i.pk_id);

            model.SelectedItemIds = selectedInstruments;
            //ViewBag.vn = db.nastroje;
            //ViewBag.nastroje = new MultiSelectList(db.nastroje, "pk_id", "jmeno", osoby.nastroje);
            return View(model);
        }

        //
        // POST: /Interpret/Edit/5

        [HttpPost]
        public ActionResult Edit(InterpretInstrumentModel model)
        {
            if (ModelState.IsValid)
            {
                db.osoby.Attach(model.Interpret);
                
                model.Interpret.nastroje.Clear();

                foreach (var i in model.SelectedItemIds)
                    model.Interpret.nastroje.Add(db.nastroje.Single(n => n.pk_id == i));
                db.ObjectStateManager.ChangeObjectState(model.Interpret, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Interpret/Delete/5

        public ActionResult Delete(int id = 0)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            if (osoby == null)
            {
                return HttpNotFound();
            }
            return View(osoby);
        }

        //
        // POST: /Interpret/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            osoby osoby = db.osoby.Single(o => o.pk_id == id);
            db.osoby.DeleteObject(osoby);
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