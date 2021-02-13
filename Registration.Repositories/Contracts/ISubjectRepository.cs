using Registration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface ISubjectRepository
    {
        Task<Subject> Add(Subject subject);
        Task<Subject> Update(Subject oldSubject, Subject newSubject);
        bool Exists(Expression<Func<Subject, bool>> expression);
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject> GetById(int id);
        Task<IEnumerable<Subject>> GetByCourse(int id);
    }
}
