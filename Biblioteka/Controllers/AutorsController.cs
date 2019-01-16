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
    public class AutorsController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Autors
        public ActionResult Index()
        {

            return View(db.Autors.ToList());
        }

        // GET: Autors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autors.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // GET: Autors/Create
        public ActionResult Create()
        {
            var ids = from autor in db.Autors
                                   select autor;
            ViewBag.IdAutor = (ids.Count() == 0) ? 1 : ids.ToArray().OrderBy(element => element.IdAutor).LastOrDefault().IdAutor + 1;
            return View();
        }

        public ActionResult CreateUsingProcedure()
        {
            var ids = from autor in db.Autors
                           select autor;

            ViewBag.IdAutor = (ids.Count() == 0) ? 1 : ids.ToArray().OrderBy(element => element.IdAutor).LastOrDefault().IdAutor + 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUsingProcedure([Bind(Include = "IdAutor, Imie, Nazwisko")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    db.DodajAutora(autor.IdAutor, autor.Imie, autor.Nazwisko);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane autora!";
                    }
                    else
                    {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    return View(autor);
                }
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // POST: Autors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAutor,Imie,Nazwisko")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Autors.Add(autor);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane autora!";
                    }
                    else
                    {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    return View(autor);
                }
                    return RedirectToAction("Index");
            }

            return View(autor);
        }

        // GET: Autors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autors.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAutor,Imie,Nazwisko")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autors.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autor autor = db.Autors.Find(id);
            db.Autors.Remove(autor);
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