using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AziendeController : Controller
    {
        ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            return View(db.Aziende.ToList());
        }


        public ActionResult Details(int id)
        {
            return View(db.Aziende.Find(id));
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Aziende azienda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Aziende.Add(azienda);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch { }
            return View();

        }


        public ActionResult Edit(int id)
        {
            return View(db.Aziende.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Aziende azienda)
        {
            try
            {
                db.Entry(azienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { }
            return View();
        }


        public ActionResult Delete(int id)
        {
            db.Aziende.Remove(db.Aziende.Find(id));
            return View();
        }
    }
}
