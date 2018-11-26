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
    public class UbezpieczeniaWypozyczeniesController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: UbezpieczeniaWypozyczenies
        public ActionResult Index()
        {
            var ubezpieczeniaWypozyczenie = db.UbezpieczeniaWypozyczenie.Include(u => u.Ubezpieczenia).Include(u => u.Wypozyczenia);
            return View(ubezpieczeniaWypozyczenie.ToList());
        }

        // GET: UbezpieczeniaWypozyczenies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UbezpieczeniaWypozyczenie ubezpieczeniaWypozyczenie = db.UbezpieczeniaWypozyczenie.Find(id);
            if (ubezpieczeniaWypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(ubezpieczeniaWypozyczenie);
        }

        // GET: UbezpieczeniaWypozyczenies/Create
        public ActionResult Create()
        {
            ViewBag.IDUbezpieczenia = new SelectList(db.Ubezpieczenia, "IDUbezpieczenia", "NazwaUbezpieczenia");
            ViewBag.IDWypozyczenia = new SelectList(db.Wypozyczenia, "IDWypozyczenia", "IDWypozyczenia");
            return View();
        }

        // POST: UbezpieczeniaWypozyczenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NrPolisy,IDWypozyczenia,IDUbezpieczenia")] UbezpieczeniaWypozyczenie ubezpieczeniaWypozyczenie)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.UbezpieczeniaWypozyczenie.Add(ubezpieczeniaWypozyczenie);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(ubezpieczeniaWypozyczenie);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IDUbezpieczenia = new SelectList(db.Ubezpieczenia, "IDUbezpieczenia", "NazwaUbezpieczenia", ubezpieczeniaWypozyczenie.IDUbezpieczenia);
            ViewBag.IDWypozyczenia = new SelectList(db.Wypozyczenia, "IDWypozyczenia", "IDWypozyczenia", ubezpieczeniaWypozyczenie.IDWypozyczenia);
            return View(ubezpieczeniaWypozyczenie);
        }

        // GET: UbezpieczeniaWypozyczenies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UbezpieczeniaWypozyczenie ubezpieczeniaWypozyczenie = db.UbezpieczeniaWypozyczenie.Find(id);
            if (ubezpieczeniaWypozyczenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUbezpieczenia = new SelectList(db.Ubezpieczenia, "IDUbezpieczenia", "NazwaUbezpieczenia", ubezpieczeniaWypozyczenie.IDUbezpieczenia);
            ViewBag.IDWypozyczenia = new SelectList(db.Wypozyczenia, "IDWypozyczenia", "IDWypozyczenia", ubezpieczeniaWypozyczenie.IDWypozyczenia);
            return View(ubezpieczeniaWypozyczenie);
        }

        // POST: UbezpieczeniaWypozyczenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NrPolisy,IDWypozyczenia,IDUbezpieczenia")] UbezpieczeniaWypozyczenie ubezpieczeniaWypozyczenie)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(ubezpieczeniaWypozyczenie).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(ubezpieczeniaWypozyczenie);
                }
                return RedirectToAction("Index");
            }
            ViewBag.IDUbezpieczenia = new SelectList(db.Ubezpieczenia, "IDUbezpieczenia", "NazwaUbezpieczenia", ubezpieczeniaWypozyczenie.IDUbezpieczenia);
            ViewBag.IDWypozyczenia = new SelectList(db.Wypozyczenia, "IDWypozyczenia", "IDWypozyczenia", ubezpieczeniaWypozyczenie.IDWypozyczenia);
            return View(ubezpieczeniaWypozyczenie);
        }

        // GET: UbezpieczeniaWypozyczenies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UbezpieczeniaWypozyczenie ubezpieczeniaWypozyczenie = db.UbezpieczeniaWypozyczenie.Find(id);
            if (ubezpieczeniaWypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(ubezpieczeniaWypozyczenie);
        }

        // POST: UbezpieczeniaWypozyczenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UbezpieczeniaWypozyczenie ubezpieczeniaWypozyczenie = db.UbezpieczeniaWypozyczenie.Find(id);
            db.UbezpieczeniaWypozyczenie.Remove(ubezpieczeniaWypozyczenie);
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
