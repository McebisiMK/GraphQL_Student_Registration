using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface ICourseRepository
    {
        Task<Course> Add(Course course);
        Task<Course> Update(Course oldCourse, Course newCourse);
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
        bool Exists(Expression<Func<Course, bool>> expression);
    }
}
