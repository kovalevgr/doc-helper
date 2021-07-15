using System.Linq;
using DocHelper.Domain.Entities;
using DocHelper.Infrastructure.Persistence;
using HotChocolate;
using HotChocolate.Data;

namespace DocHelper.Web.GraphQL
{
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Query(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        [UseFiltering]
        [UseDbContext(typeof(ApplicationDbContext))]
        [GraphQLDescription("Gets the queryable city.")]
        public IQueryable<City> Cities() => _applicationDbContext.Cities;
        
        [UseFiltering]
        [UseSorting]
        [UseDbContext(typeof(ApplicationDbContext))]
        [GraphQLDescription("Gets the queryable specialty.")]
        public IQueryable<Specialty> GetSpecialty([ScopedService] ApplicationDbContext context) => context.Specialties;
    }
}