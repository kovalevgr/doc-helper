using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Repository;
using MediatR;

namespace DocHelper.Application.Specialty.Command.ListSpecialties
{
    public class ListSpecialtiesCommand : IRequest<IReadOnlyList<Domain.Entities.Specialty>>
    {
    }

    public class
        ListSpecialtiesCommandHandler : IRequestHandler<ListSpecialtiesCommand, IReadOnlyList<Domain.Entities.Specialty>>
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public ListSpecialtiesCommandHandler(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IReadOnlyList<Domain.Entities.Specialty>> Handle(ListSpecialtiesCommand request,
            CancellationToken cancellationToken)
        {
            return await _specialtyRepository.ListAllAsync(cancellationToken);
        }
    }
}