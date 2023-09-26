using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class GenericRepositories<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _entitySet;

        public GenericRepositories(ApplicationContext context)
        {
            _context = context;
            _entitySet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entitySet;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitySet.Where(predicate);
        }

        public TEntity GetById(object id)
        {
            return _entitySet.Find(id);
        }

        public void Add(TEntity entity)
        {
            _entitySet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var entityToDelete = _entitySet.Find(id);
            if (entityToDelete != null)
                _entitySet.Remove(entityToDelete);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
