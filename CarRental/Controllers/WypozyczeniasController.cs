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
    public class WypozyczeniasController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Wypozyczenias
        public ActionResult Index()
        {
            var wypozyczenia = db.Wypozyczenia.Include(w => w.Klienci).Include(w => w.Pracownicy).Include(w => w.Samochody);
            return View(wypozyczenia.ToList());
        }

        // GET: Wypozyczenias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia wypozyczenia = db.Wypozyczenia.Find(id);
            if (wypozyczenia == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia);
        }

        // GET: Wypozyczenias/Create
        public ActionResult Create()
        {
            ViewBag.IDKlienta = new SelectList(db.Klienci, "IDKlienta", "NazwaFirmy");
            ViewBag.IDPracownika = new SelectList(db.Pracownicy, "IDPracownika", "Imie");
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny");
            return View();
        }

        // POST: Wypozyczenias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDWypozyczenia,IDPracownika,IDSamochodu,IDKlienta,DataWypozyczenia,DataZwrotu,DataFaktycznegoZwrotu,Kilometry")] Wypozyczenia wypozyczenia)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Wypozyczenia.Add(wypozyczenia);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(wypozyczenia);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IDKlienta = new SelectList(db.Klienci, "IDKlienta", "NazwaFirmy", wypozyczenia.IDKlienta);
            ViewBag.IDPracownika = new SelectList(db.Pracownicy, "IDPracownika", "Imie", wypozyczenia.IDPracownika);
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny", wypozyczenia.IDSamochodu);
            return View(wypozyczenia);
        }

        // GET: Wypozyczenias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia wypozyczenia = db.Wypozyczenia.Find(id);
            if (wypozyczenia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKlienta = new SelectList(db.Klienci, "IDKlienta", "NazwaFirmy", wypozyczenia.IDKlienta);
            ViewBag.IDPracownika = new SelectList(db.Pracownicy, "IDPracownika", "Imie", wypozyczenia.IDPracownika);
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny", wypozyczenia.IDSamochodu);
            return View(wypozyczenia);
        }

        // POST: Wypozyczenias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDWypozyczenia,IDPracownika,IDSamochodu,IDKlienta,DataWypozyczenia,DataZwrotu,DataFaktycznegoZwrotu,Kilometry")] Wypozyczenia wypozyczenia)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(wypozyczenia).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(wypozyczenia);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IDKlienta = new SelectList(db.Klienci, "IDKlienta", "NazwaFirmy", wypozyczenia.IDKlienta);
            ViewBag.IDPracownika = new SelectList(db.Pracownicy, "IDPracownika", "Imie", wypozyczenia.IDPracownika);
            ViewBag.IDSamochodu = new SelectList(db.Samochody, "IDSamochodu", "NrRejestracyjny", wypozyczenia.IDSamochodu);
            return View(wypozyczenia);
        }

        // GET: Wypozyczenias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia wypozyczenia = db.Wypozyczenia.Find(id);
            if (wypozyczenia == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia);
        }

        // POST: Wypozyczenias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wypozyczenia wypozyczenia = db.Wypozyczenia.Find(id);
            db.Wypozyczenia.Remove(wypozyczenia);
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
