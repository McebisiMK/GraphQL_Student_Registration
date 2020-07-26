﻿using Registration.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
    }
}