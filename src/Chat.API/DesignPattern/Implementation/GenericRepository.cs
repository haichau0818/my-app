using Chat.API.Data;
using Chat.API.DesignPattern.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Chat.API.DesignPattern.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ChatContext _chatContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ChatContext chatContext)
        {
            _chatContext = chatContext; 
            _dbSet = chatContext.Set<T>();
        }

        public async virtual Task<bool> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return true;    
            }
            catch (Exception e)
            {
                //Save log err here!
                return false;
            }
        }

        public async virtual Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = await _dbSet.FindAsync(id);
                if (data != null)
                {
                    _dbSet.Remove(data);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                //Save log err here!
                return false;
            }
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async virtual Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _chatContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                //Save log err here!
                return false;
            }
            
        }
    }
}

