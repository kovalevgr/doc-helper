using System.Collections.Generic;

namespace DocHelper.Domain.Dto.Doctor.Create
{
    public class DoctorCreateDto
    {
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Titles { get; set; }
        public int WorkExperience { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public IList<InformationDto> Informations { get; set; } = new List<InformationDto>();
        public IList<SpecialtyDto> Specialties { get; set; } = new List<SpecialtyDto>();
    }
}