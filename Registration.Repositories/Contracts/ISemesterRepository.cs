using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public    interface ISemesterRepository
    {
        Task<IEnumerable<Semester>> GetAll();
        Task<Semester> GetById(int id);
    }
}
