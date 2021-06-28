using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Events.Doctor;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.EventStores;
using DocHelper.Infrastructure.Persistence;

namespace DocHelper.Infrastructure.Repository
{
    public class DoctorRepository : AggregateRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext dbContext, IEventStore eventStore)
            : base(dbContext, eventStore)
        { }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor,
            CancellationToken cancellationToken = default)
        {
            doctor.AggregateId = AggregateId;

            var result = await AddAsync(doctor, cancellationToken);

            var doctorEvent = new DoctorCreatedEvent
            {
                Id = result.Id,
                Alias = result.Alias,
                Description = result.Description,
                FirstName = result.FirstName,
                LastName = result.LastName,
                MiddleName = result.MiddleName,
                Titles = result.Titles,
                WorkExperience = result.WorkExperience
            };

            Enqueue(doctorEvent);

            return result;
        }

        public async Task<Stats> CreateStatAsync(Stats stats,
            CancellationToken cancellationToken = default)
        {
            var result = await AddAsync(stats, cancellationToken);

            var doctorStatEvent = new DoctorStatCreatedEvent
            {
                Id = result.Id,
                Rating = result.Rating,
                CountComments = result.CountComments,
                CountLikes = result.CountLikes,
                CountDisLikes = result.CountDisLikes
            };

            Enqueue(doctorStatEvent);

            return result;
        }

        public async Task<Information> CreateInformationAsync(
            Information information,
            CancellationToken cancellationToken = default)
        {
            var result = await AddAsync(information, cancellationToken);

            var doctorInformationEvent = new DoctorInformationCreatedEvent
            {
                Id = result.Id,
                Type = result.Type,
                Title = result.Title,
                Priority = result.Priority
            };
            
            Enqueue(doctorInformationEvent);

            return result;
        }
    }
}