using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ContosoUniversity.Services
{
    public class ConnexionService
    {
     
        public static void SetSession(Person person)
        {
             HttpContext.Current.Session["User"] = person;
        }
        public static Person GetSession()
        {
            return (Person)HttpContext.Current.Session["User"];
        }
            public static void EmptySession()
        {
            HttpContext.Current.Session["User"] = null;
        }
    }
}