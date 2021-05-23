using DocHelper.Domain.Dto.Doctor.Create;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Factory.Doctor
{
    public static class CreateDoctorInformationFactory
    {
        public static Information Create(Entities.DoctorAggregate.Doctor doctor, InformationDto dto)
        {
            return new (
                dto.Type,
                dto.Title,
                dto.Priority,
                doctor
            );
        }
    }
}