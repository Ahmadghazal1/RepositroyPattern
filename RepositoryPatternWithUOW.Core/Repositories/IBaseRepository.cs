using RepositoryPatternWithUOW.Core.Const;
using System.Linq.Expressions;

namespace RepositoryPatternWithUOW.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> predicate, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);

        T Update(T entity);
        void Delete(T entity);  
        void DeleteRange(IEnumerable<T> entites);  
        void Atach(T entity);
        int count();
        int count(Expression<Func<T, bool>> predicate);

    }
}
