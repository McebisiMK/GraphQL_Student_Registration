using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> Add(Course course)
        {
            if (!Valid(course))
                throw new InvalidUserObject("Course");

            var lastInsertedID = await _courseRepository.Add(course);

            return await _courseRepository.GetById(lastInsertedID);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _courseRepository.GetAll();
        }

        public async Task<Course> GetById(int id)
        {
            if (id <= 0)
                throw new InvalidUserInputException(id.ToString());

            return await _courseRepository.GetById(id);
        }

        private bool Valid(Course course)
        {
            return (IsValid(course.Name));
        }

        private bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
