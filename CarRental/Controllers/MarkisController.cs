using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class MarkisController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Markis
        public ActionResult Index()
        {
            return View(db.Marki.ToList());
        }

        // GET: Markis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marki marki = db.Marki.Find(id);
            if (marki == null)
            {
                return HttpNotFound();
            }
            return View(marki);
        }

        // GET: Markis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMarki,Marka,Model")] Marki marki)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Marki.Add(marki);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(marki);
                }
                return RedirectToAction("Index");
            }

            return View(marki);
        }

        // GET: Markis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marki marki = db.Marki.Find(id);
            if (marki == null)
            {
                return HttpNotFound();
            }
            return View(marki);
        }

        // POST: Markis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMarki,Marka,Model")] Marki marki)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(marki).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(marki);
                }
                return RedirectToAction("Index");
            }
            return View(marki);
        }

        // GET: Markis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marki marki = db.Marki.Find(id);
            if (marki == null)
            {
                return HttpNotFound();
            }
            return View(marki);
        }

        // POST: Markis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marki marki = db.Marki.Find(id);
            db.Marki.Remove(marki);
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
