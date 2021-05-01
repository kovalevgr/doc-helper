using DocHelper.Domain.Common.Mappings;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Dto
{
    public class DoctorSpecialtyDto : IMapFrom<DoctorSpecialty>
    {
        public SpecialtyDto Specialty { get; set; }
    }
}