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
    public class EgzemplarzsController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Egzemplarzs
        public ActionResult Index()
        {
            var egzemplarzs = db.Egzemplarzs.Include(e => e.Ksiazka);
            return View(egzemplarzs.ToList());
        }

        // GET: Egzemplarzs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzemplarz egzemplarz = db.Egzemplarzs.Find(id);
            if (egzemplarz == null)
            {
                return HttpNotFound();
            }
            return View(egzemplarz);
        }

        // GET: Egzemplarzs/Create
        public ActionResult Create()
        {
            IEnumerable<int> ids = from egzemplarz in db.Egzemplarzs
                                   select egzemplarz.IdEgzemplarz;
            ViewBag.IdEgzemplarz = (ids.Count() == 0) ? 1 : ids.Last() + 1;
            ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul");
            return View();
        }

        // POST: Egzemplarzs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEgzemplarz,IdKsiazka")] Egzemplarz egzemplarz)
        {
            if (ModelState.IsValid)
            {
                db.Egzemplarzs.Add(egzemplarz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
            return View(egzemplarz);
        }

        // GET: Egzemplarzs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzemplarz egzemplarz = db.Egzemplarzs.Find(id);
            if (egzemplarz == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
            return View(egzemplarz);
        }

        // POST: Egzemplarzs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEgzemplarz,IdKsiazka")] Egzemplarz egzemplarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(egzemplarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
            return View(egzemplarz);
        }

        // GET: Egzemplarzs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzemplarz egzemplarz = db.Egzemplarzs.Find(id);
            if (egzemplarz == null)
            {
                return HttpNotFound();
            }
            return View(egzemplarz);
        }

        // POST: Egzemplarzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Egzemplarz egzemplarz = db.Egzemplarzs.Find(id);
            db.Egzemplarzs.Remove(egzemplarz);
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
