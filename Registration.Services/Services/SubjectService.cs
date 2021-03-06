﻿using Registration.Entities.Models;
using Registration.Repository.Contracts;
using Registration.Service.Contracts;
using Registration.Utilities.Exceptions;
using System;
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

        public async Task<Subject> Add(Subject subject)
        {
            if (!Valid(subject))
                throw new InvalidUserObject("Subject");

            if (!ContainsFeignKeys(subject))
                throw new InvalidForeignKeyException("Subject");

            return await _subjectRepository.Add(subject);
        }

        public async Task<Subject> Update(int id, Subject newSubject)
        {
            var subjectExists = _subjectRepository.Exists(subject => subject.Id.Equals(id));

            if (!Valid(newSubject) && !subjectExists)
                throw new InvalidUserObject("Subject");

            var existingSubject = await _subjectRepository.GetById(id);

            return await _subjectRepository.Update(existingSubject, newSubject);
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

        private bool ContainsFeignKeys(Subject subject)
        {
            var hasCourse = _subjectRepository.Exists(sub => sub.CourseId.Equals(subject.CourseId));

            return hasCourse;
        }

        private bool Valid(Subject subject)
        {
            return
                (
                    IsValid(subject.Name) &&
                    IsValid(subject.Semester.ToString()) &&
                    IsNumeric(subject.CourseId)
                );
        }

        private bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        private bool IsNumeric(int number)
        {
            return number > 0;
        }
    }
}
