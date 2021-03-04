using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Infrastructure.Persistence.Exceptions;

namespace DocHelper.Infrastructure.Persistence
{
    public static class ApplicationDbSeed
    {
        private static Dictionary<string, object> _references = new Dictionary<string, object>();
        
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            var seeds = GetSeedsList();

            foreach (Type seed in seeds)
            {
                var seedInstance = (IApplicationDbSeed) Activator.CreateInstance(seed);
            
                seedInstance?.Refresh(context);
                seedInstance?.SeedAsync(context).GetAwaiter().GetResult();
            }
            
            await context.SaveChangesAsync();
        }

        public static T GetReferences<T>(string key)
        {
            if (!_references.TryGetValue(key, out var reference))
            {
                throw new SeedReferencesException($"Seed reference {key} - not exist!");
            }

            return (T)reference;
        }
        
        public static void SetReferences<T>(string key, T value)
        {
            if (!_references.TryAdd(key, value))
            {
                throw new SeedReferencesException($"Seed reference {key} - exist!");
            }
        }

        private static IEnumerable<Type> GetSeedsList() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IApplicationDbSeed).IsAssignableFrom(p))
            .Where(p => p.IsClass);
    }
}