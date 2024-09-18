using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Const;
using RepositoryPatternWithUOW.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T GetById(int id) => _context.Set<T>().Find(id);

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public T Find(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes is not null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            return query.SingleOrDefault(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes is not null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            return query.Where(predicate).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip)
        {
            return _context.Set<T>().Where(predicate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if(orderBy != null)
            {
               
                if(orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                 query = query.OrderByDescending(orderBy); 
            }

            return query.ToList();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entites)
        {
            _context.Set<T>().AddRange(entites);
            return entites;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entites)
        {
            _context.Set<T>().RemoveRange(entites);

        }

        public void Atach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int count()
        {
           return  _context.Set<T>().Count();
        }

        public int count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

      
    }

}

