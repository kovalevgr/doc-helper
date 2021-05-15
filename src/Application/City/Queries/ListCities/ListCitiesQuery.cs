using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Repository;
using MediatR;

namespace DocHelper.Application.City.Queries.ListCities
{
    public class ListCitiesQuery : IRequest<IReadOnlyList<Domain.Entities.City>>
    { }

    public class ListCitiesQueryHandler : IRequestHandler<ListCitiesQuery, IReadOnlyList<Domain.Entities.City>>
    {
        private readonly ICityRepository _cityRepository;

        public ListCitiesQueryHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IReadOnlyList<Domain.Entities.City>> Handle(ListCitiesQuery request,
            CancellationToken cancellationToken)
        {
            return await _cityRepository.ListAllAsync(cancellationToken);
        }
    }
}