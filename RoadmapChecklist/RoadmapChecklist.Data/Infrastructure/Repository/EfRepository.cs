using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Infrastructure.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected RoadmapChecklistDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public EfRepository(RoadmapChecklistDbContext dbContext)
        {
            this.dbContext = dbContext;
            // AsNoTracking() kullanımının global olarak tanımlanma şekli.
            // AsNoTracking() ya da aşağıdaki tanım yapılmazsa db'den self-references olarak tanımlı olan veriler, ilişkileri ile birlikte geliyor.
            // Kaynak: https://stackoverflow.com/a/39809419/4650413
            this.dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            dbSet = this.dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy = null, bool isOrderByAsc = false, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            set = set.Where(where);
            if (orderBy != null) { set = isOrderByAsc ? set.OrderBy(orderBy) : set.OrderByDescending(orderBy); }
            return set.FirstOrDefault();
        }

        public List<T> GetMany(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy = null, bool isOrderByAsc = false, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            set = set.Where(where);
            if (orderBy != null) { set = isOrderByAsc ? set.OrderBy(orderBy) : set.OrderByDescending(orderBy); }
            return set.ToList();
        }

        public IQueryable<T> AsIQueryable(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy = null, bool isOrderByAsc = false, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations) { set = set.Include(nav); }
            set = set.Where(where);
            if (orderBy != null) { set = isOrderByAsc ? set.OrderBy(orderBy) : set.OrderByDescending(orderBy); }
            return set;
        }
    }
}
