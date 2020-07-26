using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetByStudentNumber(string studentNumber);
        Task<Student> GetByFullName(string name, string surname);
        Task<IEnumerable<Student>> GetByCourse(int courseId);
    }
}
