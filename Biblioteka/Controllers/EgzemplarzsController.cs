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
            ViewBag.LiczbaWypozyczen = db.LiczbaWypozyczenOkres(DateTime.Today.AddDays(-365), DateTime.Today).FirstOrDefault();
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
            var ids = from egzemplarz in db.Egzemplarzs
                                   select egzemplarz;
            ViewBag.IdEgzemplarz = (ids.Count() == 0) ? 1 : ids.ToArray().OrderBy(element => element.IdEgzemplarz).LastOrDefault().IdEgzemplarz + 1;
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane egzemplarza!";
                    }
                    else
                    {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
                    return View(egzemplarz);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
            return View(egzemplarz);                                  
        }

        // GET: Egzemplarzs/CreateUsingProcedure
        public ActionResult CreateUsingProcedure() {
            var ids = from egzemplarz in db.Egzemplarzs
                      select egzemplarz;
            ViewBag.IdEgzemplarz = (ids.Count() == 0) ? 1 : ids.ToArray().OrderBy(element => element.IdEgzemplarz).LastOrDefault().IdEgzemplarz + 1;
            ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul");
            return View();
        }

        // POST: Egzemplarzs/CreateUsingProcedure
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUsingProcedure([Bind(Include = "IdEgzemplarz,IdKsiazka")] Egzemplarz egzemplarz) {
            if (ModelState.IsValid)
            {
                db.Egzemplarzs.Add(egzemplarz);
                try
                {
                    db.DodajEgzemplarz(egzemplarz.IdEgzemplarz, egzemplarz.IdKsiazka);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane egzemplarza!";
                    }
                    else
                    {
                        message = e.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
                    return View(egzemplarz);
                }
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane egzemplarza!";
                    }
                    else
                    {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    ViewBag.IdKsiazka = new SelectList(db.Ksiazkas, "IdKsiazka", "Tytul", egzemplarz.IdKsiazka);
                    return View(egzemplarz);
                }
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

        public ActionResult PrzetrzymaneKsiazkis()
        {
            return View(db.PrzetrzymaneKsiazkis.ToList());
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
