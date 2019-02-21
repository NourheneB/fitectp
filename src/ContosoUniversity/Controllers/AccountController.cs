using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
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

        [HttpPost]
        public JsonResult CheckUsername(string username)
        {
            UsernameValidation userV = new UsernameValidation();

            bool available = userV.UsernameIsAvailable(username);
            return Json(available);
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


            if (user != null && user is Student)
            {
                Session["User"] = user;
                TempData["LoginMessage"] = "Welcome " + model.Login;
                return RedirectToAction("Index","Student" );
            }
            else if (user != null && user is Instructor)
            {
                Session["User"] = user;
                TempData["LoginMessage"] = "Welcome " + model.Login;
                return RedirectToAction("Index","Instructor");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login or password");
                return View();
            }    
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}