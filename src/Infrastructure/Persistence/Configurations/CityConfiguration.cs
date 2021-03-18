using DocHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocHelper.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Alias)
                .IsRequired();

            builder
                .HasMany(c => c.Specialties)
                .WithOne(s => s.City);
        }
    }
}