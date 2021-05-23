using System;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace DocHelper.Infrastructure.Persistence
{
    public class ApplicationDbTransaction : IApplicationDbTransaction, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        [CanBeNull] private Task<IDbContextTransaction> _transaction;
        
        public ApplicationDbTransaction(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void Begin()
        {
            _transaction = _dbContext.Database.BeginTransactionAsync();
        }

        public async void Commit()
        {
            ThrowIfNotStarted();

            var transaction = await _transaction;

            await transaction.CommitAsync();
        }

        public async void Rollback()
        {
            ThrowIfNotStarted();

            var transaction = await _transaction;

            await transaction.RollbackAsync();
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