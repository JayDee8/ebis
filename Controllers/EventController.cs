using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ebis.Models;
using System.Dynamic;
using System.Net.Mail;

namespace ebis.Controllers
{
    public class EventController : Controller
    {
        private dbEntities db = new dbEntities();

        private static string Encrypt(string content)
        {
            /*return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, "f",
                DateTime.Now, DateTime.MaxValue, false, content));*/
            return content;
        }
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

        public ActionResult SendMails(int id = 0)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // mail.eb-is.cz
            smtp.Port = 587; // 465 587 25
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("ebis.office@gmail.com", "awsedr1526"); // office@eb-is.cz 1a2b3c4d
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            List<string> emails = new List<string>();

            var oa_list = db.osoby_akce.Where(o => o.akce_id == id && o.stav == 0);
            foreach (osoby_akce oa in oa_list)
            {
                osoby user = db.osoby.Single(o => o.pk_id == oa.osoby_id);

                MailMessage mail = new MailMessage();
                mail.To.Add(user.email);
                emails.Add(user.email);
                mail.From = new MailAddress("ebis.office@gmail.com"); // office@eb-is.cz
                mail.Subject = "Pozvánka";
                string content = oa.akce_id.ToString() + "-" + oa.nastroje_id.ToString() + "-" + oa.osoby_id.ToString(); // localhost:52663
                var cislo_smlouvy = oa.osoby.smlouvy.Any() ? oa.osoby.smlouvy.Last().id : 0;
                string nazev_akce = oa.akce.popis;
                string Body = "<h1>NABÍDKA a POTVRZENÍ ÚČASTI NA PROJEKTU na základě smlouvy č." + cislo_smlouvy.ToString() + "</h1><br>" +
                    "Já, " + oa.osoby.jmeno + " " + oa.osoby.prijmeni + ", narozen: " + oa.osoby.datum_narozeni.ToString() + "<br>" +
                    "Potvrzuji tímto svoji účast na projektu:<br>" +
                    nazev_akce + "<br>" +
                    "<ul>" +
                    "<li>Termín vystoupení: " + oa.akce.datum + ", " + oa.akce.cas + "</li>" +
                    "<li>Program: " + oa.akce.program + "</li>" +
                    "<li>Výše honoráře: " + oa.honorar.ToString() + " Kč plus náklady na dopravu, které se zavazuji doplnit do 7 dnů od přijetí této nabídky, jinak na proplacení mohu ztratit nárok." + "</li>" +
                    "<li>Funkce: " + oa.nastroje.jmeno + "</li>" +
                    "<li>Obsazení projektu: ..." + "</li>" +
                    "<li>Zvláštní podmínky: ..." + "</li>" +
                    "<li>Poznámka: ..." + "</li>" +
                    "<li>Předběžný harmonogram projektu:</li>" +
                    "</ul>" +
                    "<div>" +

                    "</div>" +
                    "<p>Beru na vědomí, že předpokládaný ferman se může vlivem nepředvídatelných událostí změnit a že na základě své Smlouvy nebudu v uvedeném termínu plánovat žádné další aktivity, které by mi neumožnili zúčastnit se zkoušek a vystoupení.</p>" +
                    "Prosíme, zareagujte včas na naši nabídku a obratem zašlete svou odpověď, zda máte, nebo nemáte o projekt zájem. V případě, že se můžete projektu v celém rozsahu zúčastnit, klikněte na zelené tlačítko a pokračujte dle instrukcí na webové stránce, na kterou budete přesměrováni. " +
                    "V případě, že se projektu můžete zúčastnit a máte jen drobnou časovou kolizi (max. 1 zkušební frekvence), klikněte na modré tlačítko a pokračujte dle instrukcí na webové stránce, na kterou budete přesměrováni. Máte-li větší časové kolize, či nemáte-li o projekt zájem, klikněte " +
                    "na oranžové tlačítko tlačítko. Formulář se obratem odešle zpět, stane se součásti Vaší roční smlouvy č." + cislo_smlouvy.ToString() + ", a nabývá tak právní hodnoty podepsané listiny." +
                    "<div><a href=\"http://www.eb-is.cz/Invite/Response/?command=" + Encrypt(content + "-1") + "\"><img src=\"http://www.eb-is.cz/Images/accept.png\"></a>" +
                    "<a href=\"http://www.eb-is.cz/Invite/Response/?command=" + Encrypt(content + "-2") + "\"><img src=\"http://www.eb-is.cz/Images/decline.png\"></a>" +
                    "<a href=\"http://www.eb-is.cz/Invite/Response/?command=" + Encrypt(content + "-3") + "\"><img src=\"http://www.eb-is.cz/Images/accept2.png\"></a></div>";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }
            SuccessModel s_model = new SuccessModel();
            s_model.id = id;
            s_model.emails = emails;
            return RedirectToAction("Success", "Invite", s_model);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            ViewBag.tituly = db.titul;
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
            ViewBag.tituly = db.titul;
            //ViewBag.titul_id = new SelectList(db.titul, "pk_id", "nazev", akce.titul_id);
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
            ViewBag.tituly = db.titul;
            ViewBag.titul_id = akce.titul_id;
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
            ViewBag.tituly = db.titul;
            ViewBag.titul_id = akce.titul_id;
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