using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic.Infrastructure
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
        List<TEntity> GetAll();

        /// <summary>
        /// Get all records by expression
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get record by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Update record by new entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Update range records by entity list
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add many records
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add one entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
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
        public virtual List<TEntity> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> query)
        {
            return _repository.GetAll().Where(query).ToList();
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _repository.UpdateRange(entities);
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        /// <inheritdoc />
        [BotEventLog]
        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }
    }
}
