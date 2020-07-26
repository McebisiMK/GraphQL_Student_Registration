using System;
using System.Linq;
using System.Linq.Expressions;

namespace Registration.Repository.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression);
    }
}
