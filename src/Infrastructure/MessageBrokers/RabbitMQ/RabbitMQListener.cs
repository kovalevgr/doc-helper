using System;
using System.Threading.Tasks;
using DocHelper.Domain.Events;

namespace DocHelper.Infrastructure.MessageBrokers.RabbitMQ
{
    public class RabbitMQListener : IEventListener
    {
        public void Subscribe(Type type)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TEvent>() where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public Task Publish(string message, string type)
        {
            throw new NotImplementedException();
        }
    }
}