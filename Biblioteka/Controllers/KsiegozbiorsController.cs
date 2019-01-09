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
    public class KsiegozbiorsController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Ksiegozbiors
        public ActionResult Index()
        {
            return View(db.Ksiegozbiors.ToList());
        }

        // GET: Ksiegozbiors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiegozbior ksiegozbior = db.Ksiegozbiors.Find(id);
            if (ksiegozbior == null)
            {
                return HttpNotFound();
            }
            return View(ksiegozbior);
        }

        // GET: Ksiegozbiors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ksiegozbiors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImieAutora,NazwiskoAutora,Tytul,RokWydania,NazwaWydawnictwa,LiczbaEgzemplarzy")] Ksiegozbior ksiegozbior)
        {
            if (ModelState.IsValid)
            {
                db.Ksiegozbiors.Add(ksiegozbior);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ksiegozbior);
        }

        // GET: Ksiegozbiors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiegozbior ksiegozbior = db.Ksiegozbiors.Find(id);
            if (ksiegozbior == null)
            {
                return HttpNotFound();
            }
            return View(ksiegozbior);
        }

        // POST: Ksiegozbiors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImieAutora,NazwiskoAutora,Tytul,RokWydania,NazwaWydawnictwa,LiczbaEgzemplarzy")] Ksiegozbior ksiegozbior)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ksiegozbior).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ksiegozbior);
        }

        // GET: Ksiegozbiors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiegozbior ksiegozbior = db.Ksiegozbiors.Find(id);
            if (ksiegozbior == null)
            {
                return HttpNotFound();
            }
            return View(ksiegozbior);
        }

        // POST: Ksiegozbiors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ksiegozbior ksiegozbior = db.Ksiegozbiors.Find(id);
            db.Ksiegozbiors.Remove(ksiegozbior);
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
