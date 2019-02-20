using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Business
{
    public class AuthenticationBusiness
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }
        public void CreateNewStudent(PersonVM model)
        {
           Student newStudent = new Student
            {
               LastName = model.LastName,
               FirstMidName = model.FirstMidName,
               Login = model.Login,
               Password = model.Password,
               EnrollmentDate = DateTime.Now
            };

            db.Students.Add(newStudent);
            db.SaveChanges();
        }
        public void CreateNewInstructor(PersonVM model)
        {
            Instructor newInstructor = new Instructor
            {
                LastName = model.LastName,
                FirstMidName = model.FirstMidName,
                Login = model.Login,
                Password = model.Password,
                HireDate = DateTime.Now
            };

            db.Instructors.Add(newInstructor);


            db.SaveChanges();
        }


    }
}