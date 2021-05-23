using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Factory.Doctor
{
    public static class CreateDoctorStatsFactory
    {
        public static Stats Create(Entities.DoctorAggregate.Doctor doctor)
        {
            return new(doctor);
        }
    }
}