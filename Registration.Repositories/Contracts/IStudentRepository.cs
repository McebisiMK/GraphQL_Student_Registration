using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface IStudentRepository
    {
        Task<Student> Add(Student student);
        Task<Student> Update(Student oldStudent, Student newStudent);
        bool Exists(Expression<Func<Student, bool>> expression);
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetByStudentNumber(string studentNumber);
        Task<Student> GetByFullName(string name, string surname);
        Task<IEnumerable<Student>> GetByCourse(int courseId);
    }
}
