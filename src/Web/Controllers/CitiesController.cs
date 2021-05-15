using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.City.Queries.ListCities;
using Microsoft.AspNetCore.Mvc;

namespace DocHelper.Web.Controllers
{
    public class CitiesController : ApiControllerBase
    {
        public async Task<IReadOnlyList<Domain.Entities.City>> Get([FromQuery] ListCitiesQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}