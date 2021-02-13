using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface IStudentService
    {
        Task<Student> Add(Student student);
        Task<Student> Update(string studentNumber, Student student);
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetByStudentNumber(string studentNumber);
        Task<Student> GetByFullName(string name, string surname);
        Task<IEnumerable<Student>> GetByCourse(int courseId);
    }
}
