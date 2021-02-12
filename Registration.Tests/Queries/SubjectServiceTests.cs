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
    public class SubjectServiceTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-45)]
        public async Task GetById_Given_Invalid_Subject_Id_Should_Throw_Exception(int subjectId)
        {
            //-----------------------Arrange-----------------------------------
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act---------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => subjectService.GetById(subjectId));

            //-----------------------Assert------------------------------------
            exception.Message.Should().Be($"Invalid given user input: {subjectId}");
            await subjectRepository.Received(0).GetById(subjectId);
        }

        [Test]
        public async Task GetById_Given_Valid_Subject_Id_Should_Return_Subject_Details()
        {
            //-----------------------Arrange-----------------------------------
            var subjectId = 1;
            var subject = GetSubject();
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act---------------------------------------
            subjectRepository.GetById(subjectId).Returns(subject);
            var actual = await subjectService.GetById(subjectId);

            //-----------------------Assert------------------------------------
            actual.Should().BeEquivalentTo(subject);
            await subjectRepository.Received(1).GetById(subjectId);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-45)]
        public async Task GetByCourse_Given_Invalid_Course_Id_Should_Throw_Exception(int courseId)
        {
            //-----------------------Arrange-----------------------------------
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act---------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserInputException>(() => subjectService.GetByCourse(courseId));

            //-----------------------Assert------------------------------------
            exception.Message.Should().Be($"Invalid given user input: {courseId}");
            await subjectRepository.Received(0).GetByCourse(courseId);
        }

        [Test]
        public async Task GetByCourse_Given_Valid_Course_Id_Should_Return_List_Of_Subject_Details()
        {
            //-----------------------Arrange-----------------------------------
            var subjectId = 1;
            var subjectList = GetSubjects();
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act---------------------------------------
            subjectRepository.GetByCourse(subjectId).Returns(subjectList);
            var actual = await subjectService.GetByCourse(subjectId);

            //-----------------------Assert------------------------------------
            actual.Should().BeEquivalentTo(subjectList);
            await subjectRepository.Received(1).GetByCourse(subjectId);
        }

        [Test]
        public async Task GetAll_Given_Subject_Table_Contains_Data_Should_Return_List_Of_Subject_Details()
        {
            //-----------------------Arrange----------------------------------
            var subjectList = GetSubjects();
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act--------------------------------------
            subjectRepository.GetAll().Returns(subjectList);
            var actual = await subjectService.GetAll();

            //-----------------------Assert-----------------------------------
            actual.Should().BeEquivalentTo(subjectList);
            await subjectRepository.Received(1).GetAll();
        }

        private Subject GetSubject()
        {
            return new Subject
                    {
                        Id = 1,
                        Name = "Subject Name",
                        CourseId = 1,
                        Semester = Semester.First
            };
        }

        private List<Subject> GetSubjects()
        {
            var subjects = new List<Subject>
                            {
                                new Subject
                                {
                                    Id = 1,
                                    Name = "Subject Name",
                                    CourseId = 1,
                                    Semester = Semester.First
                                },
                                new Subject
                                {
                                    Id = 2,
                                    Name = "Subject Name 2",
                                    CourseId = 1,
                                    Semester = Semester.First
                                },
                                new Subject
                                {
                                    Id = 3,
                                    Name = "Subject Name 3",
                                    CourseId = 1,
                                    Semester = Semester.Second
                                }
                            };

            return subjects;
        }

        private SubjectService CreateSubjectService(ISubjectRepository subjectRepository)
        {
            return new SubjectService(subjectRepository);
        }
    }
}
