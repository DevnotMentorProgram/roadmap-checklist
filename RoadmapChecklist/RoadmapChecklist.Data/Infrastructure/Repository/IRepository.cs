using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        void Attach(T entity);
        void Add(T entity);
        void Update(T entity);
        void UpdateRange(IList<T> entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> where,
           Expression<Func<T, object>> orderBy = null,
          bool isOrderByAsc = false,
           params string[] navigations);
        IQueryable<T> AsIQueryable(Expression<Func<T, bool>> where,
           Expression<Func<T, object>> orderBy = null,
          bool isOrderByAsc = false,
           params string[] navigations);
        List<T> GetMany(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> orderBy = null,
            bool isOrderByAsc = false,
            params string[] navigations);
    }
}
