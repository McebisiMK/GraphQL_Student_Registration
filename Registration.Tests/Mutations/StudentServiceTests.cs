using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Services;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Tests.Mutations
{
    [TestFixture]
    public class StudentServiceTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Add_Given_Invalid_Student_Name_Should_Throw_Exception(string name)
        {
            //-----------------------Arrange----------------------------------
            var student = GetStudent();
            student.Name = name;
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => studentService.Add(student));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Student");
            await studentRepository.Received(0).Add(student);
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public async Task Add_Given_Invalid_Address_Id_And_Or_Course_Id_Should_Throw_Exception(int courseId, int addressId)
        {
            //-----------------------Arrange----------------------------------
            var student = GetStudent();
            student.CourseId = courseId;
            student.AddressId = addressId;
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => studentService.Add(student));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Student");
            await studentRepository.Received(0).Add(student);
            student.StudentNumber.Should().Be(null);
        }

        [Test]
        public async Task Add_Given_Non_Existing_Address_Id__Should_Throw_Exception()
        {
            //-----------------------Arrange----------------------------------
            var addressId = 99;
            var student = GetStudent();
            student.AddressId = addressId;
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.Exists(stud => stud.AddressId.Equals(addressId)).Returns(false);
            var exception = Assert.ThrowsAsync<InvalidForeignKeyException>(() => studentService.Add(student));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Given foreign key(s) does not exists: Student");
            await studentRepository.Received(0).Add(student);
            student.StudentNumber.Should().Be(null);
        }

        [Test]
        public async Task Add_Given_Non_Existing_Course_Id__Should_Throw_Exception()
        {
            //-----------------------Arrange----------------------------------
            var courseId = 99;
            var student = GetStudent();
            student.CourseId = courseId;
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.Exists(stud => stud.CourseId.Equals(courseId)).Returns(false);
            var exception = Assert.ThrowsAsync<InvalidForeignKeyException>(() => studentService.Add(student));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Given foreign key(s) does not exists: Student");
            await studentRepository.Received(0).Add(student);
            student.StudentNumber.Should().Be(null);
        }

        [Test]
        public async Task Add_Given_Valid_Student_Record_Should_Return_Newly_Added_Record()
        {
            //-----------------------Arrange----------------------------------
            var courseId = 1;
            var addressId = 1;
            var student = GetStudent();
            student.CourseId = courseId;
            student.AddressId = addressId;
            var students = GetStudents();
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.Exists(Arg.Any<Expression<Func<Student, bool>>>()).Returns(true);
            studentRepository.GetAll().Returns(students);
            await studentService.Add(student);

            //-----------------------Assert-----------------------------------
            student.StudentNumber.Length.Should().Be(10);
            await studentRepository.Received(1).Add(student);
        }

        private StudentService CreateStudentService(IStudentRepository studentRepository)
        {
            return new StudentService(studentRepository);
        }

        private Student GetStudent()
        {
            return new Student
                    {
                        Name = "Name",
                        Surname = "Surname",
                        AddressId = 1,
                        Cellphone = "0000000000",
                        IdNumber = "0000000000000",
                        CourseId = 1
                    };
        }

        private List<Student> GetStudents()
        {
            var date = DateTime.Now;
            var students = new List<Student>
                            {
                                new Student
                                {
                                    StudentNumber = $"{date.Year}{date.Month.ToString("00")}0001",
                                    Name = "Name",
                                    Surname = "Surname",
                                    AddressId = 1,
                                    Cellphone = "0000000000",
                                    IdNumber = "0000000000000",
                                    CourseId = 1
                                },
                                new Student
                                {
                                    StudentNumber = $"{date.Year}{date.Month.ToString("00")}0003",
                                    Name = "Name",
                                    Surname = "Surname",
                                    AddressId = 1,
                                    Cellphone = "0000000000",
                                    IdNumber = "0000000000000",
                                    CourseId = 1
                                }
                        };

            return students;
        }
    }
}
