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
    public class ContractController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: /Contract/5

        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var sList = db.smlouvy.Where(s => s.osoby_id == id);
                return View(sList.ToList());
            }
            var smlouvy = db.smlouvy.Include("osoby");//.Include("text_smlouvy");
            return View(smlouvy.ToList());
        }

        //
        // GET: /Contract/Details/5

        public ActionResult Details(int id = 0)
        {
            smlouvy smlouvy = db.smlouvy.Single(s => s.pk_id == id);
            if (smlouvy == null)
            {
                return HttpNotFound();
            }
            return View(smlouvy);
        }

        //
        // GET: /Contract/Create

        public ActionResult Create()
        {
            ViewBag.osoby = db.osoby.OrderBy(i=>i.prijmeni);
            //ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmenoPrijmeniId");
            //ViewBag.text_smlouvy_id = new SelectList(db.text_smlouvy, "pk_id", "text1");
            return View();
        }

        //
        // POST: /Contract/Create

        [HttpPost]
        public ActionResult Create(smlouvy smlouvy)
        {
            if (ModelState.IsValid)
            {
                smlouvy.datum_narozeni = DateTime.Now;
                smlouvy.datum_osoba = DateTime.Now;
                smlouvy.datum_spolecnost = DateTime.Now;
                db.smlouvy.AddObject(smlouvy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.osoby = db.osoby.OrderBy(i => i.prijmeni);
          /*  var osoby = db.osoby
                .Where(s => s.pk_id == smlouvy.osoby_id)
                .ToList()
                .Select(s => new
                {
                    pk_id = s.pk_id,
                    fullName = string.Format("{0} {1}", s.jmeno, s.prijmeni)
                });*/

            //ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", smlouvy.osoby_id);
            //ViewBag.osoby_id = new SelectList(osoby, "pk_id", "fullName");
            //ViewBag.text_smlouvy_id = new SelectList(db.text_smlouvy, "pk_id", "text1", smlouvy.text_smlouvy_id);
            return View(smlouvy);
        }

        //
        // GET: /Contract/Edit/5

        public ActionResult Edit(int id = 0)
        {
            smlouvy smlouvy = db.smlouvy.Single(s => s.pk_id == id);
            if (smlouvy == null)
            {
                return HttpNotFound();
            }
            ViewBag.osoby = db.osoby.OrderBy(i => i.prijmeni);
            ViewBag.osoby_id = smlouvy.osoby_id;//new SelectList(db.osoby, "pk_id", "jmeno", smlouvy.osoby_id);
            //ViewBag.text_smlouvy_id = new SelectList(db.text_smlouvy, "pk_id", "text1", smlouvy.text_smlouvy_id);
            return View(smlouvy);
        }

        //
        // POST: /Contract/Edit/5

        [HttpPost]
        public ActionResult Edit(smlouvy smlouvy)
        {
            if (ModelState.IsValid)
            {
                db.smlouvy.Attach(smlouvy);
                db.ObjectStateManager.ChangeObjectState(smlouvy, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.osoby = db.osoby.OrderBy(i => i.prijmeni);
            ViewBag.osoby_id = smlouvy.osoby_id; //ViewBag.osoby_id = new SelectList(db.osoby, "pk_id", "jmeno", smlouvy.osoby_id);
            //ViewBag.text_smlouvy_id = new SelectList(db.text_smlouvy, "pk_id", "text1", smlouvy.text_smlouvy_id);
            return View(smlouvy);
        }

        //
        // GET: /Contract/Delete/5

        public ActionResult Delete(int id = 0)
        {
            smlouvy smlouvy = db.smlouvy.Single(s => s.pk_id == id);
            if (smlouvy == null)
            {
                return HttpNotFound();
            }
            return View(smlouvy);
        }

        //
        // POST: /Contract/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            smlouvy smlouvy = db.smlouvy.Single(s => s.pk_id == id);
            db.smlouvy.DeleteObject(smlouvy);
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