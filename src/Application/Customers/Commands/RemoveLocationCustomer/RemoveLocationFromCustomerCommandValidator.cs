using FluentValidation;


namespace FreightManagement.Application.Customers.Commands.RemoveLocationCustomer
{
    public class RemoveLocationFromCustomerCommandValidator : AbstractValidator<RemoveLocationFromCustomerCommand>
    {
        public RemoveLocationFromCustomerCommandValidator()
        {
            RuleFor(v => v).NotNull().WithMessage("Location is required.");

            RuleFor(v => v.Id)
                .NotNull().WithMessage("Customer Id is required.")
                .Equal(0).WithMessage("Customer Id is required.");

            RuleFor(v => v.LocationId)
                .NotNull().WithMessage("Location Id is required.")
                .Equal(0).WithMessage("Location Id is required.");
        }
    }
}
