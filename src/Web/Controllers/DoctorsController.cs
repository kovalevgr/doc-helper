using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Application.Doctor.Command.ListDoctorsWithPagination;
using DocHelper.Domain.Dto;

namespace DocHelper.Web.Controllers
{
    public class DoctorsController : ApiControllerBase
    {
        public async Task<IEnumerable<DoctorListDto>> Get()
        {
            return await Mediator.Send(new ListDoctorsWithPaginationCommand());
        }
    }
}