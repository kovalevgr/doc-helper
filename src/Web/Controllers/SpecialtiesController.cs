using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.Specialty.Command.ListSpecialties;
using DocHelper.Domain.Entities;

namespace DocHelper.Web.Controllers
{
    public class SpecialtiesController : ApiControllerBase
    {
        public async Task<List<Specialty>> Get()
        {
            return await Mediator.Send(new ListSpecialtiesCommand());
        }
    }
}