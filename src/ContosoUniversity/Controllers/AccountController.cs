﻿using ContosoUniversity.Business;
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
            if (ModelState.IsValid)
            {
                UsernameValidation user = new UsernameValidation();
                if (user.UsernameIsAvailable(model.Login) == true && model.Role == "Student")

                {
                    AuthenticationBusiness student = new AuthenticationBusiness();
                    student.CreateNewStudent(model);
                    return View(model);
                }

                else if (user.UsernameIsAvailable(model.Login) == true && model.Role == "Instructor")

                {
                    AuthenticationBusiness instructor = new AuthenticationBusiness();
                    instructor.CreateNewInstructor(model);
                    return View(model);
                }
           

            }

            return View();


        }
        //Login
        public ActionResult Login()

        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)

        {
            AuthenticationBusiness user = new AuthenticationBusiness();
          
            if (user.LoginPerson(model) != null && model.Role=="Student")
            {
                Session["UserID"] = user;
                ViewBag.Message = "Welcome" + model.Login;
                return RedirectToAction("Student", "Index");
            }
            else if (user.LoginPerson(model) != null && model.Role== "Instructor")
            {
                Session["UserID"] = user;
                ViewBag.Message = "Welcome" + model.Login;
                return RedirectToAction("Instructor", "Index");
            }
            else
            {
                ViewBag.Message = "Login Or Password is wrong";
                return View();
            }
           
            
        }

        public ActionResult Logout()

        {
            Session["UserID"] = null;
            return RedirectToAction("Home","Index");
        }
    }
}