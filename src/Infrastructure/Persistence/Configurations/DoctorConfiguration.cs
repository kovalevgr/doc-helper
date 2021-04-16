using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocHelper.Infrastructure.Persistence.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.Property(doctor => doctor.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(doctor => doctor.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(doctor => doctor.MiddleName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(doctor => doctor.Titles)
                .IsRequired();

            builder.Property(doctor => doctor.WorkExperience)
                .IsRequired();

            builder.Property(doctor => doctor.Description)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(doctor => doctor.Photo)
                .IsRequired();

            builder
                .HasOne(doctor => doctor.Stats)
                .WithOne(s => s.Doctor)
                .HasForeignKey<Stats>(s => s.DoctorId);

            builder
                .HasMany(c => c.DoctorInformations)
                .WithOne(i => i.Doctor)
                .HasForeignKey(i => i.DoctorId);
        }
    }
}