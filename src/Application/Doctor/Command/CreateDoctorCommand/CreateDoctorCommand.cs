using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Dto.Doctor.Create;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Factory.Doctor;
using DocHelper.Domain.Repository;
using MediatR;

namespace DocHelper.Application.Doctor.Command.CreateDoctorCommand
{
    public class CreateDoctorCommand : DoctorCreateDto, IRequest<int>
    {
        public class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, int>
        {
            private readonly IDoctorRepository _repository;

            public CreateDoctorHandler(IDoctorRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(
                CreateDoctorCommand command,
                CancellationToken cancellationToken)
            {
                DoctorCreateDto dto = command;

                var doctor = await CreateDoctor(dto, cancellationToken);

                CreateDoctorStat(doctor, cancellationToken);

                ProcessingInformations(doctor, dto, cancellationToken);
                
                return doctor.Id;
            }

            private async void ProcessingInformations(
                Domain.Entities.DoctorAggregate.Doctor doctor,
                DoctorCreateDto dto,
                CancellationToken cancellationToken)
            {
                var tasks = new List<Task<Information>>();
                foreach (var information in dto.Informations)
                {
                    tasks.Add(CreateInformation(doctor, information, cancellationToken));
                }

                await Task.WhenAll(tasks);
            }

            private Task<Information> CreateInformation(
                Domain.Entities.DoctorAggregate.Doctor doctor,
                InformationDto dto,
                CancellationToken cancellationToken)
            {
                return _repository.AddInformationAsync(CreateDoctorInformationFactory.Create(doctor, dto),
                    cancellationToken);
            }

            private async void CreateDoctorStat(
                Domain.Entities.DoctorAggregate.Doctor doctor,
                CancellationToken cancellationToken)
            {
                await _repository.AddStatAsync(CreateDoctorStatsFactory.Create(doctor), cancellationToken);
            }

            private async Task<Domain.Entities.DoctorAggregate.Doctor> CreateDoctor(
                DoctorCreateDto dto,
                CancellationToken cancellationToken)
            {
                return await _repository.AddAsync(CreateDoctorFactory.Create(dto), cancellationToken);
            }
        }
    }
}