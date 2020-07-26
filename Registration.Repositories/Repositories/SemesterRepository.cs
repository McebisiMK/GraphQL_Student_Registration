using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly IGenericRepository<Semester> _genericRepository;

        public SemesterRepository(IGenericRepository<Semester> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Semester>> GetAll()
        {
            return await _genericRepository
                            .GetAll()
                            .ToListAsync();
        }

        public async Task<Semester> GetById(int id)
        {
            return await _genericRepository
                            .GetBy(semester => semester.Id.Equals(id))
                            .FirstOrDefaultAsync();
        }
    }
}
