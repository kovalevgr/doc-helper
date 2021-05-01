namespace DocHelper.Domain.Entities.DoctorAggregate
{
    public class DoctorSpecialty : BaseEntity
    {
        public Doctor Doctor { get; private set; }
        public int DoctorId { get; set; }
        public Specialty Specialty { get; private set; }
        public int SpecialtyId { get; set; }

        private DoctorSpecialty()
        {}
        
        public DoctorSpecialty(Doctor doctor, Specialty specialty)
        {
            Doctor = doctor;
            Specialty = specialty;
        }
    }
}