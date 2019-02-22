using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
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
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }
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
            if (ModelState.IsValid)
            {
                UsernameValidation user = new UsernameValidation();
                if (user.UsernameIsAvailable(model.Login) == true && model.Role == "Student")
                {
                    AuthenticationBusiness student = new AuthenticationBusiness();
                    student.CreateNewStudent(model);
                    ViewBag.MessageSuccess = "Registration successful !";
                    return RedirectToAction("Login");
                }

                else if (user.UsernameIsAvailable(model.Login) == true && model.Role == "Instructor")
                {
                    AuthenticationBusiness instructor = new AuthenticationBusiness();
                    instructor.CreateNewInstructor(model);
                    ViewBag.MessageSuccess = "Registration successful !";
                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            AuthenticationBusiness userBL = new AuthenticationBusiness();
            Person user = userBL.LoginPerson(model);

            if (user != null)
            {
                ConnexionService.SetSession(user);
                TempData["LoginMessage"] = "Welcome " + model.Login;
                if (user is Student)
                {
                    return RedirectToAction("Index", "Student");
                }
                else if(user is Instructor)
                {
                    return RedirectToAction("Index", "Instructor");
                }
            }
       
            else
            {
                ModelState.AddModelError("", "Invalid login or password");
                return View();
            }

            return View();

        }

        public ActionResult Logout()
        {
            ConnexionService.EmptySession();
            return RedirectToAction("Index", "Home");
        }
    }
}