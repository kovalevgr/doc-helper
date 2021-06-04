using System;
using DocHelper.Application.Common.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace DocHelper.Infrastructure.Persistence
{
    public class ApplicationDbTransaction : IApplicationDbTransaction, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        [CanBeNull] private IDbContextTransaction _transaction;
        
        public ApplicationDbTransaction(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Begin()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            ThrowIfNotStarted();

            _transaction?.Commit();
        }

        public void Rollback()
        {
            ThrowIfNotStarted();

            _transaction?.Rollback();
        }

        private void ThrowIfNotStarted()
        {
            if (_transaction is null)
            {
                throw new Exception("Transaction don't started");
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}