using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.Specialty.Queries.ListSpecialtiesWithPagination;
using DocHelper.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DocHelper.Web.Controllers
{
    public class SpecialtiesController : ApiControllerBase
    {
        public async Task<IEnumerable<SpecialtyDto>> Get([FromQuery] ListSpecialtiesWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}