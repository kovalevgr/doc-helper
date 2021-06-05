using FluentValidation;

namespace DocHelper.Application.Doctor.Command.CreateDoctorCommand
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(r => r.Alias)
                .MaximumLength(50)
                .NotEmpty();
            
            RuleFor(r => r.FirstName)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(r => r.LastName)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(r => r.MiddleName)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(r => r.Titles)
                .NotEmpty();
            
            RuleFor(r => r.WorkExperience)
                .NotEmpty();
            
            RuleFor(r => r.Description)
                .NotEmpty();
            
            RuleFor(r => r.Photo)
                .MaximumLength(50)
                .NotEmpty();

            // RuleForEach(r => r.Informations)
            //     .NotEmpty();
            //
            // RuleForEach(r => r.Specialties)
            //     .NotEmpty();
        }
    }
}