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
    public class UbezpieczeniasController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Ubezpieczenias
        public ActionResult Index()
        {
            return View(db.Ubezpieczenia.ToList());
        }

        // GET: Ubezpieczenias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubezpieczenia ubezpieczenia = db.Ubezpieczenia.Find(id);
            if (ubezpieczenia == null)
            {
                return HttpNotFound();
            }
            return View(ubezpieczenia);
        }

        // GET: Ubezpieczenias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ubezpieczenias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUbezpieczenia,NazwaUbezpieczenia,WartoscUbezpieczenia")] Ubezpieczenia ubezpieczenia)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Ubezpieczenia.Add(ubezpieczenia);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(ubezpieczenia);
                }
                return RedirectToAction("Index");
            }

            return View(ubezpieczenia);
        }

        // GET: Ubezpieczenias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubezpieczenia ubezpieczenia = db.Ubezpieczenia.Find(id);
            if (ubezpieczenia == null)
            {
                return HttpNotFound();
            }
            return View(ubezpieczenia);
        }

        // POST: Ubezpieczenias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUbezpieczenia,NazwaUbezpieczenia,WartoscUbezpieczenia")] Ubezpieczenia ubezpieczenia)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(ubezpieczenia).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(ubezpieczenia);
                }
                return RedirectToAction("Index");
            }
            return View(ubezpieczenia);
        }

        // GET: Ubezpieczenias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubezpieczenia ubezpieczenia = db.Ubezpieczenia.Find(id);
            if (ubezpieczenia == null)
            {
                return HttpNotFound();
            }
            return View(ubezpieczenia);
        }

        // POST: Ubezpieczenias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ubezpieczenia ubezpieczenia = db.Ubezpieczenia.Find(id);
            db.Ubezpieczenia.Remove(ubezpieczenia);
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
