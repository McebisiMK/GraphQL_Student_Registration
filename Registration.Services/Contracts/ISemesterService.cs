using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface ISemesterService
    {
        Task<Semester> Add(Semester semester);
        Task<IEnumerable<Semester>> GetAll();
        Task<Semester> GetById(int id);
    }
}
