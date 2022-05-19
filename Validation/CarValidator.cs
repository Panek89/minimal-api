using FluentValidation;
using minimal_api.Entities;

namespace minimal_api.Validation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.Manufacturer)
                .NotEmpty()
                .MinimumLength(3);
            
            RuleFor(p => p.Model)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}