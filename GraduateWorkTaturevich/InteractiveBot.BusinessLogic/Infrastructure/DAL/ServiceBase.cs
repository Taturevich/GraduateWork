using BusinessLogic.Infrastructure.Injection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogic.Infrastructure.DAL
{
    /// <summary>
    /// Base service for data access
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityServiceBase<TEntity>
    {
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();

        /// <summary>
        /// Get all records by expression
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get record by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(int id);

        /// <summary>
        /// Update record by new entity
        /// </summary>
        /// <param name="entity"></param>
        Task Update(TEntity entity);

        /// <summary>
        /// Update range records by entity list
        /// </summary>
        /// <param name="entities"></param>
        Task UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add many records
        /// </summary>
        /// <param name="entities"></param>
        Task AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add one entity
        /// </summary>
        /// <param name="entity"></param>
        Task Add(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        Task Delete(TEntity entity);
    }

    /// <inheritdoc />
    public abstract class EntityServiceBase<TEntity> : IEntityServiceBase<TEntity>
    {
        /// <inheritdoc />
        private readonly IRepository<TEntity> _repository;

        /// <inheritdoc />
        protected EntityServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public IRepository<TEntity> Repository => _repository;

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _repository.GetAll().ToListAsync();
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> query)
        {
            return await _repository.GetAll().Where(query).ToListAsync();
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task<TEntity> GetById(int id)
        {
            return await Task.Run(() => _repository.GetById(id));
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task Update(TEntity entity)
        {
            await Task.Run(() => _repository.Update(entity));
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _repository.UpdateRange(entities));
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _repository.AddRange(entities));
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task Add(TEntity entity)
        {
            await Task.Run(() => _repository.Add(entity));
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual async Task Delete(TEntity entity)
        {
            await Task.Run(() => _repository.Delete(entity));
        }
    }
}
