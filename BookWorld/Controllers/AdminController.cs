using BookWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWorld.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        UserSignupLoginEntities objUserSignupLoginEntities = new UserSignupLoginEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}