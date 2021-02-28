using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;

namespace DocHelper.Infrastructure.Persistence
{
    public static class ApplicationDbSeed
    {
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

        private static IEnumerable<Type> GetSeedsList() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IApplicationDbSeed).IsAssignableFrom(p))
            .Where(p => p.IsClass);
    }
}