using System.Linq;
using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Domain.Graph.Types
{
    public class CityType : ObjectType<City>, IType
    {
        protected override void Configure(IObjectTypeDescriptor<City> descriptor)
        {
            descriptor.Description("Represents any executable command.");
            
            descriptor
                .Field(c => c.Id)
                .Description("Represents the unique ID for the city.");
            
            descriptor
                .Field(c => c.Name)
                .Description("Represents the Name for the city.");
            
            // descriptor
            //     .Field(c => c.Specialties)
            //     .ResolveWith<Resolvers>(p => p.GetSpecialties(default!, default!))
            //     .UseDbContext<ApplicationDbContext>()
        }
        
        private class Resolvers
        {
            public IQueryable<Specialty> GetSpecialties(City city, [ScopedService] IApplicationDbContext context)
            {
                return context.Specialties.Where(s => s.City.Id == city.Id);
            }
        }
    }
}