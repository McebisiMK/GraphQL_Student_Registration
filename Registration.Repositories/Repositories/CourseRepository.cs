using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IGenericRepository<Course> _genericRepository;

        public CourseRepository(IGenericRepository<Course> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Course> Add(Course course)
        {
            await _genericRepository.Add(course);
            await _genericRepository.SaveAsync();

            return course;
        }

        public async Task<Course> Update(Course oldCourse, Course newCourse)
        {
            _genericRepository.Update(oldCourse, newCourse);
            await _genericRepository.SaveAsync();

            return newCourse;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _genericRepository
                            .GetAll()
                            .ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await _genericRepository
                            .GetBy(course => course.Id.Equals(id))
                            .FirstOrDefaultAsync();
        }

        public bool Exists(Expression<Func<Course, bool>> expression)
        {
            return _genericRepository.Exists(expression);
        }
    }
}
