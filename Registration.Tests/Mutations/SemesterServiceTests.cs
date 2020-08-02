using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Services;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Tests.Mutations
{
    [TestFixture]
    public class SemesterServiceTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Add_Given_Invalid_Semester_Name_Should_Throw_Exception(string description)
        {
            //-----------------------Arrange----------------------------------
            var semester = GetSemester();
            semester.Description = description;
            var semesterRepository = Substitute.For<ISemesterRepository>();
            var semesterService = CreateSemesterService(semesterRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => semesterService.Add(semester));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Semester");
            await semesterRepository.Received(0).Add(semester);
        }

        [Test]
        public async Task Add_Given_Valid_Semester_Record_Should_Return_Newly_Created_Record()
        {
            //-----------------------Arrange----------------------------------
            var semester = GetSemester();
            var semesterRepository = Substitute.For<ISemesterRepository>();
            var semesterService = CreateSemesterService(semesterRepository);

            //-----------------------Act--------------------------------------
            semesterRepository.Add(semester).Returns(1);
            semester.Id = 1;
            semesterRepository.GetById(1).Returns(semester);
            await semesterService.Add(semester);

            //-----------------------Assert-----------------------------------
            await semesterRepository.Received(1).Add(semester);
            semester.Id.Should().BeGreaterThan(0);
        }

        private Semester GetSemester()
        {
            return new Semester
            {
                Description = "Semester Name"
            };
        }

        private SemesterService CreateSemesterService(ISemesterRepository semesterRepository)
        {
            return new SemesterService(semesterRepository);
        }
    }
}
