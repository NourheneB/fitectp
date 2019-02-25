using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Enumeration;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        SchoolContext db = new SchoolContext();
        ScheduleBL scheduleBL = new ScheduleBL();
        LessonBL lessonBL = new LessonBL();
        CourseBL courseBL = new CourseBL();

        public ActionResult Index()
        {
            Person user = ConnexionService.GetSession();
            if (user == null)
            {

                ViewBag.CoursesNotLaunched = courseBL.GetNotLaunchedCourses();
                ViewBag.CoursesLaunched = courseBL.GetLaunchedCourses();
                return View();
            }
            else if (user is Instructor)
            {

                WeeklyScheduleVM weeklySchedule = new WeeklyScheduleVM();
                weeklySchedule.Agenda = scheduleBL.GetWeeklyScheduleInstructor(user.ID);
                weeklySchedule.UserName = user.FullName;
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