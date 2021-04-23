using DocHelper.Domain.Common.Mappings;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Enums;

namespace DocHelper.Domain.Dto
{
    public class InformationDto : IMapFrom<Information>
    {
        public InformationType Type { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
    }
}