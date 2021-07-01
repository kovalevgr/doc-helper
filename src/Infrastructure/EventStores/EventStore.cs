using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Domain.Aggregate;
using DocHelper.Domain.Events;
using DocHelper.Infrastructure.EventStores.Stores;
using DocHelper.Infrastructure.MessageBrokers;
using Newtonsoft.Json;

namespace DocHelper.Infrastructure.EventStores
{
    public class EventStore : IEventStore
    {
        private readonly IStore _store;
        
        private readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public EventStore(IStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<StreamState>> GetEvents(Guid aggregateId, int? version = null,
            DateTime? createdUtc = null)
        {
            return await _store.GetEvents(aggregateId, version, createdUtc);
        }

        public async Task Store<TAggregate>(TAggregate aggregate, Func<StreamState, Task> action = null)
            where TAggregate : AggregateState
        {
            var events = aggregate.Events;
            var initialVersion = aggregate.Version - events.Count();

            foreach (var @event in events)
            {
                initialVersion++;

                await AppendEvent(aggregate.Id, @event, initialVersion, action);
            }
        }

        public async Task Store<TAggregate>(ICollection<TAggregate> aggregates, Func<StreamState, Task> action = null)
            where TAggregate : AggregateState
        {
            foreach (var aggregate in aggregates)
            {
                await Store(aggregate, action);
            }
        }

        private async Task AppendEvent(Guid aggregateId, IEvent @event,
            int? expectedVersion = null, Func<StreamState, Task> action = null)
        {
            var version = 1;

            var events = await GetEvents(aggregateId);
            var versions = events.Select(e => e.Version).ToList();

            if (expectedVersion.HasValue)
            {
                if (versions.Contains(expectedVersion.Value))
                {
                    throw new Exception($"Version '{expectedVersion.Value}' already exists for stream '{aggregateId}'");
                }
            }
            else
            {
                version = versions.DefaultIfEmpty(0).Max() + 1;
            }

            var stream = new StreamState
            {
                AggregateId = aggregateId,
                Version = version,
                Type = MessageBrokersHelper.GetTypeName(@event.GetType()),
                Data = JsonConvert.SerializeObject(@event, _jsonSerializerSettings)
            };

            await _store.Add(stream);

            if (action is not null)
            {
                await action(stream);
            }
        }
    }
}