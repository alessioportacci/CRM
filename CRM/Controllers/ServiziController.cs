using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ServiziController : Controller
    {
        ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());
            ViewBag.azienda = idAzienda;
            return View(db.Servizi.Where(s => s.Aziende.Id == idAzienda).ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Servizi serv)
        {
            try
            {
                int idAzienda = Int32.Parse(Session["IdAzienda"].ToString());
                if (ModelState.IsValid)
                {
                    serv.Aziende = db.Aziende.Find(idAzienda);
                    db.Servizi.Add(serv);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            return View(db.Servizi.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Servizi serv)
        {
            try
            {
                db.Entry(serv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public PartialViewResult Icone()
        {
            return PartialView();
        }
    }
}
