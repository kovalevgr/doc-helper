using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Aggregate;
using DocHelper.Domain.Entities;
using DocHelper.Domain.Events;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.EventStores;
using DocHelper.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using static System.GC;

namespace DocHelper.Infrastructure.Repository
{
    public abstract class AggregateRepository<T> : BaseRepository<T>, IAggregateRepository
        where T : BaseEntity
    {
        private readonly IEventStore _eventStore;

        private readonly List<IEvent> _uncommittedEvents = new();
        private IDbContextTransaction _transaction;
        private bool _disposed;

        protected Guid AggregateId { get; } = default;

        protected AggregateRepository(ApplicationDbContext dbContext, IEventStore eventStore)
            : base(dbContext)
        {
            _eventStore = eventStore;
        }

        public async void BeginTransaction()
        {
            _transaction = await DbContext.Database.BeginTransactionAsync();
        }

        public async void SaveChanges()
        {
            await DbContext.SaveChangesAsync();

            await _eventStore.Store(GetState());
        }

        public async void Commit()
        {
            await _transaction.CommitAsync();
        }

        public async void Rollback()
        {
            await _transaction.RollbackAsync();
        }

        protected override async Task<TEntity> AddAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

            return entity;
        }

        protected virtual IEnumerable<IEvent> DequeueUncommittedEvents()
        {
            var dequeuedEvents = _uncommittedEvents.ToList();

            _uncommittedEvents.Clear();

            return dequeuedEvents;
        }

        protected virtual void Enqueue(IEvent @event)
        {
            _uncommittedEvents.Add(@event);
        }

        protected virtual AggregateState GetState()
        {
            return new(AggregateId, _uncommittedEvents);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                    _transaction?.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }
    }
}