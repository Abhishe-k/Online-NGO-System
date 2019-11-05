using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trial3.Models;

namespace Trial3.Controllers
{
    public class EventUsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: EventUsers
        public ActionResult Index()
        {
            var eventUser = db.EventUser.Include(e => e.Event).Include(e => e.User);
            return View(eventUser.ToList());
        }

        // GET: EventUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventUser eventUser = db.EventUser.Find(id);
            if (eventUser == null)
            {
                return HttpNotFound();
            }
            return View(eventUser);
        }

        // GET: EventUsers/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle");
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName");
            return View();
        }

        // POST: EventUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventUserId,UserId,EventId,Quantity")] EventUser eventUser)
        {
            if (ModelState.IsValid)
            {
                db.EventUser.Add(eventUser);
                db.SaveChanges();
                int quantity = eventUser.Quantity;
               
                Event events = db.Events.Find(eventUser.EventId);
                events.AllowedQuantity -= quantity;
                db.Events.AddOrUpdate(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle", eventUser.EventId);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName", eventUser.UserId);
            return View(eventUser);
        }

        // GET: EventUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventUser eventUser = db.EventUser.Find(id);
            if (eventUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle", eventUser.EventId);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName", eventUser.UserId);
            return View(eventUser);
        }

        // POST: EventUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventUserId,UserId,EventId,Quantity")] EventUser eventUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle", eventUser.EventId);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FullName", eventUser.UserId);
            return View(eventUser);
        }

        // GET: EventUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventUser eventUser = db.EventUser.Find(id);
            int eventid = eventUser.EventId;
            int quantity = eventUser.Quantity;
            var events = db.Events.Find(eventUser.EventId);
            events.AllowedQuantity += quantity;
            db.Events.AddOrUpdate(events);
            db.SaveChanges();
            if (eventUser == null)
            {
                return HttpNotFound();
            }
            return View(eventUser);
        }

        // POST: EventUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventUser eventUser = db.EventUser.Find(id);
            db.EventUser.Remove(eventUser);
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
