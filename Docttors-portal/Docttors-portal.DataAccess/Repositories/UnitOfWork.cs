using Docttors_portal.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Docttors_portal.DataAccess.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : IDbContext, new()
    {
        private readonly IDbContext _ctx;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;
        public UnitOfWork()
        {
            _ctx = new TContext();

            _repositories = new Dictionary<Type, object>();
            _disposed = false;

        }
        public IDbContext DBContext
        {
            get { return _ctx; }

        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            // Checks if the Dictionary Key contains the Model class
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                // Return the repository for that Model class
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            // If the repository for that Model class doesn't exist, create it
            var repository = new BaseRepository<TEntity>(_ctx);

            // Add it to the dictionary
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }

                this._disposed = true;
            }
        }
        public void Commit()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    if (_ctx != null)
                        _ctx.SaveChanges();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception", ex);
            }
        }
    }
}
