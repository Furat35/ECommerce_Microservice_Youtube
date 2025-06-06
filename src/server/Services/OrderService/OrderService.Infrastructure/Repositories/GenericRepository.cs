﻿using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Context;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderDbContext dbContext;

        public GenericRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUnitOfWork UnitOfWork => dbContext;

        public virtual async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return Get(filter, null, includes);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(i => i.Id == id);
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(expression).SingleOrDefaultAsync();

        }

        public virtual T Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
