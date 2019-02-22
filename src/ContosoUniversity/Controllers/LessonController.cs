using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Enumeration;
using ContosoUniversity.Services;
using ContosoUniversity.Constants;

namespace ContosoUniversity.Controllers
{
    public class LessonController : Controller
    {
        private SchoolContext db = new SchoolContext();
        private LessonBL lessonBL = new LessonBL();
        private CourseBL courseBL = new CourseBL();

        // GET: Lessons
        public ActionResult Index()
        {
            return View(lessonBL.GetAllLessons().ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            LessonVM model = new LessonVM();
            model.Course = courseBL.GetAllCourses();
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseDay Day, string Course, DateTime StartHour, DateTime EndHour, DateTime DateStart)
        {

            if (ModelState.IsValid)
            {
                // remplace with connected user

                 Instructor instructor = lessonBL.GetInstructor(10);

                // Check endhour > starthour
                if (DateTime.Compare(StartHour, EndHour) > 0)
                {
                    TempData["CreateError"] = LessonConstants.ERRORS_STARTHOUR_AFTER_ENDHOUR;
                    return RedirectToAction(nameof(LessonController.Create), "Lesson");
                }

                if(StartHour.Hour < 8 || StartHour.Hour > 18)
                {
                    TempData["CreateError"] = LessonConstants.ERROR_STARTHOUR;
                    return RedirectToAction(nameof(LessonController.Create), "Lesson");
                }

                if (EndHour.Hour < 9 || EndHour.Hour > 19)
                {
                    TempData["CreateError"] = LessonConstants.ERROR_ENDHOUR;
                    return RedirectToAction(nameof(LessonController.Create), "Lesson");
                }

                Lesson lesson = lessonBL.CreateLesson(instructor, Day, Course, StartHour, EndHour, DateStart);
                // vérification d'agenda
                if (!lessonBL.IsPlanningCreationValid(lesson))
                {
                    TempData["CreateError"] = $"{LessonConstants.ERRORS_STARTHOUR_AFTER_ENDHOUR} {TimeService.GetHourFromDate(StartHour)} h and {TimeService.GetHourFromDate(EndHour)} h {Day}";
                    return RedirectToAction(nameof(LessonController.Create), "Lesson");
                }

                lessonBL.AddLesson(lesson);


                // appel BL de creation
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(LessonController.Create));
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = lessonBL.GetLesson(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }


        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = lessonBL.GetLesson(id);


            if (lesson == null)
            {
                return HttpNotFound();
            }

            LessonEditVM lessonToEdit = new LessonEditVM();
            lessonToEdit.LessonId = lesson.LessonID;
            lessonToEdit.Instructor = lesson.Instructor;
            lessonToEdit.Course = lesson.Course;
            lessonToEdit.Day = lesson.Day;
            lessonToEdit.StartHour = lesson.StartHour;
            lessonToEdit.EndHour = lesson.EndHour;
            lessonToEdit.DateStart = lesson.DateStart;

            // TODO userConnected ID
            ViewBag.InstructorID = 10;
            return View(lessonToEdit);
        }

        // POST: Lessons/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LessonEditVM lessonEdit, int courseID, int instructorID, int lessonID)
        {

            if (ModelState.IsValid)
            {
                lessonEdit.StartHour = lessonBL.HarmonizeDatetime(lessonEdit.StartHour);
                lessonEdit.EndHour = lessonBL.HarmonizeDatetime(lessonEdit.EndHour);
                Lesson lesson = lessonBL.GetLesson(lessonID);
                lesson.StartHour = lessonEdit.StartHour;
                lesson.EndHour = lessonEdit.EndHour;

                // Check endhour > starthour
                if (DateTime.Compare(lesson.StartHour, lesson.EndHour) > 0)
                {
                    TempData["CreateError"] = LessonConstants.ERRORS_STARTHOUR_AFTER_ENDHOUR;
                    return RedirectToAction(nameof(LessonController.Edit), "Lesson", new { id = lessonID });
                }

                if (lesson.StartHour.Hour < 8 || lesson.StartHour.Hour > 18)
                {
                    TempData["CreateError"] = LessonConstants.ERROR_STARTHOUR;
                    return RedirectToAction(nameof(LessonController.Edit), "Lesson", new { id = lessonID });
                }

                if (lesson.EndHour.Hour < 9 || lesson.EndHour.Hour > 19)
                {
                    TempData["CreateError"] = LessonConstants.ERROR_ENDHOUR;
                    return RedirectToAction(nameof(LessonController.Edit), "Lesson", new { id = lessonID });
                }


                if (!lessonBL.IsPlanningEditValid(lesson))
                {
                    TempData["CreateError"] = $"{LessonConstants.ERRORS_STARTHOUR_AFTER_ENDHOUR}  {TimeService.GetHourFromDate(lessonEdit.StartHour)} h and {TimeService.GetHourFromDate(lessonEdit.EndHour)} h {lessonEdit.Day}";
                    return RedirectToAction(nameof(LessonController.Edit), "Lesson", new { id = lessonID });
                }
                lessonBL.EditLesson(lesson, lessonEdit);

                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(LessonController.Edit), "Lesson", new { id = lessonID });
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = lessonBL.GetLesson(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = lessonBL.GetLesson(id);
            lessonBL.DeleteLesson(lesson);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
