using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic.Infrastructure.DAL
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> query);

        TEntity GetById(int id);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteRange(Expression<Func<TEntity, bool>> query);

        void DeleteRange(IQueryable<TEntity> instances);
    }

    internal sealed class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly BlDbContext _dataContext;
        private bool _disposed;
        public Repository()
        {
            _dataContext = new BlDbContext("Server=tcp:taturevichserver.database.windows.net,1433;" +
                                           "Initial Catalog=NewDb;Persist Security Info=False;" +
                                           "User ID=ivan_taturevich;Password=Aa83386491994;" +
                                           "MultipleActiveResultSets=False;Encrypt=True;" +
                                           "TrustServerCertificate=False;Connection Timeout=30;");
            BaseRepository = _dataContext.Set<TEntity>();
        }

        public DbSet<TEntity> BaseRepository { get; }

        public void Add(TEntity entity)
        {
            BaseRepository.Add(entity);
            _dataContext.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            BaseRepository.AddRange(entities);
            _dataContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            BaseRepository.Remove(entity);
            _dataContext.SaveChanges();
        }

        public void DeleteRang(IQueryable<TEntity> instances)
        {
            BaseRepository.RemoveRange(instances);
            _dataContext.SaveChanges();
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> query)
        {
            var entitiesToDelete = BaseRepository.Where(query);
            BaseRepository.RemoveRange(entitiesToDelete);
            _dataContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            var entitiesToReturn = BaseRepository.Select(x => x);
            return entitiesToReturn;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> query)
        {
            var entitiesToReturn = BaseRepository.Where(query);
            return entitiesToReturn;
        }

        public TEntity GetById(int id)
        {
            var entityToReturn = BaseRepository.Find(id);
            return entityToReturn;
        }

        public void Update(TEntity entity)
        {
            BaseRepository.AddOrUpdate(entity);
            _dataContext.SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dataContext.Configuration.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    BaseRepository.AddOrUpdate(entity);
                }
            }
            finally
            {
                _dataContext.Configuration.AutoDetectChangesEnabled = true;
            }
            _dataContext.SaveChanges();
        }

        public void DeleteRange(IQueryable<TEntity> instances)
        {
            BaseRepository.RemoveRange(instances);
            _dataContext.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dataContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
