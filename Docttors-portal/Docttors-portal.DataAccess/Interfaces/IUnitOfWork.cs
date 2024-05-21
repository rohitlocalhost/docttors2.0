using System;
namespace Docttors_portal.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
        IDbContext DBContext { get; }
    }
}
