using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface IStudentRepository
    {
        Task Add(Student student);
        bool Exists(Expression<Func<Student, bool>> expression);
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetByStudentNumber(string studentNumber);
        Task<Student> GetByFullName(string name, string surname);
        Task<IEnumerable<Student>> GetByCourse(int courseId);
    }
}
