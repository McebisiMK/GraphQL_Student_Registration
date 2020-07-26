using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
    }
}
