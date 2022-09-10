using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> GetAll();

        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> FindRange(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> Update(Expression<Func<TEntity, bool>> expression, TEntity entity);


        Task DeleteOne(Expression<Func<TEntity, bool>> expression);

        Task DeleteRange(Expression<Func<TEntity, bool>> expression);


        //NEW stuff

        Task<TEntity> Add(TEntity entity);

        Task Update(TEntity entity);

        Task UpdateRange(List<TEntity> entities);

        Task<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> ListByExpression(Expression<Func<TEntity, bool>> expression);

        Task Delete(TEntity entity);

        Task DeleteRange(List<TEntity> entities);

        Task AddRange(List<TEntity> entities);







    }
}
