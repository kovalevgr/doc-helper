﻿using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.City> Cities { get; set; }
        DbSet<Domain.Entities.Specialty> Specialties { get; set; }
        DbSet<Domain.Entities.DoctorAggregate.Doctor> Doctors { get; set; }
        DbSet<Information> Informations { get; set; }
        DbSet<Stats> Stats { get; set; }

        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Creates a <see cref="DbSet{TEntity}" /> that can be used to query and save instances of <typeparamref name="TEntity" />.
        /// </summary>
        /// <typeparam name="TEntity"> The type of entity for which a set should be returned. </typeparam>
        /// <returns> A set for the given entity type. </returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}