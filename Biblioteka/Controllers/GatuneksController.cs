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
    public class GatuneksController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Gatuneks
        public ActionResult Index()
        {
            return View(db.Gatuneks.ToList());
        }

        // GET: Najpopualrniejsze gatunki
        public ActionResult NajpopularniejszeGatunki() {
            return View(db.NajpopularniejszeGatunki().ToList());
        }

        // GET: Gatuneks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gatunek gatunek = db.Gatuneks.Find(id);
            if (gatunek == null)
            {
                return HttpNotFound();
            }
            return View(gatunek);
        }

        // GET: Gatuneks/Create
        public ActionResult Create()
        {
            var ids = from gatunek in db.Gatuneks
                      select gatunek;
            ViewBag.IdGatunek = (ids.Count() == 0) ? 1 : ids.ToArray().OrderBy(element => element.IdGatunek).LastOrDefault().IdGatunek + 1;
            return View();
        }

        public ActionResult CreateUsingProcedure()
        {
            var ids = from gatunek in db.Gatuneks
                                   select gatunek;
            ViewBag.IdGatunek = (ids.Count() == 0) ? 1 : ids.ToArray().OrderBy(element => element.IdGatunek).LastOrDefault().IdGatunek + 1;
            return View();
        }

        // POST: Gatuneks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGatunek,Nazwa")] Gatunek gatunek)
        {
            if (ModelState.IsValid)
            {
                db.Gatuneks.Add(gatunek);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane gatunku!";
                    }
                    else
                    {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    return View(gatunek);
                }
                return RedirectToAction("Index");
            }
            return View(gatunek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUsingProcedure([Bind(Include = "IdGatunek, Nazwa")] Gatunek gatunek)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.DodajGatunek(gatunek.IdGatunek, gatunek.Nazwa);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane gatunku!";
                    }
                    else
                    {
                        message = e.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    return View(gatunek);
                }
                return RedirectToAction("Index");
            }
            return View(gatunek);
        }

        // GET: Gatuneks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gatunek gatunek = db.Gatuneks.Find(id);
            if (gatunek == null)
            {
                return HttpNotFound();
            }
            return View(gatunek);
        }

        // POST: Gatuneks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGatunek,Nazwa")] Gatunek gatunek)
        {            
            if (ModelState.IsValid)
            {
                db.Entry(gatunek).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string message = "";

                    if (e.InnerException == null)
                    {
                        message = "Podano nieprawidłowe dane gatunku!";
                    }
                    else
                    {
                        message = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception = message;
                    return View(gatunek);
                }
                return RedirectToAction("Index");
            }
            return View(gatunek);
        }

        // GET: Gatuneks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gatunek gatunek = db.Gatuneks.Find(id);
            if (gatunek == null)
            {
                return HttpNotFound();
            }
            return View(gatunek);
        }

        // POST: Gatuneks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gatunek gatunek = db.Gatuneks.Find(id);
            db.Gatuneks.Remove(gatunek);
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
