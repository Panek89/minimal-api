using FluentValidation;
using minimal_api.Entities;

namespace minimal_api.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotEqual(user => user.LastName)
                .MinimumLength(3)
                .MaximumLength(20);
            
            RuleFor(user => user.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}