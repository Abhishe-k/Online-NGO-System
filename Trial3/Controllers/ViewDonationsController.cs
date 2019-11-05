using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trial3.Models;

namespace Trial3.Controllers
{
    [Authorize]
    public class ViewDonationsController : Controller
    {
        Model1 db = new Model1();
        
        // GET: ViewDonations
        public ActionResult Index()
        {
            string email = User.Identity.Name.ToString();
            var user = db.User.FirstOrDefault(x => x.EmailId == email);
            ViewBag.UserName = user.FullName.ToString();
            if(user==null)
            {
                return RedirectToAction("Index","Home");
            }
            var donation = db.UserDonation.Where(c => c.UserId == user.UserId).ToList();
            return View(donation.ToList());
        }
    }
}