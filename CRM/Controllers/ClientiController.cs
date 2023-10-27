using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {
        ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());
            return View(db.Clienti.Where(c => c.FkAzienda == idAzienda).ToList());
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult IndexAdmin()
        {
            return View("Index", db.Clienti.ToList());
        }


        public PartialViewResult Appuntamenti(int id)
        {
            return PartialView(db.Appuntamenti.Where(c => c.FkCliente == id));
        }


        public ActionResult Details(int id)
        {
            return View(db.Clienti.Find(id));
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Clienti cliente)
        {
            try
            {
                int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());
                cliente.FkAzienda = idAzienda;
                cliente.DataRegistrazione = DateTime.Now;
                db.Clienti.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { }
            return View();
        }


        public ActionResult Edit(int id)
        {
            return View(db.Clienti.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Clienti cliente)
        {
            try
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { }
            return View();
        }


        public ActionResult Delete(int id)
        {
            db.Clienti.Remove(db.Clienti.Find(id));
            return View();
        }
    }
}
