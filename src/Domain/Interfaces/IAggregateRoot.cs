using System;

namespace DocHelper.Domain.Interfaces
{
    public interface IAggregateRoot
    {
        public Guid AggregateId { get; set; }
    }
}