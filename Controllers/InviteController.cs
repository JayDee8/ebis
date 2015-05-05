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

        //
        // GET: /Invite/

        public ActionResult Index()
        {
            InviteModel invModel = new InviteModel();

            invModel.m_osoby = db.osoby.ToList();
            return View(invModel);
        }

        [HttpPost]
        public ViewResult Index(FormCollection form)
        {
            var ids = form.GetValues("ids");
            if (ModelState.IsValid)
            {
                
                /*MailMessage mail = new MailMessage();
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
                return View("Details", _objModelMail);*/
                return null;

            }
            else
            {
                return View();
            }
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