using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocHelper.Application.Common.Mappings;
using DocHelper.Application.Common.Models;
using DocHelper.Application.Common.Specifications;
using DocHelper.Domain.Dto;
using DocHelper.Domain.Repository;
using MediatR;

namespace DocHelper.Application.Specialty.Queries.ListSpecialtiesWithPagination
{
    public class ListSpecialtiesWithPaginationQuery : IRequest<IEnumerable<SpecialtyDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }

    public class
        ListSpecialtiesWithPaginationQueryHandler : IRequestHandler<ListSpecialtiesWithPaginationQuery,
            IEnumerable<SpecialtyDto>>
    {
        private readonly ISpecialtyRepository _repository;
        private readonly IMapper _mapper;
        private readonly SpecBuilder<Domain.Entities.Specialty> _builder;

        public ListSpecialtiesWithPaginationQueryHandler(
            ISpecialtyRepository repository,
            IMapper mapper,
            SpecBuilderFactory builder)
        {
            _repository = repository;
            _mapper = mapper;
            _builder = builder.Create<Domain.Entities.Specialty>();
        }

        public async Task<IEnumerable<SpecialtyDto>> Handle(ListSpecialtiesWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            var queryable =  _builder
                .Queryable
                .ProjectTo<SpecialtyDto>(_mapper.ConfigurationProvider)
                .Paginate(request.PageNumber, request.PageSize);

            return await _repository.ListAsync(queryable, cancellationToken);
        }
    }
}