using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task Add(Student student)
        {
            await _genericRepository.Add(student);
            await _genericRepository.SaveAsync();
        }

        public bool Exists(Expression<Func<Student, bool>> expression)
        {
            return _genericRepository.Exists(expression);
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
