using FluentValidation;
using FreightManagement.Application.Common.Interfaces;

namespace FreightManagement.Application.Customers.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {

        public UpdateLocationCommandValidator(IApplicationDbContext contex)
        {

            RuleFor(v => v.Name)
                 .NotEmpty().WithMessage("Location name is required.")
                 .MaximumLength(200).WithMessage("Location name must not exceed 200 characters.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress()
                .MaximumLength(200).WithMessage("City must not exceed 200 characters.");

            RuleFor(v => v.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(200).WithMessage("City must not exceed 200 characters.");

            RuleFor(v => v.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street must not exceed 200 characters.");

            RuleFor(v => v.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(25).WithMessage("State must not exceed 25 characters.");

            RuleFor(v => v.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .MaximumLength(12).WithMessage("ZipCode must not exceed 12 characters.");

            RuleFor(v => v.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(20).WithMessage("Country must not exceed 20 characters.");
        }
    }
}
