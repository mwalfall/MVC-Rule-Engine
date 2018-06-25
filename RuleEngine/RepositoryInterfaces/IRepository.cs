using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RuleEngine.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        IList<T> Find(Expression<Func<T, bool>> whereFilter, string[] include);
        T GetSingle(Expression<Func<T, bool>> where, string[] include);
        T GetFirst(Expression<Func<T, bool>> where, string[] include);

        void Add(T entity);
        void Delete(T entity);
        void Save();
    }
}
