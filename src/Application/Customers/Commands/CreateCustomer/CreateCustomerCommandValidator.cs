using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IApplicationDbContext _contex;

        public CreateCustomerCommandValidator(IApplicationDbContext contex)
        {
            _contex = contex;

            RuleFor(v => v.Name)
                 .NotEmpty().WithMessage("Customer name is required.")
                 .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.")
                 .MustAsync(BeUniqueName).WithMessage("Customer name must not exceed 200 characters.");

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

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _contex.Customers.AllAsync(l => l.Name == name,cancellationToken);
        }
   }
}
