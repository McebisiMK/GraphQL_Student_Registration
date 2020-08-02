using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System.Collections.Generic;
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

        public async Task<int> Add(Course course)
        {
            await _genericRepository.Add(course);
            await _genericRepository.SaveAsync();

            return course.Id;
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
    }
}
