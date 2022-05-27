using FluentValidation;
using minimal_api.Entities;

namespace minimal_api.Validation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Manufacturer)
                .NotEmpty()
                .MinimumLength(3)
                .NotEqual(car => car.Model);
            
            RuleFor(car => car.Model)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
