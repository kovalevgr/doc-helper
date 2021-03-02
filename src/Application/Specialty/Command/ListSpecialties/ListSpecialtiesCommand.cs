using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using MediatR;

namespace DocHelper.Application.Specialty.Command.ListSpecialties
{
    public class ListSpecialtiesCommand : IRequest<List<Domain.Entities.Specialty>>
    {
    }

    public class
        ListSpecialtiesCommandHandler : IRequestHandler<ListSpecialtiesCommand, List<Domain.Entities.Specialty>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ListSpecialtiesCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Domain.Entities.Specialty>> Handle(ListSpecialtiesCommand request,
            CancellationToken cancellationToken)
        {
            return _applicationDbContext.Specialties.ToList();
        }
    }
}