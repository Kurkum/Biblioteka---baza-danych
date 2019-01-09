using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka;

namespace Biblioteka.Controllers
{
    public class PrzetrzymaneKsiazkisController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: PrzetrzymaneKsiazkis
        public ActionResult Index()
        {
            return View(db.PrzetrzymaneKsiazkis.ToList());
        }

        // GET: PrzetrzymaneKsiazkis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrzetrzymaneKsiazki przetrzymaneKsiazki = db.PrzetrzymaneKsiazkis.Find(id);
            if (przetrzymaneKsiazki == null)
            {
                return HttpNotFound();
            }
            return View(przetrzymaneKsiazki);
        }

        // GET: PrzetrzymaneKsiazkis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrzetrzymaneKsiazkis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEgzemplarz,IdCzytelnik,Imie,Nazwisko,Tytul,IloscDni,WartoscKary")] PrzetrzymaneKsiazki przetrzymaneKsiazki)
        {
            if (ModelState.IsValid)
            {
                db.PrzetrzymaneKsiazkis.Add(przetrzymaneKsiazki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(przetrzymaneKsiazki);
        }

        // GET: PrzetrzymaneKsiazkis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrzetrzymaneKsiazki przetrzymaneKsiazki = db.PrzetrzymaneKsiazkis.Find(id);
            if (przetrzymaneKsiazki == null)
            {
                return HttpNotFound();
            }
            return View(przetrzymaneKsiazki);
        }

        // POST: PrzetrzymaneKsiazkis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEgzemplarz,IdCzytelnik,Imie,Nazwisko,Tytul,IloscDni,WartoscKary")] PrzetrzymaneKsiazki przetrzymaneKsiazki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przetrzymaneKsiazki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(przetrzymaneKsiazki);
        }

        // GET: PrzetrzymaneKsiazkis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrzetrzymaneKsiazki przetrzymaneKsiazki = db.PrzetrzymaneKsiazkis.Find(id);
            if (przetrzymaneKsiazki == null)
            {
                return HttpNotFound();
            }
            return View(przetrzymaneKsiazki);
        }

        // POST: PrzetrzymaneKsiazkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrzetrzymaneKsiazki przetrzymaneKsiazki = db.PrzetrzymaneKsiazkis.Find(id);
            db.PrzetrzymaneKsiazkis.Remove(przetrzymaneKsiazki);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
