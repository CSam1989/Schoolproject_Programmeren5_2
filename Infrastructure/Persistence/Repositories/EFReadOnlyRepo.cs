using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Persistence.Repositories
{
    public class EfReadOnlyRepo<T> : IReadOnlyRepository<T>
        where T : class
    {
        #region private query method

        private IQueryable<T> GetQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<T> query = Context.Set<T>();

            if (filter != null) query = query.Where(filter);

            if (include != null) query = include(query);


            if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        #endregion

        #region Ctor DI

        protected readonly ApplicationDbContext Context;

        public EfReadOnlyRepo(ApplicationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Async ReadOnly DB calls

        public async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null, int? take = null)
        {
            return await GetQueryable(null, orderBy, include, skip, take).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null, int? take = null)
        {
            return await GetQueryable(filter, orderBy, include, skip, take).ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).CountAsync();
        }

        public async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).AnyAsync();
        }

        public async Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await GetQueryable(filter, orderBy, include).FirstOrDefaultAsync();
        }

        public async Task<T> GetOneAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await GetQueryable(filter, null, include).SingleOrDefaultAsync();
        }

        #endregion
    }
}