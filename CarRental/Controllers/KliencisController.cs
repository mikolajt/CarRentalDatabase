using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using CarRental.Models;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

namespace CarRental.Controllers
{
    public class KliencisController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Kliencis
        public ActionResult Index()
        {
            return View(db.Klienci.ToList());
        }

        // GET: Kliencis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // GET: Kliencis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kliencis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKlienta,NazwaFirmy,Imie,Nazwisko,Adres,KodPocztowy,Miasto,Panstwo,NrTelefonu,Email")] Klienci klienci)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid) {
                db.Klienci.Add(klienci);
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(klienci);
                }
                return RedirectToAction("Index");
            }

            return View(klienci);
        }

        // GET: Kliencis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Kliencis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKlienta,NazwaFirmy,Imie,Nazwisko,Adres,KodPocztowy,Miasto,Panstwo,NrTelefonu,Email")] Klienci klienci)
        {
            if (ModelState.IsValid) {
                ViewBag.Exception = null;
                db.Entry(klienci).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                }
                catch (Exception) {
                    ViewBag.Exception = "Wprowadzono niepoprawne dane";
                    return View(klienci);
                }
                return RedirectToAction("Index");
            }
            return View(klienci);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion) {
            string[] fieldsToBind = new string[] { "IDKlienta", "NazwaFirmy", "Imie", "Nawisko", "Adres", "KodPocztowy", "Miasto", "Panstwo", "NrTelefonu", "Email", "RowVersion" };

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var KlientToUpdate = await db.Klienci.FindAsync(id);
            if (KlientToUpdate == null) {
                Klienci deletedKlient = new Klienci();
                TryUpdateModel(deletedKlient, fieldsToBind);
                ModelState.AddModelError(string.Empty, "Nie można zapisać zmian, klient został usunięty przez innego użytkownika.");
                return View(deletedKlient);
            }
            if (TryUpdateModel(KlientToUpdate, fieldsToBind)) {
                try {
                    db.Entry(KlientToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex) {
                    var entry = ex.Entries.Single();
                    var clientValues = (Klienci)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null) {
                        ModelState.AddModelError(string.Empty, "Nie można zapisać zmian, klient został usunięty przez innego użytkownika.");
                    }
                    else {
                        var databaseValues = (Klienci)databaseEntry.ToObject();

                        if (databaseValues.IDKlienta != clientValues.IDKlienta)
                            ModelState.AddModelError("IDKlienta", "Obecna wartość: "
                                + databaseValues.IDKlienta);
                        if (databaseValues.NazwaFirmy != clientValues.NazwaFirmy)
                            ModelState.AddModelError("Nazwa Firmy", "Obecna wartość: "
                                + databaseValues.NazwaFirmy);
                        if (databaseValues.Imie != clientValues.Imie)
                            ModelState.AddModelError("Imie", "Obecna wartość: "
                                + databaseValues.Imie);
                        if (databaseValues.Nazwisko != clientValues.Nazwisko)
                            ModelState.AddModelError("Nazwisko", "Obecna wartość: "
                                + databaseValues.Nazwisko);
                        if (databaseValues.Adres != clientValues.Adres)
                            ModelState.AddModelError("Adres", "Obecna wartość: "
                                + databaseValues.Adres);
                        if (databaseValues.KodPocztowy != clientValues.KodPocztowy)
                            ModelState.AddModelError("KodPocztowy", "Obecna wartość: "
                                + databaseValues.KodPocztowy);
                        if (databaseValues.Miasto != clientValues.Miasto)
                            ModelState.AddModelError("Miasto", "Obecna wartość: "
                                + databaseValues.Miasto);
                        if (databaseValues.Panstwo != clientValues.Panstwo)
                            ModelState.AddModelError("Panstwo", "Obecna wartość: "
                                + databaseValues.Panstwo);
                        if (databaseValues.NrTelefonu != clientValues.NrTelefonu)
                            ModelState.AddModelError("NrTelefonu", "Obecna wartość: "
                                + databaseValues.NrTelefonu);
                        if (databaseValues.Email != clientValues.Email)
                            ModelState.AddModelError("Email", "Obecna wartość: "
                                + databaseValues.Email);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value.");
                        KlientToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */) {
                    //Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(KlientToUpdate);
        }

        // GET: Kliencis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Kliencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klienci klienci = db.Klienci.Find(id);
            db.Klienci.Remove(klienci);
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
