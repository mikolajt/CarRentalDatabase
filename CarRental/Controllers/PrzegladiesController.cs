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
    public class PrzegladiesController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Przegladies
        public ActionResult Index()
        {
            var przeglady = db.Przeglady.Include(p => p.Samochody);
            return View(przeglady.ToList());
        }

        // GET: Przegladies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przeglady przeglady = db.Przeglady.Find(id);
            if (przeglady == null)
            {
                return HttpNotFound();
            }
            return View(przeglady);
        }

        // GET: Przegladies/Create
        public ActionResult Create()
        {
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny");
            return View();
        }

        // POST: Przegladies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPrzegladu,IDSamochodu,DataPrzegladu,WaznoscPrzegladu")] Przeglady przeglady)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Przeglady.Add(przeglady);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(przeglady);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny", przeglady.IDSamochodu);
            return View(przeglady);
        }

        // GET: Przegladies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przeglady przeglady = db.Przeglady.Find(id);
            if (przeglady == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny", przeglady.IDSamochodu);
            return View(przeglady);
        }

        // POST: Przegladies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPrzegladu,IDSamochodu,DataPrzegladu,WaznoscPrzegladu")] Przeglady przeglady)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(przeglady).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(przeglady);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny", przeglady.IDSamochodu);
            return View(przeglady);
        }

        // GET: Przegladies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przeglady przeglady = db.Przeglady.Find(id);
            if (przeglady == null)
            {
                return HttpNotFound();
            }
            return View(przeglady);
        }

        // POST: Przegladies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przeglady przeglady = db.Przeglady.Find(id);
            db.Przeglady.Remove(przeglady);
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
