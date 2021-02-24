using FluentValidation;
using FreightManagement.Application.Common.Interfaces;


namespace FreightManagement.Application.Customers.Commands.AddTankToLocation
{
    public class AddTankToLocationCommandValidator : AbstractValidator<AddTankToLocationCommand>
    {
        public readonly IApplicationDbContext _context;

        public AddTankToLocationCommandValidator(IApplicationDbContext context)
        {
            _context = context;


            RuleFor(v => v.Name)
                 .NotEmpty().WithMessage("Customer name is required.")
                 .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.");

            RuleFor(v => v.fuelGrade)
                .NotEmpty().WithMessage("FuelGrade is required.");

            RuleFor(v => v.Capactity)
                .NotEmpty().WithMessage("Capacity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Capacity is required.");

        }
    }
}
