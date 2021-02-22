using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Commands.UpdateTruck
{
    public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTruckCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.VIN)
                 .NotEmpty().WithMessage("VIN # is required.")
                 .MaximumLength(200).WithMessage("VIN # must not exceed 50 characters.");

            RuleFor(v => v.NumberPlate)
                .NotEmpty().WithMessage("Number plate is required.")
                .MaximumLength(50).WithMessage("Number plate must not exceed 50 characters.");

            RuleFor(v => v)
                 .MustAsync(BeUniqueVIN).WithMessage("VIN # already exists.")
                 .MustAsync(BeUniqueNumberPlate).WithMessage("Number plate already exists.");
        }

        public async Task<bool> BeUniqueVIN(UpdateTruckCommand t, CancellationToken cancellationToken)
        {
            return await _context.Trucks.AllAsync(l => l.VIN == t.VIN && l.Id != t.Id, cancellationToken);
        }
        public async Task<bool> BeUniqueNumberPlate(UpdateTruckCommand t, CancellationToken cancellationToken)
        {
            return await _context.Trucks.AllAsync(l => l.NumberPlate == t.NumberPlate && l.Id != t.Id, cancellationToken);
        }
    }
}
