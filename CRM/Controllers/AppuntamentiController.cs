using CRM.Models;
using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace CRM.Controllers
{
    [Authorize]
    public class AppuntamentiController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        #region CALENDARIO

        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult LoadCalendar()
        /* Metodo per caricare gli appuntamenti del calendario alla sua apertura */
        {
            //Mi prendo la lista degli appuntamenti disponibili per l'utente loggato
            int idUtente = Int32.Parse(Session["IdUtente"].ToString());
            List<Appuntamenti> AppuntamentiDisponibili = db.Appuntamenti
                                        .Where(a => a.FkUtente == idUtente
                                                 || a.VisibilitaGlobale).ToList();

            //Li carico dentro "appuntamenti" chiamando la funzione LoadAppuntamentiCalendario
            List<AppuntamentiCalendarioModel> appuntamenti = new List<AppuntamentiCalendarioModel>();
            foreach (Appuntamenti item in AppuntamentiDisponibili)
                appuntamenti.Add(LoadAppuntamentoPerCalendario(item));

            return Json(appuntamenti, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult Create()
        {
            int idUtente = Int32.Parse(Session["IdUtente"].ToString());


            //Mi carico i clienti da usare nel modale della creazione
            List<SelectListItem> Clienti = new List<SelectListItem>();
            foreach (Clienti cliente in db.Utenti.Find(idUtente).Aziende.Clienti)
                Clienti.Add(new SelectListItem { Text = cliente.Nome, Value = cliente.Id.ToString() });
            ViewBag.Clienti = Clienti;

            //Mi carico le tipologie da usare nel modale della creazione
            List<SelectListItem> Tipologie = new List<SelectListItem>();
            foreach (AppuntamentiTipologia tipologia in db.AppuntamentiTipologia.ToList())
                Tipologie.Add(new SelectListItem { Text = tipologia.Tipologia, Value = tipologia.id.ToString() });
            ViewBag.Tipologie = Tipologie;


            return PartialView();
        }

        [HttpPost]
        public JsonResult Create(Appuntamenti appuntamento)
        {
            try
            {
                appuntamento.DataAggiunta = DateTime.Now;
                appuntamento.FkUtente = Int32.Parse(Session["IdUtente"].ToString());
                appuntamento.Concluso = false;

                if(ModelState.IsValid)
                {
                    db.Appuntamenti.Add(appuntamento);
                    db.SaveChanges();

                    appuntamento.Clienti = db.Clienti.Find(appuntamento.FkCliente);
                    appuntamento.AppuntamentiTipologia = db.AppuntamentiTipologia.Find(appuntamento.FkTipologia);

                    AppuntamentiCalendarioModel appuntamentoJSON = LoadAppuntamentoPerCalendario(appuntamento);

                    return Json(appuntamentoJSON, JsonRequestBehavior.AllowGet);
                }


            }
            catch { }

            return Json("", JsonRequestBehavior.AllowGet);

        }


        public AppuntamentiCalendarioModel LoadAppuntamentoPerCalendario(Appuntamenti appuntamento)
        /* Medoto per trasformare un Appuntamento in un AppuntamentiCalendarioModel; è necessario per evitare le
         * dipendenze circolari dovute dal lazy loading */
        {
            List<ServiziCalendarioModel> Servizi = new List<ServiziCalendarioModel>();
            foreach (AppuntamentiServizi servizio in appuntamento.AppuntamentiServizi)
                Servizi.Add(new ServiziCalendarioModel { Nome = servizio.Servizi.Servizio, Icona = servizio.Servizi.Icona });

            return new AppuntamentiCalendarioModel
            {
                id = appuntamento.Id,
                NomeCliente = appuntamento.Clienti.Nome,
                Tipologia = appuntamento.AppuntamentiTipologia.Tipologia,
                Data = appuntamento.Date.ToString("yyyy-MM-dd"),
                Inizio = appuntamento.Date.Add(appuntamento.OraInizio).ToString("yyyy-MM-ddTHH:mm"),
                Fine = appuntamento.Date.Add(appuntamento.OraFine).ToString("yyyy-MM-ddTHH:mm"),
                Descrizione = appuntamento.Descrizione,
                Note = appuntamento.Note,
                Concluso = appuntamento.Concluso,
                Colore = appuntamento.AppuntamentiTipologia.Colore,
                Colore2 = appuntamento.AppuntamentiTipologia.Colore2,
                Colore3 = appuntamento.AppuntamentiTipologia.Colore3,
                Servizi = Servizi
            };
        }


        public JsonResult UpdateDataAppuntamento(int Id, DateTime Date)
        {
            try
            {
                Appuntamenti appuntamento = db.Appuntamenti.Find(Id);

                TimeSpan differenza = appuntamento.OraFine - appuntamento.OraInizio;

                appuntamento.Date = Date.Date;
                appuntamento.OraInizio = Date.AddHours(-2).TimeOfDay;
                appuntamento.OraFine = Date.AddHours(-2).TimeOfDay + differenza;
                db.Entry(appuntamento).State = EntityState.Modified;
                db.SaveChanges();

                return Json(appuntamento.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
              return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult LoadAppuntamentoDetails(int Id)
        {
            Appuntamenti appuntamento = new Appuntamenti();
            try
            {
                appuntamento = db.Appuntamenti.Find(Id);
            }
            catch { }
            return Json(LoadAppuntamentoPerCalendario(appuntamento), JsonRequestBehavior.AllowGet);
        }


        public JsonResult EditNoteAppuntamento(int Id, string note)
        {
            Appuntamenti appuntamento = new Appuntamenti();
            try
            {
                appuntamento = db.Appuntamenti.Find(Id);
                appuntamento.Note = note;
                db.Entry(appuntamento).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch { }
            return Json(LoadAppuntamentoPerCalendario(appuntamento), JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult Index()
        {
            return View(db.Appuntamenti.ToList());
        }

        public ActionResult CreateAppuntamento()
        {
            int idUtente = Int32.Parse(Session["IdUtente"].ToString());
            //Mi carico i clienti da usare nel modale della creazione
            List<SelectListItem> Clienti = new List<SelectListItem>();
            foreach (Clienti cliente in db.Utenti.Find(idUtente).Aziende.Clienti)
                Clienti.Add(new SelectListItem { Text = cliente.Nome, Value = cliente.Id.ToString() });
            ViewBag.Clienti = Clienti;

            //Mi carico le tipologie da usare nel modale della creazione
            List<SelectListItem> Tipologie = new List<SelectListItem>();
            foreach (AppuntamentiTipologia tipologia in db.AppuntamentiTipologia.ToList())
                Tipologie.Add(new SelectListItem { Text = tipologia.Tipologia, Value = tipologia.id.ToString() });
            ViewBag.Tipologie = Tipologie;

            int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());
            ViewBag.Servizi = db.Servizi.Where(s => s.FkAzienda == idAzienda).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateAppuntamento(Appuntamenti appuntamento)
        {
            appuntamento.DataAggiunta = DateTime.Now;
            appuntamento.FkUtente = Int32.Parse(Session["IdUtente"].ToString());
            try
            {
                db.Appuntamenti.Add(appuntamento);

                foreach (var item in appuntamento.Servizi.Remove(appuntamento.Servizi.Length - 1).Split(','))
                {
                    db.AppuntamentiServizi.Add(new AppuntamentiServizi
                    {
                        FkAppuntamento = appuntamento.Id,
                        FkServizio = Int32.Parse(item)
                    });
                }

                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            int idUtente = Int32.Parse(Session["IdUtente"].ToString());

            //Mi carico i clienti da usare nel modale della creazione
            List<SelectListItem> Clienti = new List<SelectListItem>();
            foreach (Clienti cliente in db.Utenti.Find(idUtente).Aziende.Clienti)
                Clienti.Add(new SelectListItem { Text = cliente.Nome, Value = cliente.Id.ToString() });
            ViewBag.Clienti = Clienti;

            //Mi carico le tipologie da usare nel modale della creazione
            List<SelectListItem> Tipologie = new List<SelectListItem>();
            foreach (AppuntamentiTipologia tipologia in db.AppuntamentiTipologia.ToList())
                Tipologie.Add(new SelectListItem { Text = tipologia.Tipologia, Value = tipologia.id.ToString() });
            ViewBag.Tipologie = Tipologie;


            return View(db.Appuntamenti.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Appuntamenti appuntamento)
        {
            try
            {
                //Aggiorno i servizi
                AggiornaServizi(appuntamento.Servizi.Remove(appuntamento.Servizi.Length - 1), appuntamento.Id);

                db.Entry(appuntamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Calendar");
            }
            catch { }
            return View();
        }


        private void AggiornaServizi(string nuoviServizi, int idAppuntamento)
        {
            List<AppuntamentiServizi> Servizi = db.AppuntamentiServizi
                .Where(s => s.FkAppuntamento == idAppuntamento).ToList();

            //Rimuovo i servizi
            foreach (AppuntamentiServizi item in Servizi)
                db.AppuntamentiServizi.Remove(item);

            foreach(var item in nuoviServizi.Split(','))
            {
                db.AppuntamentiServizi.Add(new AppuntamentiServizi
                                                { 
                                                    FkAppuntamento = idAppuntamento, 
                                                    FkServizio = Int32.Parse(item) 
                                                });
            }

            db.SaveChanges();
        }


        public ActionResult Delete(int id)
        {
            db.Appuntamenti.Remove(db.Appuntamenti.Find(id));
            db.SaveChanges();
            return RedirectToAction("Calendar");
        }


        public PartialViewResult AppuntamentiPartial(int idCliente, int idUtente)
        //Metodo che restituisce una partial degli appuntamenti filtrata per cliente o utente che l'ha creato
        {
            if(idCliente != 0)
                return PartialView(db.Appuntamenti.Where(c => c.FkCliente == idCliente).OrderByDescending(a => a.Date));
            return PartialView(db.Appuntamenti.Where(c => c.FkUtente == idUtente).OrderByDescending(a => a.Date));
        }

    }
}
