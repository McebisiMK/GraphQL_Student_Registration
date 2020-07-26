using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IGenericRepository<Subject> _genericRepository;

        public SubjectRepository(IGenericRepository<Subject> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            return await _genericRepository
                            .GetAll()
                            .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetByCourse(int id)
        {
            return await _genericRepository
                            .GetBy(subject => subject.CourseId.Equals(id))
                            .ToListAsync();
        }

        public async Task<Subject> GetById(int id)
        {
            return await _genericRepository
                            .GetBy(subject => subject.Id.Equals(id))
                            .FirstOrDefaultAsync();
        }
    }
}
