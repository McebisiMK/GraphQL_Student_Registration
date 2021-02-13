using Registration.Entities.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression);
        Task Add(TEntity entity);
        void Update(TEntity oldEntity, TEntity newEntity);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task SaveAsync();
    }
}
