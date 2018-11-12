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
    public class WypozyczalniesController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Wypozyczalnies
        public ActionResult Index()
        {
            return View(db.Wypozyczalnie.ToList());
        }

        // GET: Wypozyczalnies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczalnie wypozyczalnie = db.Wypozyczalnie.Find(id);
            if (wypozyczalnie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczalnie);
        }

        // GET: Wypozyczalnies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wypozyczalnies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDWypozyczalni,Adres,KodPocztowy,Miasto,Panstwo")] Wypozyczalnie wypozyczalnie)
        {

            ViewBag.Exception = null;
            if (ModelState.IsValid)
            {
                db.Wypozyczalnie.Add(wypozyczalnie);
                try {
                    db.SaveChanges();
                }
                catch(Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(wypozyczalnie);
                }
                return RedirectToAction("Index");
            }

            return View(wypozyczalnie);
        }

        // GET: Wypozyczalnies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczalnie wypozyczalnie = db.Wypozyczalnie.Find(id);
            if (wypozyczalnie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczalnie);
        }

        // POST: Wypozyczalnies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDWypozyczalni,Adres,KodPocztowy,Miasto,Panstwo")] Wypozyczalnie wypozyczalnie)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Exception = null;
                db.Entry(wypozyczalnie).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(wypozyczalnie);
                }
                return RedirectToAction("Index");
            }
            return View(wypozyczalnie);
        }

        // GET: Wypozyczalnies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczalnie wypozyczalnie = db.Wypozyczalnie.Find(id);
            if (wypozyczalnie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczalnie);
        }

        // POST: Wypozyczalnies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wypozyczalnie wypozyczalnie = db.Wypozyczalnie.Find(id);
            db.Wypozyczalnie.Remove(wypozyczalnie);
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
