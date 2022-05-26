using FluentValidation;
using minimal_api.DB;
using minimal_api.Entities;

namespace minimal_api.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly MinApiContext _context;

        public UserValidator(MinApiContext context)
        {
            _context = context;

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotEqual(user => user.LastName)
                .MinimumLength(3)
                .MaximumLength(20);
            
            RuleFor(user => user.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(user => user.Login)
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(user => user.Login)
                .Must(BeUnique)
                .WithMessage("Login existed, please insert another");

            RuleFor(user => user.Password)
                .NotEmpty();

        }
        private bool BeUnique(string login)
        {
            return !_context.Users.Any(user => user.Login == login);
        }
    }
}