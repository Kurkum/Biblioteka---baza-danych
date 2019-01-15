using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Biblioteka;
using System.Configuration;
using System.Data.SqlClient;
using Biblioteka.Models;
using Newtonsoft.Json;

namespace Biblioteka.Controllers
{
    public class CzytelniksController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Czytelniks
        public ActionResult Index()
        {
            /*var czytelnicy = (from czytelnik in db.Czytelniks
                            select czytelnik);
            List<Dluznik> dluznicy = new List<Dluznik>();

            //TODO: dodać funkcję obliczającą wartość kary
            foreach (var czytelnik in czytelnicy) {
                //decimal wartoscKary = db.WartoscKary(czytelnik.IdCzytelnik);
                //if (wartoscKary) != 0.0m)
                {
                    dluznicy.Add(new Dluznik(czytelnik.IdCzytelnik, 1.2m));
                }
            }

            string json = JsonConvert.SerializeObject(dluznicy);

            FileStream fileStream = new FileStream(@"dluznicy.json", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.Write(json);

            streamWriter.Close();
            fileStream.Close();*/

            return View(db.Czytelniks.ToList());
        }

        // GET: Najpopularniejsze gatunki
        public ActionResult NajlepsiCzytelnicy() {
            var czytelnicy = (from czytelnik in db.Czytelniks
                              select czytelnik).ToArray();

            List<NajlepszyCzytelnik> najlepsiCzytelnicy = new List<NajlepszyCzytelnik>();

            foreach (var czytelnik in czytelnicy) {
                var liczbaWypozyczen = (from wypozyczenie in db.Wypozyczenies
                                        select wypozyczenie).Where(wypozyczenie => wypozyczenie.IdCzytelnik == czytelnik.IdCzytelnik).Count();
                najlepsiCzytelnicy.Add(new NajlepszyCzytelnik(czytelnik.Imie, czytelnik.Nazwisko, liczbaWypozyczen));
            }

            return View(najlepsiCzytelnicy.Where(czytelnik => czytelnik.Liczba != 0).OrderByDescending(czytelnik => czytelnik.Liczba).Take(5));
        }

        // POST: Czytelniks/Oddaj/5
        public ActionResult Oddaj(int id) {
            var wypozyczenie = (from wypozyczenia in db.Wypozyczenies
                                where wypozyczenia.IdWypozyczenie == id
                                select wypozyczenia).ToArray().FirstOrDefault();

            wypozyczenie.CzyOddane = true;

            db.SaveChanges();

            return RedirectToAction("Wypozyczenies/" + wypozyczenie.IdCzytelnik);
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

            var czytelnikId = (from wypozyczenie in db.Wypozyczenies
                               where wypozyczenie.IdWypozyczenie == id
                               select wypozyczenie.IdCzytelnik).ToArray();

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