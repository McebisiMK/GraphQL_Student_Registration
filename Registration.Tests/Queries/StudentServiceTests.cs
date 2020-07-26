using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Services;
using Registration.Utilities.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Tests.Queries
{
    [TestFixture]
    public class StudentServiceTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task GetByStudentNumber_Given_Invalid_Student_Number_Should_Throw_Exception(string studentNumber)
        {
            //-----------------------Arrange----------------------------------
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => studentService.GetByStudentNumber(studentNumber));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be($"Invalid given user input: {studentNumber}");
            await studentRepository.Received(0).GetByStudentNumber(studentNumber);
        }

        [Test]
        public async Task GetByStudentNumber_Given_Valid_Student_Number_Should_Returns_Student_Details()
        {
            //-----------------------Arrange----------------------------------
            var studentNumber = "2020070001";
            var student = new Student
            {
                StudentNumber = "2020070001",
                Name = "Name",
                Surname = "Surname",
                AddressId = 1,
                Cellphone = "0000000000",
                IdNumber = "0000000000000",
                CourseId = 1
            };
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.GetByStudentNumber(studentNumber).Returns(student);
            var actual = await studentService.GetByStudentNumber(studentNumber);

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(student);
            await studentRepository.Received(1).GetByStudentNumber(studentNumber);
        }

        [TestCase("", "Surname")]
        [TestCase("Name", " ")]
        [TestCase(null, " ")]
        public async Task GetByFullName_Given_Invalid_Student_Name_And_Or_Surname_Should_Throw_Exception(string name, string surname)
        {
            //-----------------------Arrange----------------------------------
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => studentService.GetByFullName(name, surname));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be($"Invalid given user input: {name}, {surname}");
            await studentRepository.Received(0).GetByFullName(name, surname);
        }

        [Test]
        public async Task GetByFullName_Given_Valid_Student_Name_And_Surname_Should_Returns_Student_Details()
        {
            //-----------------------Arrange----------------------------------
            var name = "FirstName";
            var surname = "LastName";
            var student = new Student
            {
                StudentNumber = "2020070001",
                Name = "FirstName",
                Surname = "LastName",
                AddressId = 1,
                Cellphone = "0000000000",
                IdNumber = "0000000000000",
                CourseId = 1
            };
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.GetByFullName(name, surname).Returns(student);
            var actual = await studentService.GetByFullName(name, surname);

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(student);
            await studentRepository.Received(1).GetByFullName(name, surname);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public async Task GetByCourse_Given_Invalid_Course_Id_Should_Throw_Exception(int courseId)
        {
            //-----------------------Arrange----------------------------------
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => studentService.GetByCourse(courseId));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be($"Invalid given user input: {courseId}");
            await studentRepository.Received(0).GetByCourse(courseId);
        }

        [Test]
        public async Task GetByCourse_Given_Valid_Course_Id_Should_Returns_List_Of_Student_Details()
        {
            //-----------------------Arrange----------------------------------
            var courseId = 1;
            var students = new List<Student>
            {
                new Student
                {
                StudentNumber = "2020070001",
                Name = "FirstName",
                Surname = "LastName",
                AddressId = 1,
                Cellphone = "0000000000",
                IdNumber = "0000000000000",
                CourseId = 1
                }
            };
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.GetByCourse(courseId).Returns(students);
            var actual = await studentService.GetByCourse(courseId);

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(students);
            await studentRepository.Received(1).GetByCourse(courseId);
        }

        [Test]
        public async Task GetAll_Given_Student_Table_Contains_Data_Should_Returns_List_Of_Student_Details()
        {
            //-----------------------Arrange----------------------------------
            var students = new List<Student>
            {
                new Student
                {
                StudentNumber = "2020070001",
                Name = "FirstName",
                Surname = "LastName",
                AddressId = 1,
                Cellphone = "0000000000",
                IdNumber = "0000000000000",
                CourseId = 1
                },
                new Student
                {
                StudentNumber = "2020070002",
                Name = "FirstName1",
                Surname = "LastName1",
                AddressId = 1,
                Cellphone = "0000000001",
                IdNumber = "0000000000001",
                CourseId = 1
                }
            };
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = CreateStudentService(studentRepository);

            //-----------------------Act--------------------------------------
            studentRepository.GetAll().Returns(students);
            var actual = await studentService.GetAll();

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(students);
            await studentRepository.Received(1).GetAll();
        }

        private static StudentService CreateStudentService(IStudentRepository studentRepository)
        {
            return new StudentService(studentRepository);
        }
    }
}
