using System;
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
            return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1,
                /*HttpContext.Current.Request.UserHostAddress*/"encstring",
                DateTime.Now, DateTime.MaxValue, false, content));
        }

        private static string Decrypt(string encryptedContent)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encryptedContent);
            if (!ticket.Expired)
                return ticket.UserData;

            return null;
        }

        public ActionResult Success(SuccessModel s_model)
        {
            return View(s_model);
        }

        public ActionResult Response(string command)
        {
            string str = Decrypt(command);
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
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("xpodsednikm@gmail.com", "Sephael024");
            smtp.EnableSsl = true;

            List<string> rowsUsers = new List<string>(form.GetValues("ucast"));
            List<string> rowsInstruments = new List<string>(form.GetValues("nastroj"));
            List<string> usersIds = new List<string>(form.GetValues("usId"));

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
                oa.poznamka = "poznamka";
                oa.honorar = 0;
                oa.doprava = 0;
                oa.srazkova_dan = 0;
                oa.vyplaceno = 0;
                oa.stav = 0;

                db.osoby_akce.AddObject(oa);
                db.SaveChanges();

                MailMessage mail = new MailMessage();
                mail.To.Add(user.email);
                emails.Add(user.email);
                mail.From = new MailAddress("xpodsednikm@gmail.com");
                mail.Subject = "test";
                string content = oa.akce_id.ToString() + "-" + oa.nastroje_id.ToString() + "-" + oa.osoby_id.ToString() + "-1";
                string Body = "http://localhost:52663/Invite/Response/" + Encrypt(content);
                mail.Body = Body;
                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }


            /*if (ModelState.IsValid)
            {
                
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.em_mail.To);
                mail.From = new MailAddress(_objModelMail.em_mail.From);
                mail.Subject = _objModelMail.em_mail.Subject;
                string Body = _objModelMail.em_mail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("xpodsednikm@gmail.com", "Sephael024");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Details", _objModelMail);
                return null;

            }
            else
            {
                return View();
            }*/
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