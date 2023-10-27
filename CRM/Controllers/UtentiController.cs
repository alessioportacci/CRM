using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
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
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
