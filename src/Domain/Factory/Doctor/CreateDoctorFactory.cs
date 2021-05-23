using DocHelper.Domain.Dto.Doctor.Create;

namespace DocHelper.Domain.Factory.Doctor
{
    public static class CreateDoctorFactory
    {
        public static Entities.DoctorAggregate.Doctor Create(DoctorCreateDto dto)
        {
            return new (
                dto.Alias,
                dto.FirstName,
                dto.LastName,
                dto.MiddleName,
                dto.Titles,
                dto.WorkExperience,
                dto.Description,
                dto.Photo
            );
        }
    }
}