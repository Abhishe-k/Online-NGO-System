using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Trial3.Models;

namespace Trial3.Controllers
{
    [Authorize]
    public class ViewEventsController : Controller
    {
        Model1 db = new Model1();
        // GET: ViewEvents
        public ActionResult Index()
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user1 = userManager.FindByNameAsync(User.Identity.Name).Result;

            string email = User.Identity.Name.ToString();
            var user = db.User.FirstOrDefault(x => x.EmailId == email);
            if (user == null)
            {
                ViewBag.UserName = user1.UserName.ToString();
                return RedirectToAction("Create", "Users");
            }
            else
            {
                ViewBag.UserName = user.FullName.ToString();
                ViewData["UserId"] = user.UserId;
                var events = db.Events.Where(x => (x.EventDate.Year>=DateTime.Now.Year)&&(x.EventDate.Month >=DateTime.Now.Month) &&(x.EventDate.Day>=DateTime.Now.Day));
                if(events!=null)
                return View(events.ToList());
                return RedirectToAction("Index", "Home");
                
            }
        }
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }
        public ActionResult getQuantity(int event_id)
        {
            var eventinfo = db.Events.Find(event_id);
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user1 = userManager.FindByNameAsync(User.Identity.Name).Result;
            string username = user1.UserName.ToString();
            var checkuser = db.User.FirstOrDefault(x => x.EmailId == username);
            Console.WriteLine(checkuser.UserId);
            int id = checkuser.UserId;
            Console.WriteLine(id);
            if(checkuser!=null)
            ViewBag.UserId = checkuser.UserId;
            ViewBag.EventId = event_id;
            return View();
           // return View("","","");

        }
        [HttpPost]
        public ActionResult getQuantity(int UserId,int EventId,int Quantity)
        {
            
            if (ModelState.IsValid)
            {
                var eventinfo = db.Events.Find(EventId);
                if (Quantity < eventinfo.AllowedQuantity)
                {
                    EventUser eventUser = new EventUser();
                    eventUser.EventId = EventId;
                    eventUser.UserId = UserId;
                    eventUser.Quantity = Quantity;
                    db.EventUser.Add(eventUser);
                    db.SaveChanges();
                    int quantity = eventUser.Quantity;
                    Event events = db.Events.Find(eventUser.EventId);
                    events.AllowedQuantity -= quantity;
                    db.Events.AddOrUpdate(events);
                    db.SaveChanges();
                    var user = db.User.Find(UserId);
                    string umail = user.EmailId.ToString();
                    MailMessage mail = new MailMessage();
                    mail.To.Add(umail);
                    mail.From = new MailAddress("tedpoh98@gmail.com");
                    string subject = "Hello" + user.FullName.ToString();
                    subject += "<br/>Show this email  at the event venue.";
                    subject += "<br/>PASS for " + quantity + " for the event " + eventinfo.EventTitle.ToString() + " .";
                    subject += " <br/>Please Remain Present at " + events.Venue.ToString() + " at half hours early than  " + events.StartTime.ToString() + " on " + events.EventDate ;
                    mail.IsBodyHtml = true;
                   
                    mail.Subject = "Event Pass for"+eventinfo.EventTitle.ToString();
                    string Body = subject;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("tedpoh98@gmail.com", "972590533onE"); // Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            
            
            }
            return RedirectToAction("Show");
        }
        public ActionResult Show()
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user1 = userManager.FindByNameAsync(User.Identity.Name).Result;
            string username = user1.UserName.ToString();
            var checkuser = db.User.FirstOrDefault(x => x.EmailId == username);
            Console.WriteLine(checkuser.UserId);
            int id = checkuser.UserId;
            var userevents = db.EventUser.Where(x => x.UserId == checkuser.UserId).ToList();
            if (userevents != null)
                return View(userevents.ToList());
            else
                return RedirectToAction("Index");
        }
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
            
            db.EventUser.Remove(eventUser);
            db.SaveChanges();
            if (eventUser == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
} 