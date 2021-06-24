using System;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Dto.Doctor.Create;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Factory.Doctor;
using DocHelper.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DocHelper.Application.Doctor.Command.CreateDoctorCommand
{
    public class CreateDoctorCommand : DoctorCreateDto, IRequest<int>
    {
        public class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, int>
        {
            private readonly IDoctorRepository _aggregateRepository;
            private readonly ILogger<CreateDoctorHandler> _logger;

            public CreateDoctorHandler(IDoctorRepository aggregateRepository,
                ILogger<CreateDoctorHandler> logger)
            {
                _aggregateRepository = aggregateRepository;
                _logger = logger;
            }

            public async Task<int> Handle(
                CreateDoctorCommand command,
                CancellationToken cancellationToken)
            {
                DoctorCreateDto dto = command;

                _aggregateRepository.BeginTransaction();

                var doctor = await CreateDoctor(dto, cancellationToken);

                try
                {
                    CreateDoctorStat(doctor, cancellationToken);

                    ProcessingInformations(doctor, dto, cancellationToken);
                    
                    _aggregateRepository.SaveChanges();
                    
                    _aggregateRepository.Commit();
                }
                catch (Exception exception)
                {
                    _aggregateRepository.Rollback();
                    
                    _logger.LogError(exception.Message);

                    throw;
                }
                finally
                {
                    _aggregateRepository.Dispose();
                }

                return doctor.Id;
            }

            private async void ProcessingInformations(
                Domain.Entities.DoctorAggregate.Doctor doctor,
                DoctorCreateDto dto,
                CancellationToken cancellationToken)
            {
                foreach (var information in dto.Informations)
                {
                    await CreateInformation(doctor, information, cancellationToken);
                }
            }

            private Task<Information> CreateInformation(
                Domain.Entities.DoctorAggregate.Doctor doctor,
                InformationDto dto,
                CancellationToken cancellationToken)
            {
                return _aggregateRepository.CreateInformationAsync(CreateDoctorInformationFactory.Create(doctor, dto),
                    cancellationToken);
            }

            private async void CreateDoctorStat(
                Domain.Entities.DoctorAggregate.Doctor doctor,
                CancellationToken cancellationToken)
            {
                await _aggregateRepository.CreateStatAsync(CreateDoctorStatsFactory.Create(doctor), cancellationToken);
            }

            private async Task<Domain.Entities.DoctorAggregate.Doctor> CreateDoctor(
                DoctorCreateDto dto,
                CancellationToken cancellationToken)
            {
                return await _aggregateRepository.AddAsync(CreateDoctorFactory.Create(dto), cancellationToken);
            }
        }
    }
}