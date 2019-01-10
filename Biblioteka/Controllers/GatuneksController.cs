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
                db.SaveChanges();
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
                db.SaveChanges();
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
