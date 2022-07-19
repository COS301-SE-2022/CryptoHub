
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {

        protected readonly CryptoHubDBContext dBContext;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(CryptoHubDBContext dBContext)
        {
            this.dBContext = dBContext;
            this.dbSet = dBContext.Set<TEntity>();
        }

        public async Task DeleteOne(Expression<Func<TEntity, bool>> expression)
        {
            var response = await dbSet.FirstOrDefaultAsync(expression);
            if (response == null)
                return;

            dbSet.Remove(response);
            dBContext.SaveChanges();
        }

        public async Task DeleteRange(Expression<Func<TEntity, bool>> expression)
        {
            var response = await dbSet.Where(expression).ToListAsync();
            dbSet.RemoveRange(response);
            dBContext.SaveChanges();

        }

        public async Task<List<TEntity>> FindRange(Expression<Func<TEntity, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> expression)
        {
            var response = await dbSet.FirstOrDefaultAsync(expression);
            if (response == null)
                return null;

            return response;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(Expression<Func<TEntity, bool>> expression)
        {
            var response = await dbSet.FirstOrDefaultAsync(expression);
            if (response == null)
                return null;

            return response;
        }

        public async Task<TEntity> Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            var response = await dbSet.FirstOrDefaultAsync(expression);
            if (response == null)
                return null;

            dbSet.Update(entity);
            dBContext.SaveChanges();

            return entity;

        }


        //NEW STUFF
        public async Task<TEntity> Add(TEntity entity)
        {
            var response = await dbSet.AddAsync(entity);
            dBContext.SaveChanges();
            return entity;

        }

        public Task Update(TEntity entity)
        {
            dbSet.Update(entity);
            dBContext.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression)
        {
            var response = await dbSet.FirstOrDefaultAsync(expression);
            if (response == null)
                return null;

            return response;
        }

        public async Task<List<TEntity>> ListByExpression(Expression<Func<TEntity, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }

        public Task Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            dBContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
