using FluentValidation;

namespace DocHelper.Domain.Events.Doctor
{
    public class DoctorCreatedEvent : IEvent
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Titles { get; set; }
        public int WorkExperience { get; set; }
        public string Description { get; set; }

        public class Validator : AbstractValidator<DoctorCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();
                RuleFor(e => e.Alias).NotEmpty();
                RuleFor(e => e.FirstName).NotEmpty();
                RuleFor(e => e.LastName).NotEmpty();
                RuleFor(e => e.MiddleName).NotEmpty();
                RuleFor(e => e.Titles).NotEmpty();
                RuleFor(e => e.WorkExperience).NotEmpty();
                RuleFor(e => e.Description).NotEmpty();
            }
        }
    }
}