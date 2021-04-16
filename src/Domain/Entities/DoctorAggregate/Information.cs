using DocHelper.Domain.Enums;

namespace DocHelper.Domain.Entities.DoctorAggregate
{
    public class Information : BaseEntity
    {
        public InformationType Type { get; private set; }
        public string Title { get; private set; }
        public int Priority { get; private set; }

        public Doctor Doctor { get; private set; }
        public int DoctorId { get; set; }

        private Information()
        {
        }

        public Information(InformationType type, string title, int priority, Doctor doctor)
        {
            Type = type;
            Title = title;
            Priority = priority;
            Doctor = doctor;
        }
    }
}