using System;
using System.Threading.Tasks;
using DocHelper.Domain.Events;

namespace DocHelper.Infrastructure.MessageBrokers
{
    public interface IEventListener
    {
        void Subscribe(Type type);
        void Subscribe<TEvent>() where TEvent : IEvent;
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        Task Publish(string message, string type);
    }
}