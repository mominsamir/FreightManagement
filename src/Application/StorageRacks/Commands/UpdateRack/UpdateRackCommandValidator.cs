using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Terminal.Commands.UpdateTerminal;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.StorageRacks.Commands.CreateTerminal
{
    public class UpdateRackValidator : AbstractValidator<UpdateRackCommand>
    {
        private readonly IApplicationDbContext _context;


        public UpdateRackValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Rack name is required.")
                .MaximumLength(200).WithMessage("Rack name must not exceed 200 characters.");

            RuleFor(v => v.IRSCode)
                .NotEmpty().WithMessage("IRS Code is required.")
                .MaximumLength(50).WithMessage("IRS Code must not exceed 50 characters.");


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

            RuleFor(v => v)
                .MustAsync(BeUniqueName).WithMessage("The Rack Name already exists.")
                .MustAsync(BeUniqueIRSCode).WithMessage("The IRS Code already exists.");
        }

        public async Task<bool> BeUniqueName(UpdateRackCommand command, CancellationToken cancellationToken)
        {
            return await _context.Racks.AllAsync(r => r.Name != command.Name && r.Id != command.Id);
        }
        public async Task<bool> BeUniqueIRSCode(UpdateRackCommand command, CancellationToken cancellationToken)
        {
            return await _context.Racks.AllAsync(r => r.IRSCode != command.IRSCode && r.Id != command.Id);
        }

    }
}
