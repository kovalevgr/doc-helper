using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.Doctor.Command.CreateDoctorCommand;
using DocHelper.Application.Doctor.Queries.ListDoctorsWithPagination;
using DocHelper.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DocHelper.Web.Controllers
{
    public class DoctorsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<DoctorListDto>> Get([FromQuery] ListDoctorsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        public async Task<int> Create(CreateDoctorCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}