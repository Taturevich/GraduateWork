using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace BusinessLogic.Infrastructure.DAL
{
    public interface IDatabaseCommandContext
    {
        int ExecuteSqlCommand(string sql, params SqlParameter[] parameters);

        DbRawSqlQuery<T> ExecuteTypeSqlCommand<T>(string sql, params SqlParameter[] parameters);
    }

    internal class DatabaseCommandContext : IDatabaseCommandContext
    {
        private readonly BlDbContext _dbContext;

        public DatabaseCommandContext()
        {
            _dbContext = new BlDbContext("Server=tcp:taturevichserver.database.windows.net,1433;" +
                                           "Initial Catalog=NewDb;Persist Security Info=False;" +
                                           "User ID=ivan_taturevich;Password=Aa83386491994;" +
                                           "MultipleActiveResultSets=False;Encrypt=True;" +
                                           "TrustServerCertificate=False;Connection Timeout=30;");
        }

        public int ExecuteSqlCommand(string sql, params SqlParameter[] parameters)
        {
            _dbContext.SaveChanges();
            return _dbContext
                .Database
                .ExecuteSqlCommand(sql, parameters);
        }

        public DbRawSqlQuery<T> ExecuteTypeSqlCommand<T>(string sql, params SqlParameter[] parameters)
        {
            _dbContext.SaveChanges();
            return _dbContext
                .Database
                .SqlQuery<T>(sql, parameters);
        }
    }
}
