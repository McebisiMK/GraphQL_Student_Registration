using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Registration.Entities.Seeding
{
    public static class RegistrationInitialData
    {
        //private
        public static void Seed(this RegistrationsDBContext registrationsDBContext)
        {

            if (!registrationsDBContext.Student.Any())
            {
                registrationsDBContext.Address.Add(GetAddressInitialData());
                registrationsDBContext.Course.Add(GetCourseInitialData());
                registrationsDBContext.SaveChanges();

                var addressId = registrationsDBContext.Address.FirstOrDefault(x => x.Unit.Equals("Unit")).Id;
                var courseId = registrationsDBContext.Course.FirstOrDefault(x => x.Name.Equals("Course Name")).Id;

                registrationsDBContext.Student.AddRange(GetStudentInitialData(addressId, courseId));
                registrationsDBContext.SaveChanges();

                registrationsDBContext.Subject.AddRange(GetSubjectInitialData(courseId));
                registrationsDBContext.SaveChanges();
            }
        }

        private static List<Student> GetStudentInitialData(int addressId, int courseId)
        {
            return new List<Student>
            {
                new Student
                {
                    StudentNumber = $"{DateTime.Now.Year}{DateTime.Now.Month.ToString("00")}0001",
                    Name = "First Name",
                    Surname = "Last Name",
                    AddressId = addressId,
                    Cellphone = "0123 456 7890",
                    IdNumber = "123456 7890 12 3",
                    CourseId = courseId
                },
                new Student
                {
                    StudentNumber = $"{DateTime.Now.Year}{DateTime.Now.Month.ToString("00")}0002",
                    Name = "First Name 1",
                    Surname = "Last Name 1",
                    AddressId = addressId,
                    Cellphone = "0123 456 7891",
                    IdNumber = "123456 7890 12 4",
                    CourseId = courseId
                }
            };
        }

        private static Address GetAddressInitialData()
        {
            return new Address
            {
                Unit = "Unit",
                Street = "Street",
                Town = "Town",
                Province = "Province"
            };
        }

        private static Course GetCourseInitialData()
        {
            return new Course
            {
                Name = "Course Name"
            };
        }

        private static List<Subject> GetSubjectInitialData(int courseId)
        {
            return new List<Subject>
            {
                new Subject
                {
                    Name = "Subject Name",
                    CourseId = courseId,
                    Semester = Semester.First
                },
                new Subject
                {
                    Name = "Subject Name 2",
                    CourseId = courseId,
                    Semester = Semester.Second
                },
                new Subject
                {
                    Name = "Subject Name 3",
                    CourseId = courseId,
                    Semester = Semester.Second
                }
            };
        }
    }
}
