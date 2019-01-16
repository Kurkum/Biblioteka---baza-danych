using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class AutorstwoesController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Autorstwoes/Index/5
        public ActionResult Index(int id)
        {
            var autorstwa = from autorstwo in db.AutorstwoPoIdKsiazki(id)
                            select autorstwo;

            List<AutorstwoWidok> autorstwoWidok = new List<AutorstwoWidok>();

            foreach (var autorstwo in autorstwa) {
                var autor = (from autorzy in db.Autors
                             where autorzy.IdAutor == autorstwo.IdAutor
                             select autorzy).FirstOrDefault();

                autorstwoWidok.Add(new AutorstwoWidok(autorstwo.IdKsiazka, autorstwo.IdAutor, autor.Imie, autor.Nazwisko));
            }

            ViewBag.IdKsiazki = id;

            return View(autorstwoWidok);
        }

        // GET: Autorstwoes/Create/5
        public ActionResult Create(int id) {
            ViewBag.IdKsiazka = id;
            ViewBag.IdAutor = new SelectList(db.Autors, "IdAutor", "IdAutor");
            return View();
        }

        // POST: Autorstwoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKsiazka,IdAutor")] Autorstwo autorstwo) {
            ViewBag.IdKsiazka = autorstwo.IdKsiazka;

            if (ModelState.IsValid) {
                db.DodajAutorstwo(autorstwo.IdKsiazka, autorstwo.IdAutor);
                try {
                    db.SaveChanges();
                }
                catch (Exception e) {
                    string message = "";

                    if (e.InnerException == null) {
                        message = "Podano nieprawidłowe dane autorstwa!";
                    }
                    else {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    return View(autorstwo);
                }
                return RedirectToAction("Index", new { id = autorstwo.IdKsiazka });
            }

            return View(autorstwo);
        }

        // GET: Ksiazkas/Delete/5
        public ActionResult Delete(int idKsiazka, int idAutor) {
            db.UsunAutorstwo(idAutor, idKsiazka);
            return RedirectToAction("Index", new { id = idKsiazka });
        }
    }
}