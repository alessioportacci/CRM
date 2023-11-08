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
            //Mi prendo il lunedì corrente => Giorno di oggi - la differenza tra il giorno di oggi e il giorno di lunedì)
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday)).Date;
            DateTime currentWeekEndDate = currentWeekStartDate.AddDays(6);
            //Faccio l'interrogazione per prendermi gli appuntamenti della settimana, così non devo chiamare il db ogni volta
            List<Appuntamenti> Appuntamentidb = db.Appuntamenti.Where(a => a.Date >= currentWeekStartDate && a.Date < currentWeekEndDate).ToList();

            //Mi creo la lista di interi dei 6 giorni della settimana
            List<int> GiorniAppuntamenti = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                DateTime date = currentWeekStartDate.AddDays(i);
                List<Appuntamenti> appuntamenti = Appuntamentidb.Where(a => a.Date == date).ToList();
                if (appuntamenti != null)
                    GiorniAppuntamenti.Add(appuntamenti.Count);
                else
                    GiorniAppuntamenti.Add(0);
            }
            ViewBag.Appuntamenti = GiorniAppuntamenti;

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
                Session["Img"] = Utente.Propic;


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