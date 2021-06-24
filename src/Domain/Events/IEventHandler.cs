using MediatR;

namespace DocHelper.Domain.Events
{
    public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent
    { }
}