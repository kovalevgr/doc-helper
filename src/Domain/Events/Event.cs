using System;

namespace DocHelper.Domain.Events
{
    public class Event : IEvent
    {
        public virtual Guid Id { get; } = Guid.NewGuid();
        public virtual DateTime CreatedUtc { get; } = DateTime.UtcNow;
    }
}