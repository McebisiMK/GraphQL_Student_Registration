using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface ISubjectService
    {
        Task<Subject> Add(Subject subject);
        Task<Subject> Update(int id, Subject subject);
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject> GetById(int id);
        Task<IEnumerable<Subject>> GetByCourse(int id);
    }
}
