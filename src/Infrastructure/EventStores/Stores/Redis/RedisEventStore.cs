using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace DocHelper.Infrastructure.EventStores.Stores.Redis
{
    public class RedisEventStore : IStore
    {
        private readonly IDatabase _redisClient;
        
        public RedisEventStore(IRedisDatabase redisClient)
        {
            _redisClient = redisClient.Database;
        }
        
        public Task Add(StreamState stream)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StreamState>> GetEvents(Guid aggregateId, int? version = null, DateTime? createdUtc = null)
        {
            throw new NotImplementedException();
        }
    }
}