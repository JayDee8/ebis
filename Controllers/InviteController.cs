﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ebis.Models;
using System.Web.Security;
using System.Net.Mail;

namespace ebis.Controllers
{
    public class InviteController : Controller
    {
        private dbEntities db = new dbEntities();

        private static string Encrypt(string content)
        {
            /*return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, "f",
                DateTime.Now, DateTime.MaxValue, false, content));*/
            return content;
        }

        private static string Decrypt(string encryptedContent)
        {
            /*FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encryptedContent);
            if (!ticket.Expired)
                return ticket.UserData;

            return null;*/
            return encryptedContent;
        }

        public ActionResult Success(SuccessModel s_model)
        {
            return View(s_model);
        }

        public ActionResult Response(string command)
        {
            string str = Decrypt(command);
            if (str != null)
            {
                TempData["command"] = str;
                string[] args;
                args = str.Split(new string[] {"-"}, StringSplitOptions.None);
                int akce_id = Convert.ToInt32(args[0]);
                int nastroje_id = Convert.ToInt32(args[1]);
                int osoby_id = Convert.ToInt32(args[2]);
                osoby_akce oa = db.osoby_akce.Single(a => a.akce_id == akce_id &&
                    a.nastroje_id == nastroje_id && a.osoby_id == osoby_id);
                if (oa == null)
                {
                    return HttpNotFound();
                }
                oa.stav = Convert.ToInt32(args[3]);
                db.SaveChanges();
                ViewBag.Response = str.Substring(str.Length - 1, 1);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Response(FormCollection form)
        {
            ViewBag.Response = "9";
            List<string> resp_type = new List<string>(form.GetValues("resp_type"));
            List<string> resp_text = new List<string>(form.GetValues("resp_text"));
            string[] args;
            args = TempData["command"].ToString().Split(new string[] { "-" }, StringSplitOptions.None);
            int akce_id = Convert.ToInt32(args[0]);
            int nastroje_id = Convert.ToInt32(args[1]);
            int osoby_id = Convert.ToInt32(args[2]);
            osoby_akce oa = db.osoby_akce.Single(a => a.akce_id == akce_id &&
                a.nastroje_id == nastroje_id && a.osoby_id == osoby_id);
            if (oa == null)
            {
                return HttpNotFound();
            }
            oa.stav = Convert.ToInt32(args[3]);
            if (resp_type.ElementAt(0) == "1")
            {
                oa.doprava = Convert.ToInt32(resp_text.ElementAt(0));
            }
            else
            if (resp_type.ElementAt(0) == "3")
            {
                oa.poznamka = resp_text.ElementAt(0);
            }
            db.SaveChanges();
            return View();
        }

        //
        // GET: /Invite/

        public ActionResult Index(int id)
        {
            InviteModel invModel = new InviteModel();

            var usersWithInst = db.osoby.Where(i => i.nastroje.Count != 0).ToList();
            
            List<osoby> usersWithoutEv = new List<osoby>();
            foreach (osoby o in usersWithInst)
            {
                if (o.osoby_akce.SingleOrDefault(t => t.osoby_id == o.pk_id && t.akce_id == id) == null)
                    usersWithoutEv.Add(o); 
            }


            invModel.m_osoby = usersWithoutEv;

            invModel.event_id = id;
            //invModel.m_nastroje = db.nastroje.Where(n => n.osoby.Any(o => o.pk_id == 1));
            //invModel.m_nastroje = db.nastroje.SingleOrDefault(x => x.pk_id == pk_id));
            
            //var osoby_nastroje = db.osoby.Include("nastroje").Where(i=>i.nastroje.Count != 0);

            List<List<nastroje>> nastroje_arr = new List<List<nastroje>>();
            foreach(var osoba_i in usersWithoutEv)
            {
                List<nastroje> na_item = new List<nastroje>();
                foreach (var nastroj_i in osoba_i.nastroje)
                {
                    na_item.Add(nastroj_i);
                }
                nastroje_arr.Add(na_item);
            }
            invModel.m_nastroje = nastroje_arr;
            ViewBag.akce_id_link = id;
            return View(invModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // mail.eb-is.cz
            smtp.Port = 25; // 465 587
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("ebis.office@gmail.com", "awsedr1526"); // office@eb-is.cz 1a2b3c4d
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            List<string> rowsUsers = new List<string>(form.GetValues("ucast"));
            List<string> rowsInstruments = new List<string>(form.GetValues("nastroj"));
            List<string> usersIds = new List<string>(form.GetValues("usId"));
            List<string> rowsPayments = new List<string>(form.GetValues("honorar"));

            List<osoby> users = new List<osoby>();
            List<nastroje> instruments = new List<nastroje>();
            List<string> emails = new List<string>();
            foreach (string row in rowsUsers)
            {
                int wgRowId = Convert.ToInt32(row) + 1;
                int instrId = Convert.ToInt32(rowsInstruments.ElementAt(wgRowId));
                int userId = Convert.ToInt32(usersIds.ElementAt(wgRowId));

                nastroje instrument = db.nastroje.Single(n => n.pk_id == instrId);
                osoby user = db.osoby.Single(o => o.id == userId);

                instruments.Add(instrument);
                users.Add(user);

                osoby_akce oa = new osoby_akce();
                oa.akce_id = Convert.ToInt32(form.GetValue("eventId").AttemptedValue);
                oa.nastroje_id = instrument.pk_id;
                oa.osoby_id = user.pk_id;
                oa.poznamka = "";
                oa.honorar = Convert.ToInt32(rowsPayments.ElementAt(wgRowId));
                oa.doprava = 0;
                oa.srazkova_dan = 0;
                oa.vyplaceno = 0;
                oa.stav = 0;

                db.osoby_akce.AddObject(oa);
                db.SaveChanges();

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
            s_model.id = Convert.ToInt32(form.GetValue("eventId").AttemptedValue);
            s_model.emails = emails;
            return View("Success", s_model);
        }

        //
        // GET: /Invite/Details/5

        public ActionResult Details(string d_req)
        {
            string req = Decrypt(d_req);
            int state = Convert.ToInt32(req.Substring(req.Length - 1, 1));
            int id = Convert.ToInt32(req.Substring(0, req.Length - 1));
            osoby_akce osoby_akce = db.osoby_akce.Single(o => o.osoby_id == id);
            if (osoby_akce == null)
            {
                return HttpNotFound();
            }
            osoby_akce.stav = id;
            db.SaveChanges();
            return View();
            /*return View(osoby_akce);*/
        }

        //
        // GET: /Invite/Delete/5

        public ActionResult Delete(int id = 0)
        {
            osoby_akce osoby_akce = db.osoby_akce.Single(o => o.osoby_id == id);
            if (osoby_akce == null)
            {
                return HttpNotFound();
            }
            return View(osoby_akce);
        }

        //
        // POST: /Invite/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            osoby_akce osoby_akce = db.osoby_akce.Single(o => o.osoby_id == id);
            db.osoby_akce.DeleteObject(osoby_akce);
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