using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Domain.Aggregate;

namespace DocHelper.Infrastructure.EventStores
{
    public interface IEventStore
    {
        Task<IEnumerable<StreamState>> GetEvents(Guid aggregateId, int? version = null, DateTime? createdUtc = null);
        Task Store<TAggregate>(TAggregate aggregate, Func<StreamState, Task> action = null) where TAggregate : AggregateState;
        Task Store<TAggregate>(ICollection<TAggregate> aggregates, Func<StreamState, Task> action = null) where TAggregate : AggregateState;
    }
}