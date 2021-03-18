using DocHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocHelper.Infrastructure.Persistence.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.Property(s => s.Title)
                .IsRequired();

            builder.Property(s => s.Alias)
                .IsRequired();
        }
    }
}