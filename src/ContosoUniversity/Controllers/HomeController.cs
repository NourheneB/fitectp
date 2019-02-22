using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Enumeration;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        SchoolContext db = new SchoolContext();
        ScheduleBL scheduleBL = new ScheduleBL();
        LessonBL lessonBL = new LessonBL();

        public ActionResult Index()
        {
            if (true)
            {
                // Temporary the ID is set manually
                // waiting to Session["User"].ID applied everywhere

                WeeklyScheduleVM weeklySchedule = new WeeklyScheduleVM();
                weeklySchedule.Agenda = scheduleBL.GetWeeklyScheduleInstructor(10);
                weeklySchedule.UserName = lessonBL.GetInstructor(10).FullName;
                ViewBag.Lessons = scheduleBL.GetWeeklyScheduleInstructor(10);
                return View("IndexInstructor", weeklySchedule);
            }
            return View();
        }

        public ActionResult About()
        {
            // Commenting out LINQ to show how to do the same thing in SQL.
            //IQueryable<EnrollmentDateGroup> = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            // SQL version of the above LINQ code.
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);

            return View(data.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}