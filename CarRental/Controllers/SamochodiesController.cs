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
    public class SamochodiesController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Samochodies
        public ActionResult Index()
        {
            var samochody = db.Samochody.Include(s => s.Marki).Include(s => s.Wypozyczalnie);
            return View(samochody.ToList());
        }

        // GET: Samochodies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochody samochody = db.Samochody.Find(id);
            if (samochody == null)
            {
                return HttpNotFound();
            }
            return View(samochody);
        }

        // GET: Samochodies/Create
        public ActionResult Create()
        {
            ViewBag.IDMarki = new SelectList(db.Marki, "IDMarki", "Marka");
            ViewBag.IDWypozyczalni = new SelectList(db.Wypozyczalnie, "IDWypozyczalni", "Adres");
            return View();
        }

        // POST: Samochodies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSamochodu,IDWypozyczalni,IDMarki,Rocznik,CenaZaDobe,NrRejestracyjny")] Samochody samochody)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Samochody.Add(samochody);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(samochody);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IDMarki = new SelectList(db.Marki, "IDMarki", "Marka", samochody.IDMarki);
            ViewBag.IDWypozyczalni = new SelectList(db.Wypozyczalnie, "IDWypozyczalni", "Adres", samochody.IDWypozyczalni);
            return View(samochody);
        }

        // GET: Samochodies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochody samochody = db.Samochody.Find(id);
            if (samochody == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMarki = new SelectList(db.Marki, "IDMarki", "Marka", samochody.IDMarki);
            ViewBag.IDWypozyczalni = new SelectList(db.Wypozyczalnie, "IDWypozyczalni", "Adres", samochody.IDWypozyczalni);
            return View(samochody);
        }

        // POST: Samochodies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSamochodu,IDWypozyczalni,IDMarki,Rocznik,CenaZaDobe,NrRejestracyjny")] Samochody samochody)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(samochody).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(samochody);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IDMarki = new SelectList(db.Marki, "IDMarki", "Marka", samochody.IDMarki);
            ViewBag.IDWypozyczalni = new SelectList(db.Wypozyczalnie, "IDWypozyczalni", "Adres", samochody.IDWypozyczalni);
            return View(samochody);
        }

        // GET: Samochodies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochody samochody = db.Samochody.Find(id);
            if (samochody == null)
            {
                return HttpNotFound();
            }
            return View(samochody);
        }

        // POST: Samochodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samochody samochody = db.Samochody.Find(id);
            db.Samochody.Remove(samochody);
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
