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
            /*InterpretsInstrumentsModel model = new InterpretsInstrumentsModel();

            IEnumerable<osoby> interprets = db.osoby.ToList();
            
            return View(db.osoby.ToList());
            */
            return View();
            //return Json(db.osoby, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult GridPartial()
        {
            
            return PartialView("_refreshInterpretGrid",db.osoby.ToList());
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
            var res = db.osoby.Any() ? db.osoby.Max(o => o.id) : 0;

            int id = res + 1;
            model.Interpret = new osoby();
            model.Interpret.id = id;    
                
            return View(model);
        }

        //
        // POST: /Interpret/Create

        [HttpPost]
        public ActionResult Create(InterpretInstrumentModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedItemIds != null)
                {
                    foreach (var i in model.SelectedItemIds)
                        model.Interpret.nastroje.Add(db.nastroje.Single(n => n.pk_id == i));
                }
                    db.osoby.AddObject(model.Interpret);
                
                db.SaveChanges();
                if (Request.Form["sub_n_stay"] != null)
                    return RedirectToAction("Create");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult QuickInsert(osoby inOsoba)
        {
            String[] result = {String.Empty,String.Empty};

            osoby o = db.osoby.SingleOrDefault(p => p.id == inOsoba.id);
            int id = -1;
            if (o == null)
            {
                var res = db.osoby.Any() ? db.osoby.Max(i => i.id) : 0;

                id = res + 1;
                inOsoba.id = id;
                
                db.osoby.AddObject(inOsoba);
                db.SaveChanges();
                result[0] = "1";
                result[1] = id.ToString();
            }
            else
            {
                result[0] = "0";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuickDelete(osoby inOsoba)
        {
            String result = String.Empty;

            osoby o = db.osoby.SingleOrDefault(p => p.id == inOsoba.id);
            
            if (o != null)
            {
                db.osoby.DeleteObject(o);
                db.SaveChanges();
                result = "1";
            }
            else
                result = "0";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuickUpdate(osoby inOsoba)
        {
            String result = String.Empty;
            osoby o = db.osoby.SingleOrDefault(p => p.id == inOsoba.id);
            if (o != null)
            {               
                db.osoby.Attach(o);

                o.jmeno = inOsoba.jmeno;
                o.prijmeni = inOsoba.prijmeni;
                o.email = inOsoba.email;

                db.ObjectStateManager.ChangeObjectState(o, EntityState.Modified);
                db.SaveChanges();
                result = "1";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result = "0";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Interpret/Edit/5

        public ActionResult Edit(int id = 0)
        {
            osoby osoby = db.osoby.SingleOrDefault(o => o.pk_id == id);
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
                if (model.SelectedItemIds != null)
                {
                    foreach (var i in model.SelectedItemIds)
                        model.Interpret.nastroje.Add(db.nastroje.Single(n => n.pk_id == i));
                }
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