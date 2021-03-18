using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Repository;
using MediatR;

namespace DocHelper.Application.City.Commands.ListCities
{
    public class ListCitiesCommand : IRequest<IReadOnlyList<Domain.Entities.City>>
    { }

    public class ListCitiesCommandHandler : IRequestHandler<ListCitiesCommand, IReadOnlyList<Domain.Entities.City>>
    {
        private readonly ICityRepository _cityRepository;

        public ListCitiesCommandHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IReadOnlyList<Domain.Entities.City>> Handle(ListCitiesCommand request,
            CancellationToken cancellationToken)
        {
            return await _cityRepository.ListAllAsync(cancellationToken);
        }
    }
}