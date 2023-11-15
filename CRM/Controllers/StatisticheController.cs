using CRM.Models;
using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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



        public List<Appuntamenti> getAppuntamentiByData(List<Appuntamenti> Appuntamenti, DateTime dataDal, DateTime dataAl)
        {
            dataDal = dataDal.Date;
            dataAl = dataAl.Date;
            int IdAzienda = Int32.Parse(Session["IdAzienda"].ToString());

            if(Appuntamenti == null)
                return db.Appuntamenti.Where(app => app.Utenti.FkAzienda == IdAzienda &&
                                                    app.Date >= dataDal && 
                                                    app.Date <= dataAl).ToList();

            return Appuntamenti.Where(app => app.Date >= dataDal &&
                                             app.Date <= dataAl).ToList();
        }

        public List<Appuntamenti> getAppuntamentiByUtente(List<Appuntamenti> Appuntamenti, int idUtente)
        {
            int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());

            if (Appuntamenti == null)
                return db.Appuntamenti.Where(app => app.Clienti.FkAzienda == idAzienda &&
                                                    app.FkUtente == idUtente).ToList();

            return Appuntamenti.Where(app => app.FkUtente == idUtente).ToList();
        }

        public List<Appuntamenti> getAppuntamentiByCliente(List<Appuntamenti> Appuntamenti, int idCliente)
        {
            int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());

            if (Appuntamenti == null)
                return db.Appuntamenti.Where(app => app.Clienti.FkAzienda == idAzienda &&
                                                    app.FkCliente == idCliente).ToList();

            return Appuntamenti.Where(app => app.FkCliente == idCliente).ToList();
        }

    }
}
