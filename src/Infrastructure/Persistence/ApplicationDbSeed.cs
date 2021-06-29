using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Domain.Common.Interfaces;
using DocHelper.Infrastructure.Persistence.Exceptions;

namespace DocHelper.Infrastructure.Persistence
{
    public static class ApplicationDbSeed
    {
        private static Dictionary<string, object> _references = new Dictionary<string, object>();

        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            var seeds = GetSeedsList();
            foreach (var seedInstance in seeds)
            {
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

            return (T) reference;
        }

        public static void SetReferences<T>(string key, T value)
        {
            if (!_references.TryAdd(key, value))
            {
                throw new SeedReferencesException($"Seed reference {key} - exist!");
            }
        }

        public static void ResetReferences()
        {
            _references = new Dictionary<string, object>();
        }

        private static IEnumerable<IApplicationDbSeed> GetSeedsList() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IApplicationDbSeed).IsAssignableFrom(p))
            .Where(p => p.IsClass)
            .Select(s => (IApplicationDbSeed) Activator.CreateInstance(s))
            .OrderBy(s => s?.Priority);
    }
}