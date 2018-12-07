using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka;

namespace Biblioteka.Controllers {
    public class WypozyczeniesController : Controller {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Wypozyczenies
        public ActionResult Index() {
            var wypozyczenies = db.Wypozyczenies.Include(w => w.Czytelnik).Include(w => w.Egzemplarz);
            return View(wypozyczenies.ToList());
        }

        // GET: Wypozyczenies/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenies.Find(id);
            if (wypozyczenie == null) {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // GET: Wypozyczenies/Create
        public ActionResult Create(int id) {
            IEnumerable<int> ids = from w in db.Wypozyczenies
                                   select w.IdWypozyczenie;
            ViewBag.IdCzytelnikaDlaWypozyczenia = id;
            ViewBag.Dzisiaj = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Termin = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            ViewBag.IdWypozyczenie = (ids.Count() == 0) ? 1 : ids.Last() + 1;
            ViewBag.IdCzytelnik = new SelectList(db.Czytelniks, "IdCzytelnik", "Imie");
            ViewBag.IdEgzemplarz = new SelectList(db.Egzemplarzs, "IdEgzemplarz", "IdEgzemplarz");
            return View();
        }

        // POST: Wypozyczenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdWypozyczenie,IdEgzemplarz,IdCzytelnik,CzyOddane,TerminWypozyczenia,TerminOddania,Wersja")] Wypozyczenie wypozyczenie, int id) {
            wypozyczenie.IdCzytelnik = id;
            IEnumerable<int> ids = from w in db.Wypozyczenies
                                   select w.IdWypozyczenie;
            ViewBag.IdWypozyczenie = (ids.Count() == 0) ? 1 : ids.Last() + 1;
            wypozyczenie.IdWypozyczenie = (ids.Count() == 0) ? 1 : ids.Last() + 1;
            ViewBag.Dzisiaj = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Termin = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

            if (ModelState.IsValid) {
                db.Wypozyczenies.Add(wypozyczenie);
                
                try {
                    db.SaveChanges();
                } catch (Exception e) {
                    string message = "";

                    if (e.InnerException == null) {
                        message = "Podano nieprawidłowe dane wypozyczenia!";
                    } else {
                        message = e.InnerException.InnerException.Message;
                    }

                    ViewBag.IdCzytelnikaDlaWypozyczenia = id;
                    ViewBag.Exception = message;
                    ViewBag.IdCzytelnik = new SelectList(db.Czytelniks, "IdCzytelnik", "Imie", wypozyczenie.IdCzytelnik);
                    ViewBag.IdEgzemplarz = new SelectList(db.Egzemplarzs, "IdEgzemplarz", "IdEgzemplarz", wypozyczenie.IdEgzemplarz);
                    return View(wypozyczenie);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdCzytelnikaDlaWypozyczenia = id;
            ViewBag.IdCzytelnik = new SelectList(db.Czytelniks, "IdCzytelnik", "Imie", wypozyczenie.IdCzytelnik);
            ViewBag.IdEgzemplarz = new SelectList(db.Egzemplarzs, "IdEgzemplarz", "IdEgzemplarz", wypozyczenie.IdEgzemplarz);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenies/Edit/5
        public ActionResult Edit(int? id) {
            
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenies.Find(id);
            if (wypozyczenie == null) {
                return HttpNotFound();
            }
            ViewBag.IdCzytelnik = new SelectList(db.Czytelniks, "IdCzytelnik", "Imie", wypozyczenie.IdCzytelnik);
            ViewBag.IdEgzemplarz = new SelectList(db.Egzemplarzs, "IdEgzemplarz", "IdEgzemplarz", wypozyczenie.IdEgzemplarz);
            return View(wypozyczenie);
        }

        // POST: Wypozyczenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdWypozyczenie,IdEgzemplarz,IdCzytelnik,CzyOddane,TerminWypozyczenia,TerminOddania,Wersja")] Wypozyczenie wypozyczenie) {
            if (ModelState.IsValid) {
                db.Entry(wypozyczenie).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                } catch (Exception e) {
                    string message = "";

                    if (e.InnerException == null) {
                        message = "Podano nieprawidłowe dane wypozyczenia!";
                    } else {
                        message = e.InnerException.InnerException.Message;
                    }

                    //ViewBag.IdCzytelnikaDlaWypozyczenia = id;
                    ViewBag.Exception = message;
                    ViewBag.IdCzytelnik = new SelectList(db.Czytelniks, "IdCzytelnik", "Imie", wypozyczenie.IdCzytelnik);
                    ViewBag.IdEgzemplarz = new SelectList(db.Egzemplarzs, "IdEgzemplarz", "IdEgzemplarz", wypozyczenie.IdEgzemplarz);
                    return View(wypozyczenie);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdCzytelnik = new SelectList(db.Czytelniks, "IdCzytelnik", "Imie", wypozyczenie.IdCzytelnik);
            ViewBag.IdEgzemplarz = new SelectList(db.Egzemplarzs, "IdEgzemplarz", "IdEgzemplarz", wypozyczenie.IdEgzemplarz);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenies/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenies.Find(id);
            if (wypozyczenie == null) {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // POST: Wypozyczenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Wypozyczenie wypozyczenie = db.Wypozyczenies.Find(id);
            db.Wypozyczenies.Remove(wypozyczenie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
