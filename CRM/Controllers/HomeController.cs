using CRM.Models;
using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {

        ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,Password")]  Utenti u)
        {
            //Controllo se le credenziali passate corrispondono a qualcosa
            Utenti Utente = db.Utenti.Where(ut => ut.Username == u.Username && ut.Password == u.Password).FirstOrDefault();
            if (Utente != null)
            {
                //Metto nella sesisone l'id (può essere utile) ed aggiorno l'ultimo accesso
                Session["IdUtente"] = Utente.Id;
                Session["IdAzienda"] = Utente.FkAzienda;

                Utente.LastOnline = DateTime.Now;
                db.Entry(Utente).State = EntityState.Modified;
                db.SaveChanges();

                //Imposto il cookie di autenticazione
                FormsAuthentication.SetAuthCookie(Utente.Username, true);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Username o Password non validi";
            return View();
        }
    }
}