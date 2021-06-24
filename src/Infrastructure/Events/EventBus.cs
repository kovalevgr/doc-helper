using System.Threading.Tasks;
using DocHelper.Domain.Events;
using DocHelper.Infrastructure.MessageBrokers;
using MediatR;

namespace DocHelper.Infrastructure.Events
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly IEventListener _eventListener;

        public EventBus(IMediator mediator, IEventListener eventListener)
        {
            _mediator = mediator;
            _eventListener = eventListener;
        }
        
        public async Task PublishLocal(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }

        public async Task Commit(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await SendToMessageBroker(@event);
            }
        }

        private async Task SendToMessageBroker(IEvent @event)
        {
            await _eventListener.Publish(@event);
        }
    }
}