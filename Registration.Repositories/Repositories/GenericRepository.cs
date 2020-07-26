using Microsoft.EntityFrameworkCore;
using Registration.Entities.Models;
using Registration.Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
