using FluentValidation;

namespace DocHelper.Domain.Events.Doctor
{
    public class DoctorStatCreatedEvent : IEvent
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public int CountComments { get; set; }
        public int CountLikes { get; set; }
        public int CountDisLikes { get; set; }
        
        public class Validator : AbstractValidator<DoctorStatCreatedEvent>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();
                RuleFor(e => e.Rating).NotEmpty();
                RuleFor(e => e.CountComments).NotEmpty();
                RuleFor(e => e.CountLikes).NotEmpty();
                RuleFor(e => e.CountDisLikes).NotEmpty();
            }
        }
    }
}