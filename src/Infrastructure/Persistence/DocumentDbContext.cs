using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Documents;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence
{
    public class DocumentDbContext : DbContext, IDocumentDbContext
    {
        public DbSet<Doctor> Doctors { get; set; }

        public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .Property(d => d.DoctorDocument)
                .HasColumnType("jsonb");
        }
    }
}