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
    public class WydawnictwoesController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Wydawnictwoes
        public ActionResult Index()
        {
            return View(db.Wydawnictwoes.ToList());
        }

        // GET: Wydawnictwoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydawnictwo wydawnictwo = db.Wydawnictwoes.Find(id);
            if (wydawnictwo == null)
            {
                return HttpNotFound();
            }
            return View(wydawnictwo);
        }

        // GET: Wydawnictwoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wydawnictwoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdWydawnictwo,Nazwa")] Wydawnictwo wydawnictwo)
        {
            if (ModelState.IsValid)
            {
                db.Wydawnictwoes.Add(wydawnictwo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wydawnictwo);
        }

        // GET: Wydawnictwoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydawnictwo wydawnictwo = db.Wydawnictwoes.Find(id);
            if (wydawnictwo == null)
            {
                return HttpNotFound();
            }
            return View(wydawnictwo);
        }

        // POST: Wydawnictwoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdWydawnictwo,Nazwa")] Wydawnictwo wydawnictwo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wydawnictwo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wydawnictwo);
        }

        // GET: Wydawnictwoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wydawnictwo wydawnictwo = db.Wydawnictwoes.Find(id);
            if (wydawnictwo == null)
            {
                return HttpNotFound();
            }
            return View(wydawnictwo);
        }

        // POST: Wydawnictwoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wydawnictwo wydawnictwo = db.Wydawnictwoes.Find(id);
            db.Wydawnictwoes.Remove(wydawnictwo);
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
