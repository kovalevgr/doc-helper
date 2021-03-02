using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Application.Common.Mappings;
using DocHelper.Application.Common.Models;
using DocHelper.Domain.Dto;
using MediatR;

namespace DocHelper.Application.Specialty.Command.ListSpecialtiesWithPagination
{
    public class ListSpecialtiesWithPaginationCommand : IRequest<PaginatedList<SpecialtyDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }
    
    public class ListSpecialtiesWithPaginationCommandHandler : IRequestHandler<ListSpecialtiesWithPaginationCommand, PaginatedList<SpecialtyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListSpecialtiesWithPaginationCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<PaginatedList<SpecialtyDto>> Handle(ListSpecialtiesWithPaginationCommand request, CancellationToken cancellationToken)
        {
            return _context.Specialties
                .ProjectTo<SpecialtyDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}