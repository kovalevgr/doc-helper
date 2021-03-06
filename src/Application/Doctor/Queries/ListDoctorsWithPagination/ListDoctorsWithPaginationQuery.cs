﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocHelper.Application.Common.Mappings;
using DocHelper.Application.Common.Specifications;
using DocHelper.Domain.Common.Services;
using DocHelper.Domain.Dto;
using DocHelper.Domain.Repository;
using DocHelper.Domain.Specification.Doctors;
using MediatR;

namespace DocHelper.Application.Doctor.Queries.ListDoctorsWithPagination
{
    public class ListDoctorsWithPaginationQuery : IRequest<IEnumerable<DoctorListDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;

        public string DistrictAlias { get; set; } = default;
        public string SpecialtyAlias { get; set; } = default;
    }

    public class
        ListDoctorsWithPaginationQueryHandler : IRequestHandler<ListDoctorsWithPaginationQuery,
            IEnumerable<DoctorListDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly SpecBuilder<Domain.Entities.DoctorAggregate.Doctor> _builder;

        public ListDoctorsWithPaginationQueryHandler(
            IDoctorRepository doctorRepository,
            IMapper mapper,
            SpecBuilderFactory builder,
            ILocationService locationService)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _locationService = locationService;
            _builder = builder.Create<Domain.Entities.DoctorAggregate.Doctor>();
        }


        public async Task<IEnumerable<DoctorListDto>> Handle(
            ListDoctorsWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            var specifications = _builder
                .AddSpecification(new DoctorLocationSpecification(_locationService.Location))
                .AddSpecification(new DoctorListSpecification(request.DistrictAlias, request.SpecialtyAlias));

            var queryable = specifications
                .Queryable
                .ProjectTo<DoctorListDto>(_mapper.ConfigurationProvider)
                .Paginate(request.PageNumber, request.PageSize);

            return await _doctorRepository.ListAsync(queryable, cancellationToken);
        }
    }
}