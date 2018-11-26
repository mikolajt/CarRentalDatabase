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
    public class PracowniciesController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Pracownicies
        public ActionResult Index()
        {
            return View(db.Pracownicy.ToList());
        }

        // GET: Pracownicies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // GET: Pracownicies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pracownicies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPracownika,Imie,Nazwisko,Etat,NrTelefonu,ZatrudnionyOd,ZatrudnionyDo")] Pracownicy pracownicy)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Pracownicy.Add(pracownicy);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(pracownicy);
                }
                return RedirectToAction("Index");
            }

            return View(pracownicy);
        }

        // GET: Pracownicies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: Pracownicies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPracownika,Imie,Nazwisko,Etat,NrTelefonu,ZatrudnionyOd,ZatrudnionyDo")] Pracownicy pracownicy)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(pracownicy).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(pracownicy);
                }
                return RedirectToAction("Index");
            }
            return View(pracownicy);
        }

        // GET: Pracownicies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: Pracownicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            db.Pracownicy.Remove(pracownicy);
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
