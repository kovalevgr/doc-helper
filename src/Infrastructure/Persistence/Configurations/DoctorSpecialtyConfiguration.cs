using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocHelper.Infrastructure.Persistence.Configurations
{
    public class DoctorSpecialtyConfiguration : IEntityTypeConfiguration<DoctorSpecialty>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
        {
            builder.ToTable("DoctorSpecialties");
            
            builder
                .HasKey(dc => new {dc.DoctorId, dc.SpecialtyId});

            builder
                .HasOne(dc => dc.Doctor)
                .WithMany(d => d.Specialties)
                .HasForeignKey(dc => dc.DoctorId);
            
            builder
                .HasOne(dc => dc.Specialty);
        }
    }
}