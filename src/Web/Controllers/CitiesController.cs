using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.City.Commands.ListCities;

namespace DocHelper.Web.Controllers
{
    public class CitiesController : ApiControllerBase
    {
        public async Task<List<Domain.Entities.City>> Get()
        {
            return await Mediator.Send(new ListCitiesCommand());
        }
    }
}