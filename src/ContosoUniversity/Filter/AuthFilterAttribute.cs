using ContosoUniversity.Models;
using ContosoUniversity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ContosoUniversity.Filters
{
    public class AuthFilterAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase filterContext)
        {


            if (ConnexionService.GetSession() == null)
            {
                return false;
            }
            Person user = ConnexionService.GetSession();
            if (Role.ToString() == "Student" && !(user is Student))
            {
                return false;
            }
            if (Role.ToString() == "Instructor" && !(user is Instructor))
            {
                return false;
            }
            return true;
        }
    }
}