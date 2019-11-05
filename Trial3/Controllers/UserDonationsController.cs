using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trial3.Models;

namespace Trial3.Controllers
{
    public class UserDonationsController : Controller
    {
        private Model1 db = new Model1();

        // GET: UserDonations
        public ActionResult Index()
        {
            var userDonation = db.UserDonation.Include(u => u.Items).Include(u => u.User);
            return View(userDonation.ToList());
        }

        // GET: UserDonations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDonation userDonation = db.UserDonation.Find(id);
            if (userDonation == null)
            {
                return HttpNotFound();
            }
            return View(userDonation);
        }

        // GET: UserDonations/Create
        public ActionResult Create()
        {
            ViewBag.ItemsId = new SelectList(db.Item, "ItemsId", "ItemsName");
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName");
            return View();
        }

        // POST: UserDonations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserDonationId,UserId,ItemsId,Quantity,Description")] UserDonation userDonation)
        {
            if (ModelState.IsValid)
            {
                db.UserDonation.Add(userDonation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemsId = new SelectList(db.Item, "ItemsId", "ItemsName", userDonation.ItemsId);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName", userDonation.UserId);
            return View(userDonation);
        }

        // GET: UserDonations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDonation userDonation = db.UserDonation.Find(id);
            if (userDonation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemsId = new SelectList(db.Item, "ItemsId", "ItemsName", userDonation.ItemsId);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName", userDonation.UserId);
            return View(userDonation);
        }

        // POST: UserDonations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserDonationId,UserId,ItemsId,Quantity,Description")] UserDonation userDonation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDonation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemsId = new SelectList(db.Item, "ItemsId", "ItemsName", userDonation.ItemsId);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName", userDonation.UserId);
            return View(userDonation);
        }

        // GET: UserDonations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDonation userDonation = db.UserDonation.Find(id);
            if (userDonation == null)
            {
                return HttpNotFound();
            }
            return View(userDonation);
        }

        // POST: UserDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDonation userDonation = db.UserDonation.Find(id);
            db.UserDonation.Remove(userDonation);
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
