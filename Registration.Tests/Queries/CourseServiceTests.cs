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
    public class CourseServiceTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-45)]
        public async Task GetById_Given_Invalid_Course_Id_Should_Throw_Exception(int courseId)
        {
            //-----------------------Arrange-----------------------------------
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = CreateCourseService(courseRepository);

            //-----------------------Act---------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => courseService.GetById(courseId));

            //-----------------------Assert------------------------------------
            exception.Message.Should().Be($"Invalid given user input: {courseId}");
            await courseRepository.Received(0).GetById(courseId);
        }

        [Test]
        public async Task GetById_Given_Valid_Course_Id_Should_Return_Course_Details()
        {
            //-----------------------Arrange-----------------------------------
            var courseId = 1;
            var course = new Course
            {
                Id = 1,
                Name = "Course Name"
            };
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = CreateCourseService(courseRepository);

            //-----------------------Act---------------------------------------
            courseRepository.GetById(courseId).Returns(course);
            var actual = await courseService.GetById(courseId);

            //-----------------------Assert------------------------------------
            actual.Should().BeEquivalentTo(course);
            await courseRepository.Received(1).GetById(courseId);
        }


        [Test]
        public async Task GetAll_Given_Course_Table_Contains_Data_Should_Return_List_Of_Course_Details()
        {
            //-----------------------Arrange----------------------------------
            var courseList = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Course Name"
                },
                new Course
                {
                    Id = 2,
                    Name = "Course Name 2"
                }
            };
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = CreateCourseService(courseRepository);

            //-----------------------Act--------------------------------------
            courseRepository.GetAll().Returns(courseList);
            var actual = await courseService.GetAll();

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(courseList);
            await courseRepository.Received(1).GetAll();
        }

        private CourseService CreateCourseService(ICourseRepository courseRepository)
        {
            return new CourseService(courseRepository);
        }
    }
}
