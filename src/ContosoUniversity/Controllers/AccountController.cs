using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()

        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(PersonVM model)

        {
            if(ModelState.IsValid)

            {
                if ( && model.Role == "Student")



                    return View();
        }

    }
}