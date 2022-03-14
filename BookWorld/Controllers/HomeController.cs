using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWorld.Models;

namespace BookWorld.Controllers
{
    public class HomeController : Controller
    {
        UserSignupLoginEntities objUserSignupLoginEntities = new UserSignupLoginEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserInfo userInfo)
        {
            if (objUserSignupLoginEntities.UserInfoes.Any(model => model.UserName == userInfo.UserName))
            {
                ViewBag.Notification = "This account already exists.";
                return View();

            }
            else 
            {
                objUserSignupLoginEntities.UserInfoes.Add(userInfo);
                objUserSignupLoginEntities.SaveChanges();

                Session["UserID"] = userInfo.UserID.ToString();
                Session["UserName"] = userInfo.UserName.ToString();
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}