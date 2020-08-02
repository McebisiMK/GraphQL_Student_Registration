using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Registration.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly RegistrationsDBContext _registrationsDBContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(RegistrationsDBContext registrationsDBContext)
        {
            _registrationsDBContext = registrationsDBContext;
            _dbSet = _registrationsDBContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task SaveAsync()
        {
           await _registrationsDBContext.SaveChangesAsync();
        }
    }
}
