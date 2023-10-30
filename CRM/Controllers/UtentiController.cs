using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class UtentiController : Controller
    {
        ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            int AziendaId = Int32.Parse(Session["IdAzienda"].ToString());
            return View(db.Utenti.Where(u => u.FkAzienda == AziendaId).ToList());
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult IndexAdmin()
        {
            return View(db.Utenti.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(db.Utenti.Find(id));
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Utenti utente)
        {
            try
            {
                utente.FkAzienda = Int32.Parse(Session["IdAzienda"].ToString());
                utente.LastOnline = DateTime.Now;

                //Se ha caricato un nuova immagine
                if (utente.FotoFile != null && utente.FotoFile.ContentLength > 0)
                {
                    //Mi creo il path per l'immagine, ci aggiungo la data per non avere mai due file con lo stesso nome e creare errori
                    string path = Path.Combine(Server.MapPath("~/Content/Imgs/Propic/"), utente.FotoFile.FileName);
                    utente.FotoFile.SaveAs(path);
                    utente.Propic = utente.FotoFile.FileName;
                }
                else
                    utente.Propic = "default.png";

                db.Utenti.Add(utente);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View(db.Utenti.Find(id));
        }


        [HttpPost]
        public ActionResult Edit(Utenti utente)
        {
            try
            {
                //Se ha caricato un nuova immagine
                if (utente.FotoFile != null && utente.FotoFile.ContentLength > 0)
                {
                    //Se non ha la propic di default, devo cancellare la vecchia sul server
                    if(utente.Propic != "default.png")
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Imgs/Propic/"), utente.Propic));

                    //Mi creo il path per l'immagine, ci aggiungo la data per non avere mai due file con lo stesso nome e creare errori
                    string path = Path.Combine(Server.MapPath("~/Content/Imgs/Propic/"), utente.FotoFile.FileName);
                    utente.FotoFile.SaveAs(path);
                    utente.Propic = utente.FotoFile.FileName;
                }

                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
