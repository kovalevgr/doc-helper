using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Doctor.Command.CreateDoctorCommand;
using DocHelper.Application.Doctor.Queries.ListDoctorsWithPagination;
using DocHelper.Domain.Dto;
using Doctor;
using Grpc.Core;
using MediatR;

namespace DocHelper.Web.Services
{
    public class DoctorService : DoctorRPCService.DoctorRPCServiceBase
    {
        private readonly ISender _mediator;

        public DoctorService(ISender mediator)
        {
            _mediator = mediator;
        }

        public override async Task<DoctorList> GetDoctorList(
            DoctorListQuery request,
            ServerCallContext context)
        {
            var query = ListDoctorFactory.CreateQuery(request);

            var result = await _mediator.Send(query);

            return ListDoctorFactory.CreateResponse(result);
        }

        public override async Task<DoctorCreateResponse> CreateDoctor(
            DoctorData request,
            ServerCallContext context)
        {
            var command = CreateDoctorFactory.CreateCommand(request);

            var doctorId = await _mediator.Send(command);

            return CreateDoctorFactory.CreateResponse(doctorId);
        }

        public override Task<DoctorData> GetDoctor(DoctorQuery request, ServerCallContext context)
        {
            return base.GetDoctor(request, context);
        }

        private static class ListDoctorFactory
        {
            public static ListDoctorsWithPaginationQuery CreateQuery(DoctorListQuery query)
            {
                return new ListDoctorsWithPaginationQuery()
                {
                    PageNumber = query.PageNumber,
                    PageSize = query.PageSize,
                    DistrictAlias = query.DistrictAlias,
                    SpecialtyAlias = query.SpecialtyAlias
                };
            }

            public static DoctorList CreateResponse(IEnumerable<DoctorListDto> doctorListDto)
            {
                var response = new DoctorList();

                response.DoctorItemList.AddRange(
                    doctorListDto.Select(
                        dl => new DoctorData
                        {
                            Id = dl.Id,
                            Alias = dl.Alias,
                            FirstName = dl.FirstName,
                            LastName = dl.LastName,
                            MiddleName = dl.MiddleName,
                            Titles = dl.Titles,
                            WorkExperience = dl.WorkExperience,
                            Description = dl.Description,
                            Photo = dl.Photo,
                            Stats =
                            {
                                Rating = dl.Stats.Rating,
                                CountComments = dl.Stats.CountComments,
                                CountLikes = dl.Stats.CountLikes,
                                CountDisLikes = dl.Stats.CountDisLikes
                            },
                        }
                    )
                );

                return response;
            }
        }
        
        private static class CreateDoctorFactory
        {
            public static CreateDoctorCommand CreateCommand(DoctorData request)
            {
                return new()
                {
                    Alias = request.Alias,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    Titles = request.Titles,
                    WorkExperience = request.WorkExperience,
                    Description = request.Description,
                    Photo = request.Photo
                    
                };
            }

            public static DoctorCreateResponse CreateResponse(int doctorId)
            {
                return new()
                {
                    OriginalId = doctorId,
                    StatusCode = 201,
                    StatusMessage = "Created"
                };
            }
        }
    }
}
