using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trial3.Models;
using System.Net.Http;
using System.Net;
using System.Net.Mail;

namespace Trial3.Controllers
{
    [Authorize]
    public class DonateController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Model1 db1 = new Model1();

        // GET: Donate
        public ActionResult Index()
        {
            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var s = UserManager.GetRoles(user.GetUserId());
              
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user1 = userManager.FindByNameAsync(User.Identity.Name).Result;
            string username = user1.UserName.ToString();
            var checkuser = db1.User.FirstOrDefault(x => x.EmailId == username);
            if (checkuser==null)
            {
                ViewBag.UserName = user1.UserName.ToString();
               
                return RedirectToAction("Create","Users");
            }
            ViewBag.ItemsId = new SelectList(db1.Item, "ItemsId", "ItemsName");
            return RedirectToAction("SelectItem","Donate");
         }
        public ActionResult SelectItem()
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user1 = userManager.FindByNameAsync(User.Identity.Name).Result;
            string username = user1.UserName.ToString();
            var checkuser = db1.User.FirstOrDefault(x => x.EmailId == username);
            Console.WriteLine(checkuser.UserId);
            int id = checkuser.UserId;
            Console.WriteLine(id);
            ViewBag.UserId = checkuser.UserId;
            ViewBag.ItemsId = new SelectList(db1.Item, "ItemsId", "ItemsName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectItem([Bind(Include = "UserDonationId,UserId,ItemsId,Quantity,Description")] UserDonation userDonation)
        {
            if (ModelState.IsValid)
            {
                /*int id =int.Parse( Request.Params["UserId"]);
                userDonation.UserId = id;*/
                db1.UserDonation.Add(userDonation);
                db1.SaveChanges();
                int q = userDonation.Quantity;
                int id = userDonation.ItemsId;
                var item = db1.Item.Find(id);
                string itemname = item.ItemsName.ToString();
                var user = db1.User.Find(userDonation.UserId);
                string addr = user.Address.ToString();
                string umail = user.EmailId.ToString();
                MailMessage mail = new MailMessage();
                mail.To.Add(umail);
                mail.From = new MailAddress("tedpoh98@gmail.com");
                string subject = "Hello" + user.FullName.ToString()+"<br/>";
                subject += "Our PickUp guy will come to your address<br/>" +addr +" within 3 days <hr/>";
                subject += "Your Donation Summary :<br/>";
                subject += "Item : " + itemname + "<br/>";
                subject += "Quantity :" + q + "<br/>";
                subject += "Your contribution will make someone smile";
                mail.IsBodyHtml = true;

                mail.Subject = itemname+" Donation, Give Foundation";
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

                return RedirectToAction("Index","ViewDonations");//Name of this view is View Donation
            }
            /*var ud = userDonation;
            ViewBag.ItemsId = new SelectList(db1.Item, "ItemsId", "ItemsName", userDonation.ItemsId);
            ViewBag.UserId = new SelectList(db1.User, "UserId", "FullName", userDonation.UserId);*/
            return RedirectToAction("Index","Home");
        }

        public ActionResult Select(int id,int id1)
        {
            ViewBag.itemid = id;
            ViewBag.userid = id1;
            return View();
        }

   }
}