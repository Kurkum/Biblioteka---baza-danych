using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka;
using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteka.Controllers
{
    public class CzytelniksController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Czytelniks
        public ActionResult Index()
        {
            return View(db.Czytelniks.ToList());
        }

        // GET: Czytelniks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czytelnik czytelnik = db.Czytelniks.Find(id);
            if (czytelnik == null)
            {
                return HttpNotFound();
            }
            return View(czytelnik);
        }

        // GET: Czytelniks/Create
        public ActionResult Create()
        {
            IEnumerable<int> ids = from czytelnik in db.Czytelniks
                                   select czytelnik.IdCzytelnik;
            ViewBag.IdCzytelnik = (ids.Count() == 0) ? 1 : ids.Last() + 1;
            return View();
        }

        // POST: Czytelniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCzytelnik,Imie,Nazwisko,Telefon,Adres,Wersja")] Czytelnik czytelnik)
        {
            if (ModelState.IsValid)
            {
                db.Czytelniks.Add(czytelnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(czytelnik);
        }

        // GET: Czytelniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czytelnik czytelnik = db.Czytelniks.Find(id);
            if (czytelnik == null)
            {
                return HttpNotFound();
            }
            return View(czytelnik);
        }

        // POST: Czytelniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCzytelnik,Imie,Nazwisko,Telefon,Adres,Wersja")] Czytelnik czytelnik) {
            if (ModelState.IsValid) {
                db.Entry(czytelnik).State = EntityState.Modified;
                try {
                    var aktualnaWersja = from czytelnikWersja in db.Czytelniks
                                         where czytelnikWersja.IdCzytelnik == czytelnik.IdCzytelnik
                                         select czytelnikWersja.Wersja;

                    if (aktualnaWersja.FirstOrDefault() != czytelnik.Wersja) {
                        throw new Exception("BO");
                    }

                    czytelnik.Wersja++;
                    db.SaveChanges();
                } catch (Exception e) {
                    string message = "";
                    if (e.Message == "BO") {
                        message = "Konflikt aktualizacji - blokowanie optymistyczne!";
                    } else if (e.InnerException == null) {
                        message = "Podano nieprawidłowe dane czytelnika!";
                    } else {
                        message = e.InnerException.InnerException.Message;
                    }

                    //ViewBag.IdCzytelnikaDlaWypozyczenia = id;
                    TempData["Exception"] = message;

                    czytelnik = (from czytelnicy in db.Czytelniks
                                 where czytelnicy.IdCzytelnik == czytelnik.IdCzytelnik
                                 select czytelnicy).FirstOrDefault();

                    return RedirectToAction("Edit");
                }
                return RedirectToAction("Index");
            }
            return View(czytelnik);
        }

        // GET: Czytelniks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czytelnik czytelnik = db.Czytelniks.Find(id);
            if (czytelnik == null)
            {
                return HttpNotFound();
            }
            return View(czytelnik);
        }

        // POST: Czytelniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            try
            {
                db.UsunCzytelnika(id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                string message = "";

                if (e.InnerException == null)
                {
                    message = "Podano nieprawidłowe dane wypozyczenia!";
                }
                else
                {
                    message = e.InnerException.Message;
                }

                TempData["message"] = message;
                return RedirectToAction("Delete");
            }

            /*using (var context = new BibliotekaEntities()) {
                using (DbContextTransaction dbContextTransaction = context.Database.BeginTransaction(IsolationLevel.ReadUncommitted)) {
                    try {
                        context.Database.ExecuteSqlCommand(
                        @"DELETE FROM Wypozyczenie WHERE Wypozyczenie.IdCzytelnik = " + id);

                        context.Database.ExecuteSqlCommand(
                        @"DELETE FROM Czytelnik WHERE Czytelnik.IdCzytelnik = " + id);

                        context.SaveChanges();

                        dbContextTransaction.Commit();

                        return RedirectToAction("Index");
                    } catch (Exception e) {
                        string message = "";

                        if (e.InnerException == null) {
                            message = "Wystąpił błąd przy usuwaniu czytelnika!";
                        } else {
                            message = e.InnerException.Message;
                        }

                        TempData["message"] = message;
                        return RedirectToAction("Delete");
                    }
                }
            }*/
        }

        // GET: Czytelnik/Wypozyczenies/5
        public ActionResult Wypozyczenies(int id) {
            ViewBag.IdCzytelnikaDlaWypozyczenia = id;
            ViewBag.Imie = (from czytelnik in db.Czytelniks
                            where czytelnik.IdCzytelnik == id
                            select czytelnik.Imie).FirstOrDefault();
            ViewBag.Nazwisko = (from czytelnik in db.Czytelniks
                                where czytelnik.IdCzytelnik == id
                                select czytelnik.Nazwisko).FirstOrDefault();
            var wypozyczenies = from item in db.Wypozyczenies
                                where item.IdCzytelnik == id
                                select item;
            return View(wypozyczenies.ToList());
        }

        // GET: Czytelnik/Przedluz/5
        public ActionResult Przedluz(int id) {
            ViewBag.IdCzytelnikaDlaWypozyczenia = id;
            db.Database.ExecuteSqlCommand("set transaction isolation level repeatable read");
            db.PrzedluzenieTerminuOddania(id);

            var czytelnikId = from wypozyczenie in db.Wypozyczenies
                              where wypozyczenie.IdEgzemplarz == id
                              select wypozyczenie.IdCzytelnik;

            return RedirectToAction("Wypozyczenies/" + czytelnikId.FirstOrDefault());
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
