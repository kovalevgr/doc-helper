using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using MediatR;

namespace DocHelper.Application.City.Commands.ListCities
{
    public class ListCitiesCommand : IRequest<List<Domain.Entities.City>>
    { }

    public class ListCitiesCommandHandler : IRequestHandler<ListCitiesCommand, List<Domain.Entities.City>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ListCitiesCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Domain.Entities.City>> Handle(ListCitiesCommand request,
            CancellationToken cancellationToken)
        {
            return _applicationDbContext.Cities.ToList();
        }
    }
}