using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public abstract class FatherOfControllers : Controller
    {
        protected SchoolContext db = DBUtils.db;
    }
}