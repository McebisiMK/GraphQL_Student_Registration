using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Service.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentRepository.GetAll();
        }

        public async Task<Student> GetByStudentNumber(string studentNumber)
        {
            if (string.IsNullOrWhiteSpace(studentNumber))
                throw new InvalidUserInputException(studentNumber);

            return await _studentRepository.GetByStudentNumber(studentNumber);
        }

        public async Task<Student> GetByFullName(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                throw new InvalidUserInputException($"{name}, {surname}");

            return await _studentRepository.GetByFullName(name, surname);
        }

        public async Task<IEnumerable<Student>> GetByCourse(int courseId)
        {
            if (courseId <= 0)
                throw new InvalidUserInputException(courseId.ToString());

            return await _studentRepository.GetByCourse(courseId);
        }
    }
}
