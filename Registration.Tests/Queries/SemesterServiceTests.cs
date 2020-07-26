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
    public class SemesterServiceTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-45)]
        public async Task GetById_Given_Invalid_Semester_Id_Should_Throw_Exception(int semesterId)
        {
            //-----------------------Arrange-----------------------------------
            var semesterRepository = Substitute.For<ISemesterRepository>();
            var semesterService = CreateSemesterService(semesterRepository);

            //-----------------------Act---------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => semesterService.GetById(semesterId));

            //-----------------------Assert------------------------------------
            exception.Message.Should().Be($"Invalid given user input: {semesterId}");
            await semesterRepository.Received(0).GetById(semesterId);
        }

        [Test]
        public async Task GetById_Given_Valid_Semester_Id_Should_Return_Semester_Details()
        {
            //-----------------------Arrange-----------------------------------
            var semesterId = 1;
            var semester = new Semester
            {
                Id = 1,
                Description = "Semester Name"
            };
            var semesterRepository = Substitute.For<ISemesterRepository>();
            var semesterService = CreateSemesterService(semesterRepository);

            //-----------------------Act---------------------------------------
            semesterRepository.GetById(semesterId).Returns(semester);
            var actual = await semesterService.GetById(semesterId);

            //-----------------------Assert------------------------------------
            actual.Should().BeEquivalentTo(semester);
            await semesterRepository.Received(1).GetById(semesterId);
        }

        [Test]
        public async Task GetAll_Given_Semester_Table_Contains_Data_Should_Return_List_Of_Semester_Details()
        {
            //-----------------------Arrange----------------------------------
            var semesterList = new List<Semester>
            {
                new Semester
                {
                    Id = 1,
                    Description = "Semester Name"
                },
                new Semester
                {
                    Id = 2,
                    Description = "Semester Name 2"
                }
            };
            var semesterRepository = Substitute.For<ISemesterRepository>();
            var semesterService = CreateSemesterService(semesterRepository);

            //-----------------------Act--------------------------------------
            semesterRepository.GetAll().Returns(semesterList);
            var actual = await semesterService.GetAll();

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(semesterList);
            await semesterRepository.Received(1).GetAll();
        }

        private SemesterService CreateSemesterService(ISemesterRepository semesterRepository)
        {
            return new SemesterService(semesterRepository);
        }
    }
}
