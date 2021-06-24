using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using DocHelper.Domain.Events;
using DocHelper.Infrastructure.Events;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DocHelper.Infrastructure.MessageBrokers.Kafka
{
    public class KafkaListener : IEventListener
    {
        private readonly IEventBus _eventBus;
        private readonly KafkaOptions _options;

        private string[] Topics => _options.Topics.Split(";");

        public KafkaListener(IEventBus eventBus, IOptions<KafkaOptions> options)
        {
            _eventBus = eventBus;
            _options = options.Value;
        }

        public void Subscribe<TEvent>() where TEvent : IEvent => Subscribe(typeof(TEvent));

        public void Subscribe(Type type)
        {
            using var consumer = new ConsumerBuilder<string, string>(_options.Consumer).Build();
            consumer.Subscribe(Topics);
            while (true)
            {
                var message = consumer.Consume();

                var @event = JsonConvert.DeserializeObject(message.Message.Value, type) as IEvent;

                _eventBus.PublishLocal(@event);
            }
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            using var p = new ProducerBuilder<string, string>(_options.Producer).Build();
            await p.ProduceAsync(_options.Topic,
                new Message<string, string>
                {
                    Key = MessageBrokersHelper.GetTypeName<TEvent>(),
                    Value = JsonConvert.SerializeObject(@event)
                });
        }

        public async Task Publish(string message, string type)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Event message can not be null.");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException(nameof(type), "Event type can not be null.");
            }

            using var p = new ProducerBuilder<string, string>(_options.Producer).Build();
            await p.ProduceAsync(_options.Topic,
                new Message<string, string>
                {
                    Key = type,
                    Value = message
                });
        }
    }
}