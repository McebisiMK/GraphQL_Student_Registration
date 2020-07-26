using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetByStudentNumber(string studentNumber);
        Task<Student> GetByFullName(string name, string surname);
        Task<IEnumerable<Student>> GetByCourse(int courseId);
    }
}
