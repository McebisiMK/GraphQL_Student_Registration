using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            return await _subjectRepository.GetAll();
        }

        public async Task<IEnumerable<Subject>> GetByCourse(int id)
        {
            if (id <= 0)
                throw new InvalidUserInputException(id.ToString());

            return await _subjectRepository.GetByCourse(id);
        }

        public async Task<Subject> GetById(int id)
        {
            if (id <= 0)
                throw new InvalidUserInputException(id.ToString());

            return await _subjectRepository.GetById(id);
        }
    }
}
