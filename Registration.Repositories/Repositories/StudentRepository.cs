using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IGenericRepository<Student> _genericRepository;

        public StudentRepository(IGenericRepository<Student> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _genericRepository.GetAll().ToListAsync();
        }

        public async Task<Student> GetByStudentNumber(string studentNumber)
        {
            return await _genericRepository
                            .GetBy(student => student.StudentNumber.Equals(studentNumber))
                            .FirstOrDefaultAsync();
        }

        public async Task<Student> GetByFullName(string name, string surname)
        {
            return await _genericRepository
                            .GetBy(student => student.Name.Equals(name) && student.Surname.Equals(surname))
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetByCourse(int courseId)
        {
            return await _genericRepository
                            .GetBy(student => student.CourseId.Equals(courseId))
                            .ToListAsync();
        }
    }
}
