using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Terminal.Commands.CreateTerminal;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.StorageRacks.Commands.CreateTerminal
{
    public class CreateRackCommandValidator : AbstractValidator<CreateRackCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRackCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Rack name is required.")
                .MaximumLength(200).WithMessage("Rack name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("The rack name already exists.");

            RuleFor(v => v.IRSCode)
                .NotEmpty().WithMessage("IRS Code is required.")
                .MaximumLength(50).WithMessage("IRS Code must not exceed 50 characters.")
                .MustAsync(BeUniqueIRSCode).WithMessage("The IRS code already exists.");

            RuleFor(v => v.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(v => v.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(50).WithMessage("Street must not exceed 50 characters.");

            RuleFor(v => v.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State must not exceed 50 characters.");

            RuleFor(v => v.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .MaximumLength(50).WithMessage("ZipCode must not exceed 50 characters.");

            RuleFor(v => v.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Racks .AllAsync(l => l.Name != name);
        }
        public async Task<bool> BeUniqueIRSCode(string IRSCode, CancellationToken cancellationToken)
        {
            return await _context.Racks.AllAsync(l => l.IRSCode != IRSCode);
        }

    }
}
