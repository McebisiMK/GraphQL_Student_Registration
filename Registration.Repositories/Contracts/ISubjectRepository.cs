using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface ISubjectRepository
    {
        Task<int> Add(Subject subject);
        bool Exists(Expression<Func<Subject, bool>> expression);
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject> GetById(int id);
        Task<IEnumerable<Subject>> GetByCourse(int id);
    }
}
