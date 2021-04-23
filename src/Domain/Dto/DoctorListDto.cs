using System.Collections.Generic;
using DocHelper.Domain.Common.Mappings;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Dto
{
    public class DoctorListDto : IMapFrom<Doctor>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Titles { get; set; }
        public int WorkExperience { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public StatsDto Stats { get; set; }
        public IList<InformationDto> Informations { get; set; }
    }
}