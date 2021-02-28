using System.Reflection;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<City> Cities { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // TODO Implement this in the future
            // foreach (Type entityType in GetEntityTypes())
            // {
            //     modelBuilder.Entity(entityType);
            // }
            
            base.OnModelCreating(modelBuilder);
        }

        // private IEnumerable<Type> GetEntityTypes() => AppDomain.CurrentDomain.GetAssemblies()
        //     .SelectMany(x => x.GetTypes())
        //     .Where(x => typeof(IEntity).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        // ;
    }
}