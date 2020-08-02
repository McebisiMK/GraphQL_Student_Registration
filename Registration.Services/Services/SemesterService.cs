using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterService(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public async Task<Semester> Add(Semester semester)
        {
            if (!Valid(semester))
                throw new InvalidUserObject("Semester");

            var lastInsertedID = await _semesterRepository.Add(semester);

            return await _semesterRepository.GetById(lastInsertedID);
        }

        public async Task<IEnumerable<Semester>> GetAll()
        {
            return await _semesterRepository.GetAll();
        }

        public async Task<Semester> GetById(int id)
        {
            if (id <= 0)
                throw new InvalidUserInputException(id.ToString());

            return await _semesterRepository.GetById(id);
        }

        private bool Valid(Semester semester)
        {
            return (IsValid(semester.Description));
        }

        private bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
