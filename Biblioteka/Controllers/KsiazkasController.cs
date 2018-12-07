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
    public class KsiazkasController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Ksiazkas
        public ActionResult Index()
        {
            var ksiazkas = db.Ksiazkas.Include(k => k.Gatunek).Include(k => k.Wydawnictwo);
            return View(ksiazkas.ToList());
        }

        // GET: Ksiazkas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazkas.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // GET: Ksiazkas/Create
        public ActionResult Create()
        {
            IEnumerable<int> ids = from ksiazka in db.Ksiazkas
                                   select ksiazka.IdKsiazka;
            ViewBag.IdKsiazka = (ids.Count() == 0) ? 1 : ids.Last() + 1;
            ViewBag.IdGatunek = new SelectList(db.Gatuneks, "IdGatunek", "Nazwa");
            ViewBag.IdWydawnictwo = new SelectList(db.Wydawnictwoes, "IdWydawnictwo", "Nazwa");
            return View();
        }

        // POST: Ksiazkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKsiazka,IdWydawnictwo,IdGatunek,Tytul,RokWydania")] Ksiazka ksiazka)
        {

            if (ModelState.IsValid)
            {
                db.Ksiazkas.Add(ksiazka);

                try {
                    db.SaveChanges();
                }
                catch (Exception e) {
                    string message = "";

                    if (e.InnerException == null) {
                        message = "Podano nieprawidłowe dane książki!";
                    }
                    else {
                        message = e.InnerException.InnerException.Message;
                    }

                    ViewBag.Exception = message;
                    ViewBag.IdGatunek = new SelectList(db.Gatuneks, "IdGatunek", "Nazwa", ksiazka.IdGatunek);
                    ViewBag.IdWydawnictwo = new SelectList(db.Wydawnictwoes, "IdWydawnictwo", "Nazwa", ksiazka.IdWydawnictwo);
                    return View(ksiazka);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdGatunek = new SelectList(db.Gatuneks, "IdGatunek", "Nazwa", ksiazka.IdGatunek);
            ViewBag.IdWydawnictwo = new SelectList(db.Wydawnictwoes, "IdWydawnictwo", "Nazwa", ksiazka.IdWydawnictwo);
            return View(ksiazka);
        }

        // GET: Ksiazkas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazkas.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGatunek = new SelectList(db.Gatuneks, "IdGatunek", "Nazwa", ksiazka.IdGatunek);
            ViewBag.IdWydawnictwo = new SelectList(db.Wydawnictwoes, "IdWydawnictwo", "Nazwa", ksiazka.IdWydawnictwo);
            return View(ksiazka);
        }

        // POST: Ksiazkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKsiazka,IdWydawnictwo,IdGatunek,Tytul,RokWydania")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.SaveChanges();
                }
                catch (Exception e) {
                    string message = "";

                    if (e.InnerException == null) {
                        message = "Podano nieprawidłowe dane książki!";
                    }
                    else {
                        message = e.InnerException.InnerException.Message;
                    }

                    ViewBag.Exception = message;
                    ViewBag.IdGatunek = new SelectList(db.Gatuneks, "IdGatunek", "Nazwa", ksiazka.IdGatunek);
                    ViewBag.IdWydawnictwo = new SelectList(db.Wydawnictwoes, "IdWydawnictwo", "Nazwa", ksiazka.IdWydawnictwo);
                    return View(ksiazka);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdGatunek = new SelectList(db.Gatuneks, "IdGatunek", "Nazwa", ksiazka.IdGatunek);
            ViewBag.IdWydawnictwo = new SelectList(db.Wydawnictwoes, "IdWydawnictwo", "Nazwa", ksiazka.IdWydawnictwo);
            return View(ksiazka);
        }

        // GET: Ksiazkas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazkas.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // POST: Ksiazkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ksiazka ksiazka = db.Ksiazkas.Find(id);
            db.Ksiazkas.Remove(ksiazka);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Ksiazkas/Egzemplarzs/5
        public ActionResult Egzemplarzs(int id) {
            var egzemplarzs = from item in db.Egzemplarzs
                              where item.IdKsiazka == id
                              select item;
            return View(egzemplarzs.ToList());
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
