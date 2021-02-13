using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Student> Add(Student student)
        {
            if (!Valid(student))
                throw new InvalidUserObject("Student");

            if (!ContainsFeignKeys(student))
                throw new InvalidForeignKeyException("Student");

            var studentNumber = await CreateNewStudentNumber();
            student.StudentNumber = studentNumber;

            return await _studentRepository.Add(student);
        }

        public async Task<Student> Update(string studentNumber, Student newStudent)
        {
            var studentExists = _studentRepository.Exists(student => student.StudentNumber.Equals(studentNumber));

            if (!Valid(newStudent) && !studentExists)
                throw new InvalidUserObject("Student");

            var existingStudent = await _studentRepository.GetByStudentNumber(studentNumber);

            return await _studentRepository.Update(existingStudent, newStudent);
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

        private async Task<string> CreateNewStudentNumber()
        {
            var date = DateTime.Now;
            var yearMonth = $"{ date.Year }{ date.Month.ToString("00")}";
            var students = await _studentRepository.GetAll();
            var LastInsertedStudentNumber = students
                                            .OrderByDescending(student => student.StudentNumber)
                                            .FirstOrDefault()
                                            .StudentNumber;

            var studentNumber = string.IsNullOrWhiteSpace(LastInsertedStudentNumber) ?
                                    $"{yearMonth}0001" :
                                    $"{yearMonth}{(int.Parse(LastInsertedStudentNumber.Substring(6, 4)) + 1).ToString("0000")}";

            return studentNumber;
        }

        private bool ContainsFeignKeys(Student student)
        {
            var hasCourse = _studentRepository.Exists(stud => stud.CourseId.Equals(student.CourseId));
            var hasAddress = _studentRepository.Exists(addr => addr.AddressId.Equals(student.AddressId));

            return (hasCourse && hasAddress);
        }

        private bool Valid(Student student)
        {
            return
                (
                    IsValid(student.Name) &&
                    IsValid(student.Surname) &&
                    IsNumeric(student.CourseId) &&
                    IsValid(student.Cellphone) &&
                    IsValid(student.IdNumber) &&
                    IsNumeric(student.AddressId)
                );
        }

        private bool IsNumeric(int input)
        {
            return (input > 0);
        }

        private bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
