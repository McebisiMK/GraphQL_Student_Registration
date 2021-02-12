using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Services;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Tests.Mutations
{
    [TestFixture]
    public class SubjectServiceTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Add_Given_Invalid_Subject_Name_Should_Throw_Exception(string name)
        {
            //-----------------------Arrange----------------------------------
            var subject = GetSubject();
            subject.Name = name;
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => subjectService.Add(subject));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Subject");
            await subjectRepository.Received(0).Add(subject);
            subject.Id.Should().Be(0);
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        [TestCase(-2, -7)]
        public async Task Add_Given_Invalid_Course_Id_And_Or_Semester_Id_Should_Throw_Exception(int courseId, int semesterId)
        {
            //-----------------------Arrange----------------------------------
            var subject = GetSubject();
            subject.CourseId = courseId;
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act--------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserObject>(() => subjectService.Add(subject));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Invalid given object: Subject");
            await subjectRepository.Received(0).Add(subject);
            subject.Id.Should().Be(0);
        }

        [Test]
        public async Task Add_Given_Non_Existing_Course_Id_Should_Throw_Exception()
        {
            //-----------------------Arrange----------------------------------
            var courseId = 7;
            var subject = GetSubject();
            subject.CourseId = courseId;
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act--------------------------------------
            subjectRepository.Exists(Arg.Any<Expression<Func<Subject, bool>>>()).Returns(false);
            var exception = Assert.ThrowsAsync<InvalidForeignKeyException>(() => subjectService.Add(subject));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Given foreign key(s) does not exists: Subject");
            await subjectRepository.Received(0).Add(subject);
            subject.Id.Should().Be(0);
        }

        [Test]
        public async Task Add_Given_Non_Existing_Semester_Id_Should_Throw_Exception()
        {
            //-----------------------Arrange----------------------------------
            var semesterId = 7;
            var subject = GetSubject();
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act--------------------------------------
            subjectRepository.Exists(Arg.Any<Expression<Func<Subject, bool>>>()).Returns(false);
            var exception = Assert.ThrowsAsync<InvalidForeignKeyException>(() => subjectService.Add(subject));

            //-----------------------Assert-----------------------------------
            exception.Message.Should().Be("Given foreign key(s) does not exists: Subject");
            await subjectRepository.Received(0).Add(subject);
            subject.Id.Should().Be(0);
        }

        [Test]
        public async Task Add_Given_Valid_Subject_Record_Should_Return_Newly_Created_Record()
        {
            //-----------------------Arrange----------------------------------
            var subject = GetSubject();
            var subjectRepository = Substitute.For<ISubjectRepository>();
            var subjectService = CreateSubjectService(subjectRepository);

            //-----------------------Act--------------------------------------
            subjectRepository.Exists(Arg.Any<Expression<Func<Subject, bool>>>()).Returns(true);
            subjectRepository.Add(subject).Returns(1);
            subject.Id = 1;
            subjectRepository.GetById(1).Returns(subject);
            var actual = await subjectService.Add(subject);

            //-----------------------Assert-----------------------------------
            await subjectRepository.Received(1).Add(subject);
            actual.Id.Should().BeGreaterThan(0);
        }

        private Subject GetSubject()
        {
            return new Subject
            {
                Name = "Subject Name",
                CourseId = 1,
                Semester = Semester.First
            };
        }

        private SubjectService CreateSubjectService(ISubjectRepository subjectRepository)
        {
            return new SubjectService(subjectRepository);
        }
    }
}
