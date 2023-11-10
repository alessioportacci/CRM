using CRM.Models;
using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class StatisticheController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            int IdAzienda = Int32.Parse(Session["IdAzienda"].ToString());

            //Utenti
            List<SelectListItem> Utenti = new List<SelectListItem>();
            foreach (Utenti utente in db.Utenti.Where(u => u.FkAzienda == IdAzienda).ToList())
                Utenti.Add(new SelectListItem { Text = utente.Nome, Value = utente.Id.ToString() });

            //Clienti
            List<SelectListItem> Clienti = new List<SelectListItem>();
            foreach (Clienti cliente in db.Clienti.Where(c => c.FkAzienda == IdAzienda).ToList())
                Clienti.Add(new SelectListItem { Text = cliente.Nome, Value = cliente.Id.ToString() });

            //Servizi
            List<SelectListItem> Servizi = new List<SelectListItem>();
            foreach (Servizi servizio in db.Servizi.Where(c => c.FkAzienda == IdAzienda).ToList())
                Servizi.Add(new SelectListItem { Text = servizio.Servizio, Value = servizio.Id.ToString() });

            //Tipologie
            List<SelectListItem> Tipologie = new List<SelectListItem>();
            foreach (Servizi tipologia in db.Servizi.Where(c => c.FkAzienda == IdAzienda).ToList())
                Tipologie.Add(new SelectListItem { Text = tipologia.Servizio, Value = tipologia.Id.ToString() });


            return View(new StatisticheModel
                        {
                            Utenti = Utenti,
                            Clienti = Clienti,
                            Servizi = Servizi,
                            Tipologie = Tipologie
                        });
        }

    }
}
