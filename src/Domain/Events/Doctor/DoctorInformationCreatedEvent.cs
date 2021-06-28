using DocHelper.Domain.Enums;
using FluentValidation;

namespace DocHelper.Domain.Events.Doctor
{
    public class DoctorInformationCreatedEvent : IEvent
    {
        public int Id { get; set; }
        public InformationType Type { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        
        public class Validator : AbstractValidator<DoctorInformationCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();
                RuleFor(e => e.Type).NotEmpty();
                RuleFor(e => e.Title).NotEmpty();
                RuleFor(e => e.Priority).NotEmpty();
            }
        }
    }
}