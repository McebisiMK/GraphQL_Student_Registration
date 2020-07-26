using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject> GetById(int id);
        Task<IEnumerable<Subject>> GetByCourse(int id);
    }
}
