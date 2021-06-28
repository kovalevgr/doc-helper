using System;
using System.Collections.Generic;
using DocHelper.Domain.Events;

namespace DocHelper.Domain.Aggregate
{
    public class AggregateState
    {
        public Guid Id { get; }
        public int Version { get; } = 0;
        public DateTime CreatedUtc { get; }
        public IEnumerable<IEvent> Events { get; } = new List<IEvent>();
        
        public AggregateState(Guid id, IEnumerable<IEvent> events)
        {
            Id = id;
            Events = events;
        }
        
        public AggregateState(Guid id, int version, DateTime createdUtc)
        {
            Id = id;
            Version = version;
            CreatedUtc = createdUtc;
        }
    }
}