using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
        public Person CreateNewStudent(PersonVM model)
        {
            Student newStudent = new Student
            {
                LastName = model.LastName,
                FirstMidName = model.FirstMidName,
                Login = model.Login,
                Password = HashService.GenerateSHA256String(model.Password),
                EnrollmentDate = DateTime.Now
            };

            db.Students.Add(newStudent);
            db.SaveChanges();
            return newStudent;
        }
        public void CreateNewInstructor(PersonVM model)
        {
            Instructor newInstructor = new Instructor
            {
                LastName = model.LastName,
                FirstMidName = model.FirstMidName,
                Login = model.Login,
                Password = HashService.GenerateSHA256String(model.Password),
                HireDate = DateTime.Now
            };

            db.Instructors.Add(newInstructor);


            db.SaveChanges();
        }

        public Person LoginPerson(LoginVM model)
        {
            string EncryptPass = HashService.GenerateSHA256String(model.Password);
            Person user = db.People.FirstOrDefault(u => u.Login == model.Login && u.Password == EncryptPass);
            return (user);
        }


    }
}