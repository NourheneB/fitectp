﻿using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Business;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.Services;

namespace ContosoUniversity.Tests.Tools
{
    public class EntityGenerator
    {
        private readonly SchoolContext dbContext;

        public EntityGenerator(SchoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public EntityGenerator()
        {
        }

        public Student CreateStudent(string lastname, string firstname, string login, string password)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname,
                Login = login,
                Password = password,
                EnrollmentDate = DateTime.Now,
            };

            DBUtils.db.Students.Add(student);
            DBUtils.db.SaveChanges();
            return student;
        }
        //CreatePerson for LoginPersonTest
        public Student CreatePersonWithLoginAndPassword(LoginVM model)
        {
            var student = new Student()
            {
                LastName = "lastname",
                FirstMidName = "firstname",
                Login = model.Login,
                Password = HashService.GenerateSHA256String(model.Password),
                EnrollmentDate = DateTime.Now
            };

            this.dbContext.Students.Add(student);
            dbContext.SaveChanges();
            return student;
        }
    }
}
