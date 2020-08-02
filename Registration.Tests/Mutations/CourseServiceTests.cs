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
    public class CourseServiceTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Add_Given_Invalid_Course_Name_Should_Throw_Exception(string name)
        {
            //-----------------------Arrange----------------------------------
            var course = GetCourse();
            course.Name = name;
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = CreateCourseService(courseRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => courseService.Add(course));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Course");
            await courseRepository.Received(0).Add(course);
        }

        [Test]
        public async Task Add_Given_Valid_Course_Record_Should_Return_Newly_Created_Record()
        {
            //-----------------------Arrange----------------------------------
            var course = GetCourse();
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = CreateCourseService(courseRepository);

            //-----------------------Act--------------------------------------
            courseRepository.Add(course).Returns(1);
            course.Id = 1;
            courseRepository.GetById(1).Returns(course);
            await courseService.Add(course);

            //-----------------------Assert-----------------------------------
            await courseRepository.Received(1).Add(course);
            course.Id.Should().BeGreaterThan(0);
        }

        private Course GetCourse()
        {
            return new Course
            {
                Id = 1,
                Name = "Course Name"
            };
        }

        private CourseService CreateCourseService(ICourseRepository courseRepository)
        {
            return new CourseService(courseRepository);
        }
    }
}
